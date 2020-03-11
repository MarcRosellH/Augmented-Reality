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

def gaussianKrnFilter(img, krad):
    # normalize
    img=img/255.0

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

def convolvebilateral(img, krn):
    # kernel
    ksize, _ = krn.shape
    krad = int(ksize/2)

    # copy image in intensity
    img2 = cv.cvtColor(img, cv.COLOR_BGR2GRAY)

    # normalize
    img=img/255.0

    # frame
    height, width, depth = img.shape
    framed2 = np.ones((height + 2*krad, width + 2*krad))
    framed = np.ones((height + 2*krad, width + 2*krad, depth))
    framed2[krad:-krad,krad:-krad] = img2
    framed[krad:-krad,krad:-krad] = img

    # filter
    output = np.zeros(img.shape)
    for i in range(0, height):
        for j in range(0, width):
            test = np.zeros((ksize,ksize,3))
            test[:] = framed2[i,j]
            intensityDelta = abs(test-framed2[i:i+ksize,j:j+ksize,np.newaxis])/(0.5*(1/3)**2)
            output[i, j] = (framed[i:i+ksize, j:j+ksize] * krn[:,:,np.newaxis] * intensityDelta).sum(axis=(0, 1))
    
    return output/output.max()

def bilateralFilter(img, krad):

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
    output = convolvebilateral(img,krn)

    return output


def main():
    img = cv.imread('marvel.png',cv.IMREAD_COLOR)
    krad = 5
    #filtered = gaussianKrnFilter(img, krad)
    filtered = bilateralFilter(img, krad)
    cv.imshow("Original",img)
    cv.imshow("Filtered",filtered)
    cv.waitKey(0)

main()