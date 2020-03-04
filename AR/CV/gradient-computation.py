import numpy as np
import cv2 as cv

def edgesconvolve(img,krn):
    # kernel
    ksize, _ = krn.shape
    krad = int(ksize/2)

    # frame
    height, width, depth = img.shape
    framed = np.ones((height + 2*krad, width + 2*krad, depth))
    framed[krad:-krad,krad:-krad] = img

    # filter
    output1 = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output1[i, j] = (framed[i:i+ksize, j:j+ksize] * krn[:,:, np.newaxis]).sum(axis=(0, 1))
    output = np.zeros(shape=img.shape)
    krn = krn.transpose()
    output2 = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output2[i, j] = (framed[i:i+ksize, j:j+ksize] * krn[:,:, np.newaxis]).sum(axis=(0, 1))
    output[:,:] = np.sqrt(output1[:,:]**2 + output2[:,:]**2)
    
    return output

def trueEdges(img,krn):
        # kernel
    ksize, _ = krn.shape
    krad = int(ksize/2)

    # frame
    height, width = img.shape
    framed = np.ones((height + 2*krad, width + 2*krad))
    framed[krad:-krad,krad:-krad] = img

    # filter
    output1 = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output1[i, j] = (framed[i:i+ksize, j:j+ksize]).sum(axis=(0, 1))
    output = np.ones(shape=img.shape)
    krn = krn.transpose()
    output2 = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output2[i, j] = (framed[i:i+ksize, j:j+ksize]).sum(axis=(0, 1))
    output[:,:] = np.sqrt(output1[:,:]**2 + output2[:,:]**2)
    
    return output

def EdgeDetection(img,n):
    img = img/255.0
    kr = [-1,0,1,-2,0,2,-1,0,1]
    krn = np.array(kr)
    krn = krn.reshape(3,3)
    if n==1:
        output = edgesconvolve(img,krn)
    if n==2:
        output = trueEdges(img,krn)

    return output

def main():
    img = cv.imread('marvel.png',cv.IMREAD_COLOR)
    filtered = EdgeDetection(img,1)
    #filtered = EdgeDetection(img,2)
    cv.imshow("Original",img)
    cv.imshow("Filtered",filtered)
    cv.waitKey(0)

main()