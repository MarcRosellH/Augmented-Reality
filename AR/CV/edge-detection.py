import numpy as np
import cv2 as cv

def ScharrEdges():
    kr = [-3,0,3,-10,0,10,-3,0,3]
    krn = np.array(kr)
    krn = krn.reshape(3,3)
    return krn

def SobelEdges():
    kr = [-1,0,1,-2,0,2,-1,0,1]
    krn = np.array(kr)
    krn = krn.reshape(3,3)
    return krn

def EdgeDetection(img, krn):
    # kernel
    ksize, _ = krn.shape
    krad = int(ksize/2)

    # frame
    height, width = img.shape
    framed = np.zeros((height + 2*krad, width + 2*krad))
    framed[krad:-krad,krad:-krad] = img

    # filter
    output1 = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output1[i, j] = (framed[i:i+ksize, j:j+ksize] * krn[:,:]).sum(axis=(0, 1))
    output = np.zeros(shape=img.shape)
    krn = krn.transpose()
    output2 = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output2[i, j] = (framed[i:i+ksize, j:j+ksize] * krn[:,:]).sum(axis=(0, 1))
    output[:,:] = np.sqrt(output1[:,:]**2 + output2[:,:]**2)

    output /= output.max()
    
    return output

def main():
    img = cv.imread('marvel.png',cv.IMREAD_GRAYSCALE)
    img = img/255.0
    filtered = EdgeDetection(img, ScharrEdges())
    cv.imshow("Original",img)
    cv.imshow("Filtered",filtered)
    cv.waitKey(0)

main()