import cv2
import numpy as np
import os

def cls():
    os.system('cls' if os.name=='nt' else 'clear')

# now, to clear the screen
cls()

##----- Template Matching Project -----##
## By Pol Recasens And Marc Rosell

# input manager function
def input_manager():
    # detect input
    img = input("Input Image: ")
    target = input("\nTarget Image: ")
    trs = input("\nDetection Threshold: ")
    threshold = float(trs)
    return threshold, img, target
# ----------------------

# template matching algorithm
def template_matching(img, target, threshold, original):

    #resize
    scale_percent = 50
    wid = int(img.shape[1] * scale_percent / 100)
    hei = int(img.shape[0] * scale_percent / 100)
    wi = int(target.shape[1] * scale_percent / 100)
    he = int(target.shape[0] * scale_percent / 100)
    dim = (wid, hei)
    di = (wi, he)
    ia = cv2.resize(img, dim)
    ta = cv2.resize(target, di)

    # get img and target shape
    height, width = ia.shape
    h, w = ta.shape

    # create matching map
    hmap = (height - h) + 1
    wmap = (width - w) + 1

    matching_map = np.zeros((hmap, wmap),float)

    # loop
    for i in range(0, hmap):
        for j in range(0, wmap):
            # metric
            matching_map[i, j] = ((ta[:, :] - ia[i: i + h,j: j + w])**2).sum()
    
    # show matching map
    matching_map /= matching_map.max()
    cv2.imshow("Matching Map", matching_map)

    # check varaible
    isFound = False

    print("Looking for matches...\n")
    # draw quads and check
    for i in range(0, hmap):
        for j in range(0, wmap):
            if matching_map[i, j] < threshold:
                # all the *2 numbers need to be changed if the scaling needs to be changed too
                x = j*2 + (w)*2 -1
                y = i*2 + (h)*2 -1
                isFound = True
                cv2.rectangle(original, (j*2, i*2), (x, y),(0, 255, 0), 0)

    return original, isFound
# ---------------------------

def warn():

    # needed warning
    print("--------------------------------------------------")
    print("- Python Test Program: Template Matching Project -")
    print("---------|| Pol Recasens & Marc Rosell ||---------")
    print("--------------------------------------------------")
    print("Warning Message: Threshold for img1 and t1-img1, to detect a match is **0.38**")
    print("Img2 works with 0.1 threshold")
    print("To check the work done, input is needed!\n")
    input("Press any key to continue...")
    cls()


# main function
def main():

    warn()

    # presentation and credits
    print("--------------------------------------------------")
    print("- Python Test Program: Template Matching Project -")
    print("---------|| Pol Recasens & Marc Rosell ||---------")
    print("--------------------------------------------------\n")

    # 0.38

    # call input function
    threshold, path, second_path = input_manager()

    print("\nLoading Images...")
    # load all images in variables
    img = cv2.imread(path, cv2.IMREAD_COLOR)
    target = cv2.imread(second_path, cv2.IMREAD_COLOR)

    print("Grayscale Transformation...")
    # transform to grayscale
    img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    target_gray = cv2.cvtColor(target, cv2.COLOR_BGR2GRAY)

    print("Template Match Algorithm...")
    # call matching algorithm
    final, isFound = template_matching(img_gray, target_gray, threshold, img)

    print("Output:")
    # show original image and target
    cv2.imshow("Input Image", img)
    cv2.imshow("Target", target)

    # text preparation
    foundText = np.zeros((40,342,3), np.uint8)
    font = cv2.FONT_HERSHEY_SIMPLEX

    if isFound:
        cv2.imshow("Input Image", final)
        cv2.putText(foundText, "TARGET FOUND", (5, 30), font, 1, (0, 255, 0), 2)
        print("TARGET FOUND\n")
    else:
        cv2.imshow("Input Image", img)
        cv2.putText(foundText, "TARGET NOT FOUND", (5, 30), font, 1, (0, 255, 0), 2)
        print("TARGET NOT FOUND\n")

    cv2.imshow("Result", foundText)
    cv2.waitKey(0)
# -------------

main()