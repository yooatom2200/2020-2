from socket import *
serverName = '127.0.0.1'#로컬호스트
serverPort = 12000
clientSocket = socket(AF_INET,SOCK_DGRAM)
message = input('input lowerrcase sentence : ')
message = message.encode()
clientSocket.sendto(message,(serverName,serverPort))
modifiedMessage, serverAddress = clientSocket.recvfrom(2048)
print(modifiedMessage.decode())
clientSocket.close()
