"""
deep neural network
"""
import numpy as np
import activationfunction as af
import costfunction as cf

class DeepNeuralNetwork:
    """
    X: training examples, inputs
    Y: training examples, outputs
    layer_dims:[(5,"relu"), (3,"relu"), (1,"sigmoid")]
    input layer==>hidden layer=>hidden layer=>xxx=>output layer
    """
    def __init__(self, X, Y, layer_dims):
        self.X = X
        self.Y = Y
        self.layer_dims = layer_dims
        self.parameters = self.__initialize_parameters()

    def __initialize_parameters(self):
        parameters = {}
        L = len(self.layer_dims)
        for l in range(1, L):
            current_layer_size, _ = self.layer_dims[l]
            previous_layer_size, _ = self.layer_dims[l-1]
            parameters["W" + str(l)] = np.random.randn(current_layer_size, previous_layer_size) * 0.01
            parameters["b" + str(l)] = np.zeros((current_layer_size, 1))
        return parameters

    def __forward_propagation(self, X):
        L = len(self.layer_dims)
        cache = {}
        A = X
        for l in range(1, L):
            _, af_name = self.layer_dims[l]
            A_prev = A
            Z = np.dot(self.parameters["W" + str(l)], A) + self.parameters["b" + str(l)]
            A = af.activate(Z, af_name)
            cache["Z" + str(l)] = Z
            cache["A" + str(l)] = A
            cache["A_prev" + str(l)] = A_prev
        return A, cache

    def __compute_cost(self, AL, Y):
        # Compute loss from aL and y.
        cost = cf.cross_entropy(AL, Y)
        dAL = cf.cross_entropy_d(AL, Y)
        return cost, dAL

    def __backward_propagation(self, dAL, cache):
        grads = {}
        L = len(self.layer_dims)
        m = dAL.shape[1]
        dA = dAL
        for l in reversed(range(1, L)):
            _, af_name = self.layer_dims[l]
            Z = cache["Z" + str(l)]
            W = self.parameters["W" + str(l)]
            A_prev = cache["A_prev" + str(l)]
            dZ = dA * af.activate_d(Z, af_name)
            dW = 1/m * np.dot(dZ, A_prev.T)
            db = 1/m * np.sum(dZ, axis=1, keepdims=True)
            dA = np.dot(W.T, dZ)

            grads["dW" + str(l)] = dW
            grads["db" + str(l)] = db
            grads["dA" + str(l)] = dA

        return grads

    def __update_parameters(self, learning_rate, grads):
        L = len(self.layer_dims)
        # Update rule for each parameter. Use a for loop.
        for l in range(1, L):
            self.parameters["W" + str(l)] = self.parameters["W" + str(l)] - learning_rate * grads["dW" + str(l)]
            self.parameters["b" + str(l)] = self.parameters["b" + str(l)] - learning_rate * grads["db" + str(l)]

    def optimize(self, learning_rate=0.5, num_iterations=1000, print_cost=False):
        costs = []
        for i in range(num_iterations):
            AL, cache = self.__forward_propagation(self.X)
            cost, dAL = self.__compute_cost(AL, self.Y)
            grads = self.__backward_propagation(dAL, cache)
            self.__update_parameters(learning_rate, grads)
            costs.append(cost)
            if print_cost and i % 100 == 0:
                print("Cost after iteration %i: %f" % (i, cost))
        return costs

    def predict(self, X):
        A, _ = self.__forward_propagation(X)
        predictions = A > 0.5   
        return predictions

def model(X_train, Y_train, X_test, Y_test, layer_dims, learning_rate=0.5, num_iterations=10000, print_cost=False):
    nn = DeepNeuralNetwork(X_train, Y_train, layer_dims)
    costs = nn.optimize(learning_rate, num_iterations, print_cost)
    Y_prediction_test = nn.predict(X_test)
    Y_prediction_train = nn.predict(X_train)

    # print train/test Errors
    print("train accuracy: {} %".format(100 - np.mean(np.abs(Y_prediction_train - Y_train)) * 100))
    print("test accuracy: {} %".format(100 - np.mean(np.abs(Y_prediction_test - Y_test)) * 100))
