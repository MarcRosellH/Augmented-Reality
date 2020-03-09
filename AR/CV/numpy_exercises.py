import numpy as np
import cv2 as cv

def ex16():
    arr = np.random.seed(2)
    arr = np.random.randint(0,10,(3,3))
    print(len(arr[arr>5]))

def ex17():
    arr = np.zeros(shape=(64,64))
    arr2 = np.arange(0.0,1.0,(1.0/64.0))
    arr +=arr2
    print(arr2)
    cv.imshow("window",arr)
    cv.waitKey(0)

#ex17()

def ex18():
    arr = np.zeros(shape=(64,64))
    arr2 = np.arange(0.0,1.0,(1.0/64.0))
    arr2 = arr2.reshape(64,1)
    arr +=arr2
    print(arr2)
    cv.imshow("window",arr)
    cv.waitKey(0)

#ex18()

def ex19():
    arr = np.ones(shape=(64,64,3))
    arr[:,:,2] = 0.0
    cv.imshow("window",arr)
    cv.waitKey(0)

#ex19()

def ex20():
    arr = np.ones(shape=(64,64,3))
    cv.imshow("window",arr)
    cv.waitKey(0)

#ex20()

def test():
    arr = np.ones((3,3))
    arr2 = np.ones((3,3))
    arr2[:] = 2
    arr3 = arr * arr2
    print(arr3)

test()