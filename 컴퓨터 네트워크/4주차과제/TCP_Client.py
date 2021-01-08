from socket import *
serverName = '127.0.0.1'
serverPort = 12001
clientSocket = socket(AF_INET,SOCK_STREAM)
clientSocket.connect((serverName,serverPort))
sentence = input("Input lowercase sentence : ")
sentence = sentence.encode()#메세지 인코딩
clientSocket.send(sentence)
modifiedSentence = clientSocket.recv(1024)
print("From Server : ", modifiedSentence)
clientSocket.close()
