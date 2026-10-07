import cv2

import numpy as np
import socket
import time
import select
import matplotlib.pyplot as plt

from ultralytics import YOLO
import torch

from ultralytics.utils.plotting import Colors
colors = Colors()

model = YOLO("yolo11n.pt")

cap = cv2.VideoCapture(r"C:\Users\max\Downloads\Coldplay - Adventure Of A Lifetime (Official Video).mp4")
connectionLost = False
curentBytesInfo = 13
mode = 1
willContinue = 1
rotPict = 0
flipHor = 0
flipVer = 0
baseSize = 0
baseDenoise = 0
baseColor = 0
baseGray = 0
isBinaryPic = 0
threshLevel = 0
isMorfolofy = 0
baseMorfology = 0

def picDetection(img):
    result = model(img)
    scores = result[0].boxes.conf.cpu().numpy()
    classes = result[0].boxes.cls.cpu().numpy().astype(np.int32)
    boxes = result[0].boxes.xyxy.cpu().numpy().astype(np.int32)
    return scores, classes, boxes

def picDrawRectangle(img, scores, classes, boxes):
    if len(img.shape) != 3:
        img = cv2.cvtColor(img, cv2.COLOR_GRAY2BGR)    
    for score, cls, bbox in zip(scores, classes, boxes):
        class_label = model.names[cls]
        label = f"{class_label} : {score:0.2f}"
        lbl_margin = 3
        img = cv2.rectangle(img, (bbox[0], bbox[1]), (bbox[2], bbox[3]), color=colors(cls, True), thickness=1)
        label_size = cv2.getTextSize(label, fontFace=cv2.FONT_HERSHEY_SIMPLEX, fontScale=1, thickness=1)
        lbl_w, lbl_h = label_size[0]
        lbl_w += 2*lbl_margin
        lbl_h += 2*lbl_margin
        img = cv2.rectangle(img, (bbox[0], bbox[1]), (bbox[0] + lbl_w, bbox[1] - lbl_h), color=colors(cls, True), thickness=-1)
        cv2.putText(img, label, (bbox[0] + lbl_margin, bbox[1] - lbl_margin), fontFace=cv2.FONT_HERSHEY_SIMPLEX, fontScale=1.0, color=(255,255,255), thickness=1)
    return img

def rotatePicture(img, rotation):
    if rotation == 0:
        result = img
        return result
    elif rotation == 1:
        result = cv2.rotate(img, cv2.ROTATE_90_CLOCKWISE)
        return result
    elif rotation == 2:
        result = cv2.rotate(img, cv2.ROTATE_180)
        return result
    elif rotation == 3:
        result = cv2.rotate(img, cv2.ROTATE_90_COUNTERCLOCKWISE)
        return result

def flipPictureHor(img, hor):
    if hor == 1:
        result = cv2.flip(img, 1)
        return result
    elif hor == 0:
        result = img
        return result
    
def flipPictureVer(img, ver):
    if ver == 1:
        result = cv2.flip(img, 0)
        return result
    elif ver == 0:
        result = img
        return result

def resizePicture(img, size):
    if size == 0:
        result = img
        return result
    elif size == 1:
        result = cv2.resize(img, None, fx=0.75, fy=0.75, interpolation=cv2.INTER_AREA)
        return result
    elif size == 2:
        result = cv2.resize(img, None, fx=0.5, fy=0.5, interpolation=cv2.INTER_AREA)
        return result
    elif size == 3:
        result = cv2.resize(img, None, fx=0.25, fy=0.25, interpolation=cv2.INTER_AREA)
        return result

def denoisePicture(img, value):
    if value == 0:
        result = img
        return result
    elif value == 1:
        result = cv2.GaussianBlur(img, (5, 5), 0)
        return result
    elif value == 2:
        result = cv2.medianBlur(img, 5)
        return result
    elif value == 3:
        result = cv2.bilateralFilter(img, 9, 75, 75)
        return result

def changePalette(img, value):
    if value == 0:
        result = img
        return result
    if value == 1:
        result = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
        return result
    if value == 2:
        result = cv2.cvtColor(img, cv2.COLOR_BGR2HSV_FULL)
        return result
    if value == 3:
        result = cv2.applyColorMap(img, cv2.COLORMAP_JET)
        return result

def greyScale(img, value):
    if value == 0:
        result = img
        return result
    if value == 1:
        result = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        return result

def binaryPicture(img, value, threshlord):
    if value == 1:
        ret, result = cv2.threshold(img, threshlord, 255, cv2.THRESH_BINARY)
        return result
    elif value == 0:
        result = img
        return result

def morfologyPicture(img, value, morftype):
    kernel = np.ones((5,5), np.uint8)
    if value == 1:
        if morftype == 0:
            result = cv2.erode(img, kernel, iterations=1)
            return result
        elif morftype == 1:
            result = cv2.dilate(img, kernel, iterations=1)
            return result
        elif morftype == 2:
            result = cv2.morphologyEx(img, cv2.MORPH_OPEN, kernel)
            return result
        elif morftype == 3:
            result = cv2.morphologyEx(img, cv2.MORPH_CLOSE, kernel)
            return result
    elif value == 0:
        return img

def revokeConnection():
    client.close()
    cap.release()
    cv2.destroyAllWindows()

client = socket.socket()
client.connect(("127.0.0.1", 13000))
fps = cap.get(cv2.CAP_PROP_FPS) # 10
frame_time = 1 / fps
while cap.isOpened():
    while True:
        readable, _, _ = select.select([client], [], [], 0)
        
        if client in readable:
            controlNow  = client.recv(curentBytesInfo)
    
            if len(controlNow) == 0:
                connectionLost = True
                break
            elif len(controlNow) >= 1 and len(controlNow) < 13:
                while len(controlNow) < curentBytesInfo:
                    part = client.recv(curentBytesInfo - len(controlNow))
                    if not part:
                        connectionLost = True
                        break
                    else:
                        controlNow += part
    
            if connectionLost:
                break
            
            mode = controlNow[0]
            willContinue = controlNow[1]
            rotPict = controlNow[2]
            flipHor = controlNow[3]
            flipVer = controlNow[4]
            baseSize = controlNow[5]
            baseDenoise = controlNow[6]
            baseColor = controlNow[7]
            baseGray = controlNow[8]
            isBinaryPic = controlNow[9]
            threshLevel = controlNow[10]
            isMorfolofy = controlNow[11]
            baseMorfology = controlNow[12]
        else:
            break

    if connectionLost:
        break
    
    startTime = time.perf_counter()

    ret, frame = cap.read()
    
    if not ret:
        break

    frame_complete = frame

    if willContinue == 0:
        break

    # змінює знаходження пікселя
    frame_complete = rotatePicture(frame_complete, rotPict)
    # змінює знаходження пікселя
    frame_complete = flipPictureHor(frame_complete, flipHor)
    # змінює знаходження пікселя
    frame_complete = flipPictureVer(frame_complete, flipVer)
    # змінює знаходження пікселя
    frame_complete = resizePicture(frame_complete, baseSize)
    if mode == 0:
        scores, classes, boxes = picDetection(frame_complete)
    # змінює піксель
    frame_complete = denoisePicture(frame_complete, baseDenoise)
    # змінює піксель
    frame_complete = changePalette(frame_complete, baseColor)
    # змінює піксель
    frame_complete = greyScale(frame_complete, baseGray)
    # змінює піксель
    frame_complete = binaryPicture(frame_complete, isBinaryPic, threshLevel)
    # змінює піксель
    frame_complete = morfologyPicture(frame_complete, isMorfolofy, baseMorfology)
    if mode == 0:
        frame_complete = picDrawRectangle(frame_complete, scores, classes, boxes)

    print(len(frame_complete.shape))

    cv2.imshow("frame", frame_complete)

    result, imgencode = cv2.imencode(".jpg", frame_complete)

    frame_bytes = imgencode.tobytes()

    frame_size = len(frame_bytes)
    size_bytes = frame_size.to_bytes(4, byteorder="big")

    client.sendall(size_bytes)
    client.sendall(frame_bytes)

    endTime = time.perf_counter()
    elapsed = endTime - startTime
    print(elapsed)
    if elapsed > 0 and elapsed < frame_time:
        time.sleep(frame_time - elapsed)
    else:
        continue

if not connectionLost:
    endByte = 0
    client.sendall(endByte.to_bytes(4, byteorder="big"))
    revokeConnection()
else:
    revokeConnection()