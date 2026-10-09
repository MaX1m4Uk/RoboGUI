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
faceCascade = cv2.CascadeClassifier(r"D:\Global Env\openCV\Lib\site-packages\cv2\data\haarcascade_frontalface_default.xml")

cap = cv2.VideoCapture(r"C:\Users\max\Downloads\ssstik.io_@machfromnyc_1791540057821.mp4")
connectionLost = False
curentBytesInfo = 14
mode = 0
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
baseColorPreset = 0

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

colorsList = [ 
    (()), 
    ((np.array([0, 100, 70]), np.array([17, 255, 255])), #red
    (np.array([165, 100, 70]), np.array([180, 255, 255]))), #red
    [(np.array([17, 100, 70]), np.array([35, 255, 255]))], #yellow
    [(np.array([35, 100, 70]), np.array([75, 255, 255]))], #green
    [(np.array([75, 100, 70]), np.array([105, 255, 255]))], #cyan
    [(np.array([105, 100, 70]), np.array([135, 255, 255]))], #blue
    [(np.array([135, 100, 70]), np.array([165, 255, 255]))], #purple
]

colorsListString = [
    ("None"),
    ("Red"),
    ("Yellow"),
    ("Green"),
    ("Cyan"),
    ("Blue"),
    ("Purple")
]

def picColorDetection(img, colorNow):
    contourLists = []
    img = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
    mask = np.zeros(img.shape[:2], dtype="uint8")
    currentList = colorsList[colorNow]
    for limitMin, limitMax in currentList:
        maskColor = cv2.inRange(img, limitMin, limitMax)
        mask = cv2.bitwise_or(mask, maskColor)
    contours, hierarchy = cv2.findContours(mask, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    for _, cnt in enumerate(contours):
        area = cv2.contourArea(cnt)
        if area > 100:
            M = cv2.moments(cnt)
            if M['m00'] != 0:
                cx = int(M['m10']/M['m00'])
                cy = int(M['m01']/M['m00'])
                h,s,v = img[cy,cx,:3]
                contourLists.append((cnt, area, (cx, cy), h, s, v))
    return contourLists

def picColorDetectionDrawRectangle(img, contourLists, colorNow):
    if len(img.shape) != 3:
        img = cv2.cvtColor(img, cv2.COLOR_GRAY2BGR) 
    for cnt, area, (cx, cy), h, s, v in contourLists:
        cv2.drawContours(img, [cnt], -1, (97, 46, 176))
        cv2.circle(img, (cx, cy), 7, (97, 46, 176), -1)
        cv2.putText(img, f"{colorsListString[colorNow]} CX: {cx} CY: {cy} Area: {area} H: {h} S: {s} V: {v}", (cx - 20, cy - 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (145, 176, 195), 2) # в іншу функцію
    return img

def faceDetection(img):
    img_copy = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    face_rect = faceCascade.detectMultiScale(img_copy, scaleFactor=1.2, minNeighbors=5)
    return face_rect

def faceDetectionDrawRectangle(img, face_rect):
    if len(img.shape) != 3:
        img = cv2.cvtColor(img, cv2.COLOR_GRAY2BGR) 
    for x, y, w, h in face_rect:
        img = cv2.rectangle(img, (x, y), (x + w, y + h), color=(0,255,255), thickness=1)
    return img

client = socket.socket()
client.connect(("127.0.0.1", 13000))
fps = 5 # cap.get(cv2.CAP_PROP_FPS) # 10
frame_time = 1 / fps
while cap.isOpened():
    while True:
        readable, _, _ = select.select([client], [], [], 0)
        
        if client in readable:
            controlNow  = client.recv(curentBytesInfo)
    
            if len(controlNow) == 0:
                connectionLost = True
                break
            elif len(controlNow) >= 1 and len(controlNow) < curentBytesInfo:
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
            baseColorPreset = controlNow[13]
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
    if mode == 1:
        scores, classes, boxes = picDetection(frame_complete)
    elif mode == 2:
        face_rect = faceDetection(frame_complete)
    elif mode == 3:
        color_detect = picColorDetection(frame_complete, baseColorPreset)
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
    if mode == 1:
        frame_complete = picDrawRectangle(frame_complete, scores, classes, boxes)
    elif mode == 2:
        frame_complete = faceDetectionDrawRectangle(frame_complete, face_rect)
    elif mode == 3:
        frame_complete = picColorDetectionDrawRectangle(frame_complete, color_detect, baseColorPreset)

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