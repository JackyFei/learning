import numpy as np
import activationfunction as af

"""
One hidden layer neural network.
"""
class SimplyNeuralNetwork:
    def __init__(self, X, Y, hidden_layer_size=5):
        """
        initialize parameters
        n_x: size of input layer
        n_h: size of hidden layer
        n_y: size of output layer
        """
        self.name = "simply neural network"
        self.X = X
        self.Y = Y
        self.n_x = X.shape[0]
        self.n_h = hidden_layer_size
        self.n_y = Y.shape[0]
        self.__initialize_parameters()

    def __initialize_parameters(self):
        """
        Argument:
        n_x -- size of the input layer
        n_h -- size of the hidden layer
        n_y -- size of the output layer
    
        Returns:
        params -- python dictionary containing your parameters:
                        W1 -- weight matrix of shape (n_h, n_x)
                        b1 -- bias vector of shape (n_h, 1)
                        W2 -- weight matrix of shape (n_y, n_h)
                        b2 -- bias vector of shape (n_y, 1)
        """
    
        self.W1 = np.random.randn(self.n_h, self.n_x) * 0.01
        self.b1 = np.zeros((self.n_h, 1))
        self.W2 = np.random.randn(self.n_y, self.n_h) * 0.01
        self.b2 = np.zeros((self.n_y, 1))

        assert (self.W1.shape == (self.n_h, self.n_x))
        assert (self.b1.shape == (self.n_h, 1))
        assert (self.W2.shape == (self.n_y, self.n_h))
        assert (self.b2.shape == (self.n_y, 1))

    def __forward_propagation(self, X):
        Z1 = np.dot(self.W1, X) + self.b1
        A1 = af.tanh(Z1)
        Z2 = np.dot(self.W2, A1) + self.b2
        A2 = af.sigmoid(Z2)
        cache = {"Z1":Z1,
                 "A1":A1,
                 "Z2":Z2,
                 "A2":A2}
        return A2, cache

    def __compute_cost(self, YHat):
        m = self.Y.shape[1]
        logprobs = -np.multiply(Y, np.log(YHat)) - np.multiply(1 - Y, np.log(1 - YHat))
        cost = np.sum(logprobs) / m
        cost = np.squeeze(cost)
        return cost

    def __backward_propagation(self, cache):
        A1 = cache["A1"]
        A2 = cache["A2"]
        dZ2 = A2 - self.Y
        dW2 = np.dot(dZ2, A1.T) / m
        db2 = np.sum(dZ2, axis = 1, keepdims = True) / m
        dA1 = np.dot(self.W2.T, dZ2)
        dZ1 = dA1 * af.tanh_d(A1)
        dW1 = np.dot(dZ1, self.X.T) / m
        db1 = np.sum(dZ1, axis = 1, keepdims = True) / m
    
        grads = {"dW1": dW1,
                 "db1": db1,
                 "dW2": dW2,
                 "db2": db2}
    
        return grads

    def __update_parameters(self, grads, learning_rate):
        dw1 = grads["dw1"]
        dw2 = grads["dw2"]
        db1 = grads["db1"]
        db2 = grads["db2"]
        self.w1 = self.w1 - dw1 * learning_rate
        self.db1 = self.db1 - db1 * learning_rate
        self.w2 = self.w2 - dw2 * learning_rate
        self.db2 = self.db2 - db2 * learning_rate

    def optimize(self, learning_rate=0.5, num_iterations=10000, print_cost=False):
        costs = []
        for i in range(0, num_iterations):
            A2, cache = __forward_propagation(self.X)
            cost = __compute_cost(A2)
            grads = __backward_propagation(cache)
            __update_parameters(grads, learning_rate)
            costs.append(cost)
            if print_cost and i % 100 == 0:
                print("Cost after iteration %i: %f" % (i, cost))
        return costs

    def predict(X):
        # Computes probabilities using forward propagation, and classifies to
        # 0/1 using 0.5 as the threshold.
        A2, _ = __forward_propagation(X)
        predictions = A2 > 0.5   
        return predictions

def model(X_traing, Y_traing, X_test, Y_test, hidden_layer_size, learning_rate=0.5, num_iterations=10000, print_cost=False):
    nn = SimplyNeuralNetwork(X_train, Y_traing, hidden_layer_size)
    costs = nn.optimize(learning_rate, num_iterations, print_cost)
    Y_prediction_test = nn.predict(X_test)
    Y_prediction_train = nn.predict(X_train)

    # print train/test Errors
    print("train accuracy: {} %".format(100 - np.mean(np.abs(Y_prediction_train - Y_train)) * 100))
    print("test accuracy: {} %".format(100 - np.mean(np.abs(Y_prediction_test - Y_test)) * 100))