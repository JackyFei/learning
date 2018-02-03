import numpy as np
import h5py
import matplotlib.pyplot as plt

%matplotlib inline

def load_data():
    train_dataset = h5py.File('C:/Dev_WS/TS/Learning/machine learning/nn/datasets/train_catvnoncat.h5', "r")
    train_set_x_orig = np.array(train_dataset["train_set_x"][:]) # your train set features
    train_set_y_orig = np.array(train_dataset["train_set_y"][:]) # your train set labels

    test_dataset = h5py.File('C:/Dev_WS/TS/Learning/machine learning/nn/datasets/test_catvnoncat.h5', "r")
    test_set_x_orig = np.array(test_dataset["test_set_x"][:]) # your test set features
    test_set_y_orig = np.array(test_dataset["test_set_y"][:]) # your test set labels

    classes = np.array(test_dataset["list_classes"][:]) # the list of classes
    
    train_set_y_orig = train_set_y_orig.reshape((1, train_set_y_orig.shape[0]))
    test_set_y_orig = test_set_y_orig.reshape((1, test_set_y_orig.shape[0]))
    
    return train_set_x_orig, train_set_y_orig, test_set_x_orig, test_set_y_orig, classes

X_train = np.array([1,0,1,1,0,0,0,1]).reshape(4,2).T
Y_train = np.array([0,1,0,0]).reshape(1,4)
X_test = np.array([1,1,1,0,0,1]).reshape(3,2).T
Y_test = np.array([1,0,0]).reshape(1,3)

print(X_train, Y_train)

layer_dims = [(2,"relu"),(5,"relu"),(1,"sigmoid")]

train_x_orig, train_y, test_x_orig, test_y, classes = load_data()

index = 10
plt.imshow(train_x_orig[index])
print ("y = " + str(train_y[0,index]) + ". It's a " + classes[train_y[0,index]].decode("utf-8") +  " picture.")

#dnn.model(X_train, Y_train, X_test, Y_test, layer_dims, 0.5, 1000,False)

