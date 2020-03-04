import numpy as np
import cv2 as cv

def convolve(img, krn):
    # kernel
    ksize, _ = krn.shape
    krad = int(ksize/2)

    # frame
    height, width, depth = img.shape
    framed = np.ones((height + 2*krad, width + 2*krad, depth))
    framed[krad:-krad,krad:-krad] = img

    # filter
    output = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            output[i, j] = (framed[i:i+ksize, j:j+ksize] * krn[:,:, np.newaxis]).sum(axis=(0, 1))
    
    return output

def boxFilter(img):
    # normalize
    img=img/255.0

    # kernel
    ksize = 11
    krn = np.zeros((ksize, ksize))
    krn[:,:] = 1.0/(ksize*ksize)
    
    # filter
    output = convolve(img,krn)

    return output

def main():
    img = cv.imread('marvel.png',cv.IMREAD_COLOR)
    filtered = boxFilter(img)
    cv.imshow("Original",img)
    cv.imshow("Filtered",filtered)
    cv.waitKey(0)

main()