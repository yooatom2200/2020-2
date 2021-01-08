from matplotlib import pyplot as plt
SelectTime = [835.8,3262.2,7406.4,13317.4,20710.2]
SelectCompare =[199990000.0,799980000.0,1799970000.0,3199960000.0,4999950000.0]
SelectMove = [59997.0,1199970.0,179997.0,239997.0,299997.0]

InsertTime = [410.8,1644.2,3720.2,6634.6,10328.6]
InsertCompare =[100192419.6,400055475.0,901263721.8,1598681399.4,2499912332.4]
InsertMove = [100212418.6,400095474.0,901323720.8,1598761398.4,2500012331.4]

BubbleTime = [1028.8,4592.2,10370.4,18501.8,29098.0]
BubbleCompare =[199990000.0,799980000.0,1799970000.0,3199960000.0,4999950000.0]
BubbleMove = [300914286.6,1200747451.8,2695793026.8,4799868249.0,7494994849.8]

ShellTime = [3.6,7.4,12.2,16.0,20.6]
ShellCompare =[512549.4,1170807.8,1844605.0,2538958.4,3302836.0]
ShellMove = [772545.4,1730802.8,2684604.0,3738952.4,4802832.0]

HeapTime = [3.4,7.0,11.0,14.6,2.2]
HeapCompare =[473226.8,1026499.2,1608747.2,2212920.8,12030.4]
HeapMove = [253601.4,547350.2,855505.0,1174545.6,105900.2]

MergeTime = [154.8,311.8,479.2,621.4,786.4]
MergeCompare =[260853.2,561747.0,877104.2,1203351.6,1536063.0]
MergeMove = [574464.0,1228928.0,1908928.0,2617856.0,3337856.0]

QuickTime = [2.0,3.8,6.2,8.2,11.4]
QuickCompare =[431237.2,910293.6,1429641.0,1938485.0,2477889.8]
QuickMove = [238263.6,532609.2,846099.0,1178224.2,1523955.0]

plt.subplot(1,3,1)
plt.plot(SelectTime,label='Select',marker='o')
plt.plot(InsertTime,label='Insert',marker='o')
plt.plot(BubbleTime,label='Bubble',marker='o')
plt.plot(ShellTime,label='Shell',marker='o')
plt.plot(HeapTime,label='Heap',marker='o')
plt.plot(MergeTime,label='Merge',marker='o')
plt.plot(QuickTime,label='Quick',marker='o')
plt.title('Time')
plt.legend();

plt.subplot(1,3,2)
plt.plot(SelectCompare,label='Select',marker='o')
plt.plot(InsertCompare,label='Insert',marker='o')
plt.plot(BubbleCompare,label='Bubble',marker='o')
plt.plot(ShellCompare,label='Shell',marker='o')
plt.plot(HeapCompare,label='Heap',marker='o')
plt.plot(MergeCompare,label='Merge',marker='o')
plt.plot(QuickCompare,label='Quick',marker='o')
plt.title('Compare')
plt.legend();

plt.subplot(1,3,3)
plt.plot(SelectMove,label='Select',marker='o')
plt.plot(InsertMove,label='Insert',marker='o')
plt.plot(BubbleMove,label='Bubble',marker='o')
plt.plot(ShellMove,label='Shell',marker='o')
plt.plot(HeapMove,label='Heap',marker='o')
plt.plot(MergeMove,label='Merge',marker='o')
plt.plot(QuickMove,label='Quick',marker='o')
plt.title('Move')
plt.legend();

plt.show()
