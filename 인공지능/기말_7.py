# -*- coding: cp949 -*-
import numpy as np
from sklearn.datasets import load_iris

def actf(x):
    return 1/(1+np.exp(-x))


def actf_deriv(x):
    return x*(1-x)


iris = load_iris()

X = np.array(iris.data[:, (0, 1)])
#세토사 출력값들을 이차원배열 형태로 변환
y = np.reshape(np.array((iris.target == 0).astype(np.int)), (150, 1))
print(X)
print(y)
np.random.seed(5)

inputs = 2	#기존코드에서 가중치는 제거되고 너비와 높이만을 이용
hiddens = 6
outputs = 1

weight0 = np.zeros((inputs, hiddens)) # 가중치 0으로 세트
weight1 = np.zeros((hiddens, outputs)) # 가중치 0으로 세트
layer2 = 0
for i in range(10000):

    layer0 = X

    net1 = np.dot(layer0, weight0)
    layer1 = actf(net1)

    net2 = np.dot(layer1, weight1)
    layer2 = actf(net2)

    layer2_error = layer2-y
    layer2_delta = layer2_error*actf_deriv(layer2)

    layer1_error = np.dot(layer2_delta, weight1.T)
    layer1_delta = layer1_error*actf_deriv(layer1)

    weight1 += -0.2*np.dot(layer1.T, layer2_delta)
    weight0 += -0.2*np.dot(layer0.T, layer1_delta)

print(layer2)			# 현재 출력층의 값을 출력한다.

#전체 결과 출력
i = 0
while i < np.size(y):
    print("붓꽃 크기 : [", '{:0.1f} '.format(X[i][0]), '{:0.1f}'.format(X[i][1]), "], 품종이 세토사인가? :", bool(y[i]), ", 학습값 : ", np.round(layer2[i],6))
    i = i + 1