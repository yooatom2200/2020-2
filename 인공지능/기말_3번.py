# -*- coding: cp949 -*-
from sklearn import metrics
from sklearn.neighbors import KNeighborsClassifier
from sklearn.model_selection import train_test_split
from sklearn.datasets import load_iris
iris = load_iris()

print(iris['data'])
print(iris['target'])

# �Է°� ����� �����Ѵ�.
X = iris.data
y = iris.target

# ��ü �����͸� �н� �����Ϳ� �׽�Ʈ ������ ���� (80:20)���� �����Ѵ�.
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=4)
print(X_train.shape)
print(y_train)

knn = KNeighborsClassifier(n_neighbors=5)
knn.fit(X, y)

# 0 = setosa, 1=versicolor, 2=virginica
classes = {0: 'setosa', 1: 'versicolor', 2: 'virginica'}

# ���� ���� ���� ���ο� �����͸� �����غ���.
x_new = [[3, 4, 5, 2],
         [5, 4, 2, 2]]
y_predict = knn.predict(x_new)

print(classes[y_predict[0]])
print(classes[y_predict[1]])
