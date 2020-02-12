import cv2 as cv
import numpy

# Load an image in grayscale
def ImageChange():
    img = cv.imread('marvel.png',0)
    cv.imshow('Marvel',img)

    k = cv.waitKey(0)

    if k == 27:
        cv.destroyAllWindows()
    elif k == ord('s'):
        cv.imwrite('marvel-grayscale.png',img)
        cv.destroyAllWindows()

# Capture Video, flip it and save it
def SaveVideo():
    cap = cv.VideoCapture(0)
    fourcc = cv.VideoWriter_fourcc(*'XVID')
    out = cv.VideoWriter('Output.avi',fourcc,20.0,(640,480))

    while(cap.isOpened()):
        ret, frame = cap.read()
        if ret == True:
            frame = cv.cvtColor(frame,cv.COLOR_BGR2GRAY)
            frame = cv.cvtColor(frame,cv.COLOR_GRAY2BGR)
            frame = cv.flip(frame,1)
            out.write(frame)
            cv.imshow('VideoFrame',frame)
            if cv.waitKey(1) & 0xFF == ord('q'):
                break
        else:
            break
    cap.release()
    out.release()
    cv.destroyAllWindows()

# ImageChange()
SaveVideo()