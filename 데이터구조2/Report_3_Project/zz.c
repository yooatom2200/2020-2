#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int sorted[100005];

typedef int element;

typedef struct {
	element heap[100005];
	int heapSize;
}Heap;

typedef struct {
	double elapsed;
	unsigned long long move;
	unsigned long long compare;
}info;

typedef Heap* HeapPtr; //힙 포인터 정의

//함수 원형 정의부
void initHeap(HeapPtr h);


void initHeap(HeapPtr h) { //힙 초기화
	//힙의 크기를 0으로 설정해서 초기화 해준다.
	h->heapSize = 0;
}

void insertMinHeap(HeapPtr h, element data,info* info) { //힙에 삽입하는 함수
	int i = ++(h->heapSize);  //힙이 삽입되므로 1 추가
	//맨 아래에 삽입해야 함으로 i 는 최하단 위치
	while ((i != 1) && (data < h->heap[i / 2])) {
		info->compare++;
		//부모가 더 크면 교환을 계속 진행한다.
		h->heap[i] = h->heap[i / 2];
		info->move++;
		i /= 2;
		info->move++;
	}
	h->heap[i] = data;   //정위치를 찾았으면 데이터를 삽입
	info->move++;
}

element deleteMinHeap(HeapPtr h,info * info) { //최소힙 삭제
	int parent, child;   //부모 자식 노드의 인덱스
	element data, temp;
	data = h->heap[1];   //삭제 당할 노드
	info->move++;
	temp = h->heap[(h->heapSize)--];   //상위로 올릴노드
	info->move++;
	//삭제가 되면 노드가 한개 삭제됨으로, h->heap_size 에 1을 뺀다.
	parent = 1;  //부모 노드의 위치
	info->move++;
	child = 2;  //자식 노드의 위치. paret *2 가 자식노드임
	info->move++;
	while (child <= h->heapSize) {   //자식노드는 힙에 저장된 값을 넘어가면 종료해야함.
		info->compare++;
		if ((child < h->heapSize) && h->heap[child]> h->heap[child + 1]) {
			info->compare++;
			child++;       //자식 노드중, 우측 노드가 더 작으면 1을 더하여
			//우측 노드로 설정
			info->move++;
		}
		if (temp < h->heap[child]) {     //임시 노드가 제 위치를 찾아갔으면, 종료
			info->compare++;
			break;
		}
		h->heap[parent] = h->heap[child];     //부모 노드랑 자식 노드랑 교환
		info->move++;
		parent = child;     //부모 노드랑 자식 노드랑 교환
		info->move++;
		child *= 2;
		info->move++;
	}
	h->heap[parent] = temp;   //현재 부모 위치에 temp 를 씌워서
	info->move++;
	//값을 정상적으로 설정
	return data;
}



void swap(int* a, int* b,info *info) {
	int temp = *a;
	*a = *b;
	*b = temp;
	info->move += 3;
}

void selectSort(int* arr, int index,info *info) {
	for (int i = 0; i < index; i++) {
		info->compare++;
		int min = i;
		info->move++;
		for (int j = i; j < index; j++) {
			info->compare++;
			if (arr[j] < arr[min]) {
				info->compare++;
				min = j;
				info->move++;
			}
		}
		swap(&arr[min], &arr[i],info);
	}
}

void insertSort(int* arr, int index,info *info) {
	for (int i = 1; i < index; i++) {
		info->compare++;
		int temp = arr[i];
		info->move++;
		int j = i-1;
		info->move++;
		while (j >= 0 && arr[j] > temp) {
			info->compare++;
			arr[j + 1] = arr[j];
			info->move++;
			j--;
			info->move++;
		}
		arr[j + 1] = temp;
		info->move++;
	}
}

void boubleSort(int* arr, int index,info *info) {
	for (int i = index-1; i > 0; i--) {
		info->compare++;
		for (int j = 0; j < i; j++) {
			info->compare++;
			if (arr[j] > arr[j + 1]) {
				info->compare++;
				swap(&arr[j], &arr[j + 1],info);
			}
		}

	}
}

void printArr(int* arr, int index) {
	for (int i = 0; i < index; i++) {
		printf("%d ", arr[i]);
	}
	printf("\n\n\n");
}

void insertSort2(int* arr, int first, int last, int gap,info *info) {

	for (int i = first +gap; i < last; i+= gap) {
		info->compare++;
		int temp = arr[i];
		info->move++;

		int j = i - gap;
		info->move++;
		while (j >= first && arr[j] > temp) {
			info->compare++;
			arr[j + gap] = arr[j];
			info->move++;
			j-= gap;
			info->move++;
		}
		arr[j + gap] = temp;
		info->move++;
	}

}

void shellSort(int* arr, int index,info *info) {
	for (int gap = index / 2; gap > 0; gap /= 2) {
		info->compare++;
		if (gap % 2 == 0) {
			info->compare++;
			gap++;
			info->move++;
		}

		for (int i = 0; i < gap; i++) {
			info->compare++;
			insertSort2(arr, i, index, gap,info);
		}
	}
}

void heapSort(int* arr, int index,info *info) {
	HeapPtr h = (HeapPtr)malloc(sizeof(Heap));
	initHeap(h);

	for (int i = 0; i < index; i++) {
		info->compare++;
		insertMinHeap(h, arr[i],info);
	}
	for (int i = 0; i < index; i++) {
		info->compare++;
		arr[i] = deleteMinHeap(h,info);
	}
	free(h);
}


void merge(int* arr, int start, int mid, int end,info *info) {
	int index1, index2, sortedIndex;
	index1 = start;
	info->move++;
	index2 = mid + 1;
	info->move++;
	sortedIndex = start;
	info->move++;

	/* 분할 정렬된 list의 합병 */
	while (index1 <= mid && index2 <= end) {
		info->compare++;
		if (arr[index1] <= arr[index2]) {
			info->compare++;
			sorted[sortedIndex++] = arr[index1++];
			info->move++;
		}
		else {
			info->compare++;
			sorted[sortedIndex++] = arr[index2++];
			info->move++;

		}

	}

	// 남아 있는 값들을 일괄 복사

	for (int i = index2; i <= end; i++) {
		info->compare++;
		sorted[sortedIndex++] = arr[i];
		info->move++;
	}

	// 남아 있는 값들을 일괄 복사
	for (int i = index1; i <= mid; i++) {
		info->compare++;
		sorted[sortedIndex++] = arr[i];
		info->move++;
	}


	// 배열 sorted[](임시 배열)의 리스트를 배열 list[]로 재복사
	for (int i = start; i <= end; i++) {
		info->compare++;
		arr[i] = sorted[i];
		info->move++;
	}
}

void mergeSort(int* arr, int start, int end,info *info) {
	if (start < end) {
		info->compare++;
		int mid = (start + end) / 2;
		info->move++;
		mergeSort(arr, start, mid,info);
		mergeSort(arr, mid + 1, end,info);
		merge(arr, start, mid, end,info);
	}

}
void printInfo(info* info) {
	printf("\t%10.4f%15lld%15lld\n", info->elapsed, info->compare, info->move);
}

int main() {

	srand(time(NULL));
	int SIZE = 0;
	int *insertArr = (int*)malloc(sizeof(int) * SIZE);
	int *selectArr = (int*)malloc(sizeof(int) * SIZE);
	int *boubleArr = (int*)malloc(sizeof(int) * SIZE);
	int *shellArr = (int*)malloc(sizeof(int) * SIZE);
	int *heapArr = (int*)malloc(sizeof(int) * SIZE);
	int *mergeArr = (int*)malloc(sizeof(int) * SIZE);
	int *quickArr = (int*)malloc(sizeof(int) * SIZE);
	clock_t start, end;



	for (int arrSize = 0; arrSize < 5; arrSize++) {

		SIZE += 3000;

		insertArr = (int*)realloc(insertArr, sizeof(int) * SIZE);
		selectArr = (int*)realloc(selectArr, sizeof(int) * SIZE);
		boubleArr = (int*)realloc(boubleArr, sizeof(int) * SIZE);
		shellArr = (int*)realloc(shellArr, sizeof(int) * SIZE);
		heapArr = (int*)realloc(heapArr, sizeof(int) * SIZE);
		mergeArr = (int*)realloc(mergeArr, sizeof(int) * SIZE);
		quickArr = (int*)realloc(quickArr, sizeof(int) * SIZE);

		info insertInfo = { 0,0,0 };
		info selectInfo = { 0,0,0 };
		info boubleInfo = { 0,0,0 };
		info shellInfo = { 0,0,0 };
		info heapInfo = { 0,0,0 };
		info mergeInfo = { 0,0,0 };
		info quickInfo = { 0,0,0 };


		for (int time = 0; time < 5; time++) {

			for (int i = 0; i < SIZE; i++) {
				int temp = rand();
				selectArr[i] = insertArr[i] = boubleArr[i] = shellArr[i] = heapArr[i] =
					mergeArr[i] = quickArr[i] = temp;
			}

			mergeSort(mergeArr, 0, SIZE-1,&mergeInfo);
			//printArr(mergeArr, SIZE);

			start = clock();
			selectSort(selectArr, SIZE,&selectInfo);
			end = clock();
			selectInfo.elapsed += (double)((end - start) * 1000) / CLOCKS_PER_SEC;
			//printArr(selectArr, SIZE);

			start = clock();
			insertSort(insertArr, SIZE,&insertInfo);
			end = clock();
			insertInfo.elapsed += (double)((end - start) * 1000) / CLOCKS_PER_SEC;
			//printArr(insertArr, SIZE);

			start = clock();
			boubleSort(boubleArr, SIZE,&boubleInfo);
			end = clock();
			boubleInfo.elapsed += (double)((end - start) * 1000) / CLOCKS_PER_SEC;
			//printArr(boubleArr, SIZE);

			start = clock();
			shellSort(shellArr, SIZE,&shellInfo);
			end = clock();
			shellInfo.elapsed += (double)((end - start) * 1000) / CLOCKS_PER_SEC;
			//printArr(shellArr, SIZE);

			start = clock();
			heapSort(heapArr, SIZE,&heapInfo);
			end = clock();
			heapInfo.elapsed += (double)((end - start) * 1000) / CLOCKS_PER_SEC;
			//printArr(heapArr, SIZE);




		}
		printf("------데이터가 %d 개 일때 5회 수행 평균 결과 -------\n",SIZE);
		printf("%s%20s%15s%15s\n","정렬명","수행시간","비교수","이동수");
		printf("선택정렬");
		printInfo(&selectInfo);
		printf("삽입정렬");
		printInfo(&insertInfo);
		printf("버블정렬");
		printInfo(&boubleInfo);
		printf("쉘정렬  ");
		printInfo(&shellInfo);
		printf("힙정렬  ");
		printInfo(&heapInfo);
		printf("------------------------------------------\n");

	}


}

/*
   void mergeSort(int arr, int index);
   void quickSort(int arr, int index);*/
