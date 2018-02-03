import numpy as np
import logisticregression as lr
X = np.array([1,0,1,1,0,0,0,1]).reshape(4,2).T
Y = np.array([0,1,0,0])
X_test = np.array([1,1,1,0,0,1]).reshape(3,2).T
Y_test = np.array([1,0,0])
lr.logistic_regression_model(X,Y,X_test,Y_test,num_iterations=500,print_cost=True)
