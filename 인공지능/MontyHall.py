import random #랜덤 사용을 위해 추가
Try = 100 #시도횟수
Max = 1000000 #최대시도횟수
Correct = Fail = ChangeC = ChangeF = 0 #성공,실패,바꾼뒤성공,바꾼뒤실패 변수들
while(Try <= Max):
    for i in range(0,Try):
        RealCar = random.randrange(3) #실제 차가 있는 슬롯
        SelectCar = random.randrange(3) #참가자 슬롯 선택
        ShowGoat = random.randrange(3) #진행자가 보여줄 염소 슬롯
        #보여줄 염소슬롯은 참가자가 선택한 곳과 실제 차가 있는곳을 제외한 곳이다
        while(SelectCar == ShowGoat or ShowGoat == RealCar):
            ShowGoat = random.randrange(3)
        ChangeCar = random.randrange(3) #바꾼 선택슬롯
        #바꾼 선택슬롯은 보여준 염소슬롯과 선택한 슬롯을 제외한 곳이다.
        while(ChangeCar == ShowGoat or ChangeCar == SelectCar):
            ChangeCar = random.randrange(3)

        if(SelectCar == RealCar): #선택한슬롯과 실제 차가 있는 슬롯의 판별
            Correct = Correct + 1
        else:
            Fail = Fail + 1

        if(ChangeCar == RealCar): #바꾼슬롯과 실제 차가 있는 슬롯의 판별
            ChangeC = ChangeC + 1
        else:
            ChangeF = ChangeF + 1
    print("\n--------------------------------------------")
    print("Try = ", Try)
    print("Not Change Correct = ", Correct, ", Fail = ", Fail)
    print("Yes Change Correct = ", ChangeC, ", Fail = ", ChangeF)
    print("Not Change Correct Percentage = ", round((Correct/Try)*100,2))
    print("Yes Change Correct Percentage = ", round((ChangeC/Try)*100,2))

    Try = Try * 10 #시도횟수를 10배씩 늘려서 진행
    Correct = Fail = ChangeC = ChangeF = 0 #각 변수 초기화
