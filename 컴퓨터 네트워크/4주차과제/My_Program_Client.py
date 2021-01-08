from socket import *
serverName = 'localhost'
serverPort = 12000

clientSocket = socket(AF_INET,SOCK_STREAM)
clientSocket.connect((serverName,serverPort))
print("최근 일주일간의 원하는 종목의 장정보를 알 수 있습니다.")

number = input("카카오 : 035720 | 삼성전자 : 005930 | 셀트리온 : 068270\
\nSK하이닉스 : 000660 | 신풍제약 : 019170 | LG화학 : 051910\
\n코드조회는 ktb.co.kr/trading/popup/itemPop.jspx 이용\n\
---------------------------------------------------------\
\n종목 코드를 정확히 입력해 주세요(종료 : q) : ")
number =str(number) + '.KS'
number = number.encode()#메세지 인코딩
clientSocket.send(number)

rcvdata = clientSocket.recv(8196).decode()
print(rcvdata)
clientSocket.close()
