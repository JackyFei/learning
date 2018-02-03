import numpy as np

def cross_entropy(AL, Y):
    m = Y.shape[1]
    # Compute loss from aL and y.
    cost = -1/m * np.sum(np.dot(Y, np.log(AL).T) + np.dot((1 - Y), np.log(1 - AL.T)), axis = 1, keepdims = True)
    cost = np.squeeze(cost)
    return cost

def cross_entropy_d(AL, Y):
    AL = -(np.divide(Y, AL) - np.divide(1 - Y, 1 - AL))
    return AL
