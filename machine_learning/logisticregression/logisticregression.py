import numpy as np
import activationfunction as af

class LogisticRegression:
    def __init__(self, X, Y):
        self.name = "logistic regression"
        self.X = X
        self.Y = Y
        self.w = np.zeros([X.shape[0], 1])
        self.b = 0
        
    def __propagate(self):
        """
        Implement the cost function and its gradient for the propagation explained above

        Arguments:
        w -- weights, a numpy array of size (num_px * num_px * 3, 1)
        b -- bias, a scalar
        X -- data of size (num_px * num_px * 3, number of examples)
        Y -- true "label" vector (containing 0 if non-cat, 1 if cat) of size (1, number of examples)

        Return:
        cost -- negative log-likelihood cost for logistic regression
        dw -- gradient of the loss with respect to w, thus same shape as w
        db -- gradient of the loss with respect to b, thus same shape as b
    
        Tips:
        - Write your code step by step for the propagation. np.log(), np.dot()
        """
        m = self.X.shape[1]
        # FORWARD PROPAGATION (FROM X TO COST)
        Z = np.dot(self.w.T, self.X) + self.b
        A = af.sigmoid(Z)
        cost = -1/m * np.sum(self.Y * np.log(A) + (1 - self.Y) * np.log(1 - A), axis = 1, keepdims = True)

        # BACKWARD PROPAGATION (TO FIND GRAD)
        dw = np.dot(self.X, (A - self.Y).T)/m
        db = 1/m * np.sum(A - self.Y, axis = 1, keepdims = True)

        assert(dw.shape == self.w.shape)
        assert(db.dtype == float)
        cost = np.squeeze(cost)
        assert(cost.shape == ())
    
        grads = {"dw": dw,
                 "db": db}
    
        return grads, cost

    def optimize(self, num_iterations, learning_rate, print_cost = False):
        """
        This function optimizes w and b by running a gradient descent algorithm
    
        Arguments:
        w -- weights, a numpy array of size (num_px * num_px * 3, 1)
        b -- bias, a scalar
        X -- data of shape (num_px * num_px * 3, number of examples)
        Y -- true "label" vector (containing 0 if non-cat, 1 if cat), of shape (1, number of examples)
        num_iterations -- number of iterations of the optimization loop
        learning_rate -- learning rate of the gradient descent update rule
        print_cost -- True to print the loss every 100 steps
    
        Returns:
        params -- dictionary containing the weights w and bias b
        grads -- dictionary containing the gradients of the weights and bias with respect to the cost function
        costs -- list of all the costs computed during the optimization, this will be used to plot the learning curve.
    
        Tips:
        You basically need to write down two steps and iterate through them:
            1) Calculate the cost and the gradient for the current parameters. Use propagate().
            2) Update the parameters using gradient descent rule for w and b.
        """
        costs = []
    
        for i in range(num_iterations): 
            # Cost and gradient calculation
            grads, cost = self.__propagate()
        
            # Retrieve derivatives from grads
            dw = grads["dw"]
            db = grads["db"]
        
            # update rule
            self.w = self.w - learning_rate * dw
            self.b = self.b - learning_rate * db
        
            # Record the costs
            costs.append(cost)
        
            # Print the cost every 100 training examples
            if print_cost and i % 10 == 0:
                print ("Cost after iteration %i: %f" %(i, cost))
    
        return costs

    def predict(self, X):
        '''
        Predict whether the label is 0 or 1 using learned logistic regression parameters (w, b)
    
        Arguments:
        w -- weights, a numpy array of size (num_px * num_px * 3, 1)
        b -- bias, a scalar
        X -- data of size (num_px * num_px * 3, number of examples)
    
        Returns:
        Y_prediction -- a numpy array (vector) containing all predictions (0/1) for the examples in X
        '''
    
        m = X.shape[1]
        Y_prediction = np.zeros((1,m))
        w = self.w.reshape(X.shape[0], 1)
        b = self.b
    
        # Compute vector "A" predicting the probabilities of a cat being present in the picture
        A = np.dot(w.T, X) + b
    
        for i in range(A.shape[1]):       
            # Convert probabilities A[0,i] to actual predictions p[0,i]
            if A[0, i] > 0.5:
                Y_prediction[0, i] = 1
            else:
                Y_prediction[0, i] = 0
    
        assert(Y_prediction.shape == (1, m))
    
        return Y_prediction

def logistic_regression_model(X_train, Y_train, X_test, Y_test, num_iterations = 2000, learning_rate = 0.5, print_cost = False):
    """
    Create the logistic regresstion class and optimize the w, b.
    """
    
    # create LogisticRegression class instance.
    lr = LogisticRegression(X_train, Y_train)
    # optimize the parameters
    costs = lr.optimize(num_iterations, learning_rate, print_cost)
    # predict test/train set examples.
    Y_prediction_test = lr.predict(X_test)
    Y_prediction_train = lr.predict(X_train)

    # print train/test Errors
    print("train accuracy: {} %".format(100 - np.mean(np.abs(Y_prediction_train - Y_train)) * 100))
    print("test accuracy: {} %".format(100 - np.mean(np.abs(Y_prediction_test - Y_test)) * 100))