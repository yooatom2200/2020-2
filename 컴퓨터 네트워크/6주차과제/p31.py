from matplotlib import pyplot as plt
SampleRTT = [106, 120, 140, 90, 115]
EstimateRTT = [100]
TimeoutInterval = []
alpha = 0.125
beta = 0.25
DevRTT = [5]

for i in range(0,5):
    EstimateRTT.append(round(alpha * SampleRTT[i] + (1-alpha) * EstimateRTT[i],3))
    DevRTT.append(round(beta * abs(SampleRTT[i] - EstimateRTT[i+1]) + (1-beta) * DevRTT[i],3))
    TimeoutInterval.append(round(EstimateRTT[i+1] + 4 * DevRTT[i+1],3))

for i in range(0,5):
    print(i+1,"calc value-----------------------------------------------------------")
    print("EstimateRTT : ", EstimateRTT[i+1], " | DevRTT : ", DevRTT[i+1], " | TimeoutInterval : ", TimeoutInterval[i])

x_value = [1,2,3,4,5];
plt.plot(x_value,SampleRTT)
plt.plot(x_value,TimeoutInterval)
plt.plot(x_value,[EstimateRTT[1],EstimateRTT[2],EstimateRTT[3],EstimateRTT[4],EstimateRTT[5]])
plt.legend(['SampleRTT','TimeoutInterval','EstimateRTT'])
plt.show()
