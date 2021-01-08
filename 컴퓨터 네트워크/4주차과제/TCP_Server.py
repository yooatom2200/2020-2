from socket import *
serverPort = 12001
serverSocket = socket(AF_INET,SOCK_STREAM)
serverSocket.bind(('127.0.0.1',serverPort))
serverSocket.listen(1)
print("The Server is ready to recieve")
while 1:
    connectionSocket, addr = serverSocket.accept()
    sentence = connectionSocket.recv(1024)
    capitalizedSentence = sentence.upper()
    connectionSocket.send(capitalizedSentence)
    connectionSocket.close()
