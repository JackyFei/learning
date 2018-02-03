"""
utils of activation functions
"""

import numpy as np


def sigmoid(x):
    """
    Compute the sigmoid of x

    Arguments:
    x -- A scalar or numpy array of any size

    Return:
    s -- sigmoid(x)
    """
    s = 1 / (1 + np.exp(-x))
    return s


def sigmoid_d(x):
    """
    Compute the gradient (also called the slope or derivative) of the sigmoid function with respect to its input x.
    You can store the output of the sigmoid function into variables and then use it to calculate the gradient.
    
    Arguments:
    x -- A scalar or numpy array

    Return:
    ds -- Your computed gradient.
    """
    s = sigmoid(x)
    ds = s * (1 - s)
    return ds


def tanh(x):
    """
    Compute the tanh of x

    Arguments:
    x -- A scalar or numpy array of any size

    Return:
    t -- tanh(x)
    """
    t = np.divide(np.exp(x) - np.exp(-x), np.exp(x) + np.exp(-x))
    return t


def tanh_d(x):
    """
    Compute the gradient (also called the slope or derivative) of the tanh function with respect to its input x.
    You can store the output of the tanh function into variables and then use it to calculate the gradient.
    
    Arguments:
    x -- A scalar or numpy array

    Return:
    dt -- Your computed gradient.
    """
    t = tanh(x)
    dt = 1 - np.power(t, 2)
    return dt


def relu(x):
    """
    Compute the relu of x.

    Arguments:
    x -- A scalar or numpy array of any size

    Return:
    r -- relu(x)
    """

    r = np.maximum(0, x)
    return r


def relu_d(x):
    """
    Compute the gradient (also called the slope or derivative) of the relu function with respect to its input x.
    You can store the output of the relu function into variables and then use it to calculate the gradient.
    
    Arguments:
    x -- A scalar or numpy array

    Return:
    dr -- Your computed gradient.
    """
    r = relu(x)
    dr = (x < 0) * 0 + (x >= 0) * 1
    return dr


def leakyrelu(x):
    """
    Compute the leaky relu of x.

    Arguments:
    x -- A scalar or numpy array of any size

    Return:
    r -- leakyrelu(x)
    """

    r = np.maximum(0.01 * x, x)
    return r


def leakyrelu_d(x):
    """
    Compute the gradient (also called the slope or derivative) of the leaky relu function with respect to its input x.
    You can store the output of the leaky relu function into variables and then use it to calculate the gradient.
    
    Arguments:
    x -- A scalar or numpy array

    Return:
    dr -- Your computed gradient.
    """
    r = leakyrelu(x)
    dr = (x < 0) * 0.01 + (x >= 0) * 1
    return dr

def activate(x, af_name = "relu"):
    if af_name == "relu":
        return relu(x)
    elif af_name == "sigmoid":
        return sigmoid(x)
    elif af_name == "tanh":
        return tanh(x)
    elif af_name == "leakyrelu":
        return leakyrelu(x)
    else:
        raise Exception("not supported activation function.")

def activate_d(x, af_name = "relu"):
    if af_name == "relu":
        return relu_d(x)
    elif af_name == "sigmoid":
        return sigmoid_d(x)
    elif af_name == "tanh":
        return tanh_d(x)
    elif af_name == "leakyrelu":
        return leakyrelu_d(x)
    else:
        raise Exception("not supported activation function.")

