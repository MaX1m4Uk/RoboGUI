import cv2

import numpy as np
import socket
import time
import select

from coppeliasim_zmqremoteapi_client import RemoteAPIClient
clnt = RemoteAPIClient()
sim = clnt.require("sim")
sim.setStepping(True)

connectionLost = False
curentBytesInfo = 1
willContinue = 1

KP = 5.0
V_MAX = 2.0       
V_MIN = 1.0        
SEARCH_TURN = 2.0 
LOST_TIMEOUT = 5.0
KI = 1.0
integral = 0.0
prevFrame = None

lastEn = 0.0
lostTime = 0.0
dt = sim.getSimulationTimeStep() 

NEAR = (0.65, 1.0)  
FAR = (0.35, 0.6)   
W_NEAR, W_FAR = 0.8, 0.2

def robotInit():
    robot = sim.getObject('/RobotnikSummitXL')
    front_left_wheel = sim.getObject('/RobotnikSummitXL/front_left_wheel')
    back_left_wheel = sim.getObject('/RobotnikSummitXL/back_left_wheel')
    front_right_wheel = sim.getObject('/RobotnikSummitXL/front_right_wheel')
    back_right_wheel = sim.getObject('/RobotnikSummitXL/back_right_wheel')
    cam = sim.getObject('/RobotnikSummitXL/visionSensor')
    return robot, front_left_wheel, back_left_wheel, front_right_wheel, back_right_wheel, cam

robot, front_left_wheel, back_left_wheel, front_right_wheel, back_right_wheel, cam = robotInit()

def setWheels(vL, vR):
    sim.setJointTargetVelocity(front_left_wheel, vL)
    sim.setJointTargetVelocity(back_left_wheel, vL)
    sim.setJointTargetVelocity(front_right_wheel, -vR)
    sim.setJointTargetVelocity(back_right_wheel, -vR)

def revokeConnection():
    client.close()
    setWheels(0, 0)
    sim.stopSimulation()
    cv2.destroyAllWindows()

def getLineMask(frame):
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    return cv2.inRange(hsv, np.array([0, 0, 0]), np.array([179, 70, 70]))

def lineCenterX(mask, top, bottom):
    h = mask.shape[0]
    band = mask[int(top * h):int(bottom * h), :]
    m = cv2.moments(band, binaryImage=True)
    if m['m00'] < 50:
        return None
    return m['m10'] / m['m00']

client = socket.socket()
client.connect(("127.0.0.1", 13000))
sim.startSimulation()

while not connectionLost:
    sim.step()
    img, res = sim.getVisionSensorImg(cam)
    frame = cv2.flip(np.frombuffer(img, np.uint8).reshape(res[1], res[0], 3), 0)
    frame = cv2.cvtColor(frame, cv2.COLOR_RGB2BGR)
    while True:
        readable, _, _ = select.select([client], [], [], 0)
        
        if client in readable:
            controlNow  = client.recv(curentBytesInfo)
    
            if len(controlNow) == 0:
                connectionLost = True
                break
    
            if connectionLost:
                break
            
            willContinue = controlNow[0]
        else:
            break

    if connectionLost:
        break

    frame_complete = frame

    mask = getLineMask(frame_complete)

    W = frame_complete.shape[1]
    H = frame_complete.shape[0]
    half = W / 2

    xNear = lineCenterX(mask, *NEAR)
    xFar = lineCenterX(mask, *FAR)

    en = None
    if xNear is not None and xFar is not None:
        en = W_NEAR * (xNear - half) / half + W_FAR * (xFar - half) / half
    elif xNear is not None:
        en = (xNear - half) / half
    elif xFar is not None:
        en = (xFar - half) / half

    for x, band in ((xNear, NEAR), (xFar, FAR)):
        if x is not None:
            cv2.circle(frame_complete, (int(x), int((band[0] + band[1]) / 2 * H)), 7, (97, 46, 176), -1)
    if en is None and prevFrame is not None and lostTime == 0.0:
        cv2.imwrite("lost_before.png", prevFrame)
        cv2.imwrite("lost_now.png", frame_complete)
    prevFrame = frame_complete.copy()
    if willContinue == 0:
        setWheels(0, 0)
    elif en is None:
        integral = 0.0
        lostTime += dt
        if lostTime > LOST_TIMEOUT:
            setWheels(0, 0)
        elif lastEn > 0:
            setWheels(SEARCH_TURN, -SEARCH_TURN)
        else:
            setWheels(-SEARCH_TURN, SEARCH_TURN)
    else:
        lostTime = 0.0
        if xFar is not None:
            lastEn = (xFar - half) / half
        elif abs(en) > 0.05:
            lastEn = en     
        integral = max(-0.5, min(0.5, integral + en * dt))
        u = KP * en + KI * integral
        v = V_MAX - (V_MAX - V_MIN) * min(1.0, abs(en))
        setWheels(v + u, v - u)

    print(xNear, xFar, en)

    cv2.imshow("frame", frame_complete)

    result, imgencode = cv2.imencode(".jpg", frame_complete)

    frame_bytes = imgencode.tobytes()

    frame_size = len(frame_bytes)
    size_bytes = frame_size.to_bytes(4, byteorder="big")

    client.sendall(size_bytes)
    client.sendall(frame_bytes)

if not connectionLost:
    endByte = 0
    client.sendall(endByte.to_bytes(4, byteorder="big"))
    revokeConnection()
else:
    revokeConnection()