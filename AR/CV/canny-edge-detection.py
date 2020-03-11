import numpy as np
import cv2 as cv

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

def convolve(img, krn):
    # kernel
    ksize, _ = krn.shape
    krad = int(ksize/2)

    # frame
    height, width= img.shape
    framed = np.ones((height + 2*krad, width + 2*krad))
    framed[krad:-krad,krad:-krad] = img

    # filter
    output = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output[i, j] = (framed[i:i+ksize, j:j+ksize] * krn[:,:]).sum(axis=(0, 1))
    
    return output

def gaussianKrnFilter(img, krad):
    # define ksize
    sigma = krad/3
    ksize = krad*2 + 1
    krn = np.zeros((ksize,ksize))
    for i in range(0, ksize):
        for j in range(0, ksize):
            d = np.sqrt((krad - i)**2 + (krad - j)**2)
            krn[i, j] = np.exp(-d**2 / (2*sigma**2))

    krn /= krn.sum()

    # filter
    output = convolve(img,krn)

    return output

def GradientDirection(img, krn):
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
    
    output[:,:] = np.arctan2(output2[:,:],output1[:,:])
    output *= (180.0/np.pi)
    h, w = output.shape
    output[output < 0] += 180
    for i in range(0,h):
        for j in range(0,w):
            if output[i,j] < 45.0/2.0 and output[i,j] >= 0 or output[i,j] > 135.0+(45.0/2.0) and output[i,j] <= 180.0:
                output[i,j] = 0
            if output[i,j] > 45.0/2.0 and output[i,j] < 90.0-(45.0/2.0):
                output[i,j] = 45
            if output[i,j] > 90.0-(45.0/2.0) and output[i,j] < 90.0+(45.0/2.0):
                output[i,j] = 90
            if output[i,j] > 90.0+(45.0/2.0) and output[i,j] < 135.0+(45.0/2.0):
                output[i,j] = 135

    print(output)
    
    return output

def NonMaximumSupression(edged, directional):
    height, width = edged.shape
    output = np.zeros((height+2,width+2))
    output[1:-1,1:-1] = edged
    for i in range(0,height):
        for j in range(0,width):
            if directional[i,j] = 0:
                if edged[i,j] < edged[i]
            if directional[i,j] = 45:

            if directional[i,j] = 90:

            if directional[i,j] = 135:
    return output

def main():
    # load image
    img = cv.imread('marvel.png',cv.IMREAD_GRAYSCALE)
    # normalize
    img = img/255.0
    # gaussian blur
    first = gaussianKrnFilter(img, 2)
    # sobel EdgeDetection
    second = EdgeDetection(first,SobelEdges())
    # gradient direction
    third = GradientDirection(first,SobelEdges())
    # Edge Thinning
    fourth = NonMaximumSupression(second, third)
    cv.imshow("Original",img)
    #cv.imshow("Filtered",second)
    cv.waitKey(0)

main()