import matplotlib.pyplot as plt
import numpy as np


def ex_calculate(input):
    global weights
    global bias
    activation = bias
    for i in range(2):
        activation += weights[i] * input[i]
    if activation >= 0.0:
        return 1.0
    else:
        return 0.0


def ex_train_weights(X, Y, l_rate, n_epoch):
    global weights
    global bias
    for epoch in range(n_epoch):
        sum_error = 0.0
        for row, target in zip(X, Y):
            actual = ex_calculate(row)
            error = target - actual
            bias = bias + l_rate * error
            sum_error += error**2
            for i in range(2):
                weights[i] = weights[i] + l_rate * error * row[i]
            print(weights, bias)
        print("에포크 번호 = %d, 학습률 = %.3f, 오류 = %.3f" % (epoch, l_rate, sum_error))
    return weights


X = np.array([[0, 0], [-1, 2], [1, 3], [1, 1], [3, 1], [3, -1], [-3, 1], [-3, -1], [-1, -1], [-1, -3], [1, -2]])
Y = [1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0]

weights = [0.0, 0.0]
bias = 1.0

l_rate = 0.1
n_epoch = 10
weights = ex_train_weights(X, Y, l_rate, n_epoch)
print(weights, bias)

plt.scatter(X[:6, 0], X[:6, 1], color='r')
plt.scatter(X[6:11, 0], X[6:11, 1], color='b', marker='x')
plt.xlabel("X")
plt.ylabel("Y")
plt.title("Train Point(Red, Blue)")
plt.show()

# 좌표 입력 후 결과 출력
test_X, test_Y = input("두 점의 좌표를 입력해주세요 : ").split()
result = float(test_X) * weights[0] + float(test_Y) * weights[1] + bias
if(result > 0.0):
    print("빨간점 입니다. 결과값 : ", result)
else:
    print("파란점 입니다. 결과값 : ", result)

plt.scatter(X[:6, 0], X[:6, 1], color='red')
plt.scatter(X[6:11, 0], X[6:11, 1], color='b', marker='x')
plt.scatter(int(test_X), int(test_Y), color='y', marker='*')
plt.xlabel("X")
plt.ylabel("Y")
plt.title("Train Point(Red, Blue) and Test Point(Yellow Star)")
plt.show()
