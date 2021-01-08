from socket import *
from pandas_datareader import data
import datetime

now = datetime.datetime.today()
today = now.strftime('%Y-%m-%d')
before = (now - datetime.timedelta(days=6)).strftime('%Y-%m-%d')

serverPort = 12000

serverSocket = socket(AF_INET,SOCK_STREAM)
serverSocket.bind(('localhost',serverPort))
serverSocket.listen(1)
print("주식종목입력 대기중...")

connectionSocket, addr = serverSocket.accept()
stock = connectionSocket.recv(2048).decode()
print(stock)
data ="\n" + str(data.DataReader('000660.ks','yahoo', before, today)) + "\n"
print(stock + "에대한 주식값 전송")
connectionSocket.sendall(data.encode())
connectionSocket.close()
