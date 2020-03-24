import cv2
import numpy as np

##----- Template Matching Project -----##
## By Pol Recasens And Marc Rosell

# input manager function
def input_manager():
    # detect input
    img = input("Input Image: ")
    target = input("Target Image: ")
    trs = input("Detection Threshold: ")
    threshold = float(trs)
    return threshold, img, target
# ----------------------

# template matching algorithm
def template_matching(img, target, threshold, original):
    # get img and target shape
    height, width = img.shape
    h, w = target.shape 

    # create matching map
    matching_map = np.zeros((height - h + 1, width - w + 1))

    hmap = (height - h) + 1
    wmap = (width - w) + 1

    # loop
    for i in range(0, hmap):
        for j in range(0, wmap):
            # metric
            matching_map[i, j] = ((target[:, :] - img[i: i + h,j: j + w])**2).sum()
    
    # show matching map
    cv2.imshow("Matching Map", matching_map)
    matching_map /= matching_map.max()

    # check varaible
    isFound = False

    # draw quads and check
    for i in range(0, hmap):
        for j in range(0, wmap):
            if matching_map[i, j] > threshold:
                isFound = True
                cv2.rectangle(original, (i, j), (i + h, j + w),(0, 255, 0), 1, 8, 0)

    return original, isFound
# ---------------------------

# main function
def main():
    # presentation and credits
    print("Python Test Program: Template Matching Project")
    print("By Pol Recasens and Marc Rosell")

    # call input function
    threshold, path, second_path = input_manager()

    # load all images in variables
    img = cv2.imread(path, cv2.IMREAD_COLOR)
    target = cv2.imread(second_path, cv2.IMREAD_COLOR)

    # transform to grayscale
    img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    target_gray = cv2.cvtColor(target, cv2.COLOR_BGR2GRAY)

    # call matching algorithm
    final, isFound = template_matching(img_gray, target_gray, threshold, img)

    # show original image and target
    cv2.imshow("Input Image", img)
    cv2.imshow("Target", target)

    # text preparation
    foundText = np.zeros((40,245,3), np.uint8)
    font = cv2.FONT_HERSHEY_SIMPLEX

    if isFound:
        cv2.imshow("Input Image", final)
        cv2.putText(foundText, "TARGET FOUND", (5, 30), font, 1, (0, 255, 0), 2)
    else:
        cv2.imshow("Input Image", img)
        cv2.putText(foundText, "TARGET NOT FOUND", (5, 30), font, 1, (0, 255, 0), 2)

    cv2.imshow("Result", foundText)
# -------------

main()