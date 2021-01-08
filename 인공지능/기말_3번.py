# -*- coding: cp949 -*-
from sklearn import metrics
from sklearn.neighbors import KNeighborsClassifier
from sklearn.model_selection import train_test_split
from sklearn.datasets import load_iris
iris = load_iris()

print(iris['data'])
print(iris['target'])

# 입력과 출력을 설정한다.
X = iris.data
y = iris.target

# 전체 데이터를 학습 데이터와 테스트 데이터 비율 (80:20)으로 분할한다.
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=4)
print(X_train.shape)
print(y_train)

knn = KNeighborsClassifier(n_neighbors=5)
knn.fit(X, y)

# 0 = setosa, 1=versicolor, 2=virginica
classes = {0: 'setosa', 1: 'versicolor', 2: 'virginica'}

# 아직 보지 못한 새로운 데이터를 제시해보자.
x_new = [[3, 4, 5, 2],
         [5, 4, 2, 2]]
y_predict = knn.predict(x_new)

print(classes[y_predict[0]])
print(classes[y_predict[1]])
