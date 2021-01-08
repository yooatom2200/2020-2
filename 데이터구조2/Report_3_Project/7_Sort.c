#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#define MAX 100000 //데이터 항목 최대 개수
struct avg {//데이터를 저장시킬 포맷
	double move[7][7];
	double comp[7][7];
	double clocktime[7][7];
};
struct avg avglist[5];//데이터 저장할 곳
double mvcount;//이동횟수
double cmpcount;//비교횟수
int arr1[MAX], arr2[MAX], arr3[MAX],
    arr4[MAX], arr5[MAX],arr6[MAX],arr7[MAX];//배열확인
int many = 0;
void make_arr(int size){//크기에 맞는 배열 생성
	int x;
	srand(time(NULL));
	for(int i = 0; i < size; i++) {  //size 크기만큼 난수 생성
		x = rand() % 999 + 1;       //1부터 1000까지의 난수
		//난수 생성 후 저장
		arr1[i] = arr2[i] = arr3[i] = arr4[i] = arr5[i] =
			arr6[i] = arr7[i] = x;
	}
}

void swap(int* x, int* y){
	int temp;
	temp = *x;
	*x = *y;
	*y = temp;
}

void selection_sort(int *arr, int size){//선택정렬
	for (int i = 0; i < size - 1; i++) {
		int least = i;
		for (int j = i + 1; j < size; j++) {
			cmpcount++;
			if (arr[j] < arr[least]) {
				least = j;
			}
		}
		swap(&arr[least], &arr[i]);
		mvcount = mvcount + 3;  //이동 횟수
	}
}

int insert_sort(int* arr, int size){//삽입정렬
	int key;
	for (int i = 1; i < size; i++) {
		int j = i - 1;
		key = arr[i];
		mvcount++;//이동횟수 증가
		cmpcount++;//비교횟수 증가
		while (j >= 0 && arr[j] > key) {
			cmpcount++;
			arr[j + 1] = arr[j];
			mvcount++;
			j = j - 1;
		}
		arr[j + 1] = key;
		mvcount++;
	}
}

void bubble_sort(int* arr, int size)
{
	for (int i = size - 1; i > 0; i--)
	{
		for (int j = 0; j < i; j++)
		{
			cmpcount++;//비교횟수 증가
			if (arr[j] > arr[j + 1])
			{
				swap(&arr[j], &arr[j + 1]);
				mvcount = mvcount + 3;//이동횟수 증가
			}
		}
	}
}
int shell_insert(int* arr, int first, int last, int gap){//쉘전용 insert
	int key;
	int j;
	for (int i = first + gap; i <= last; i = i + gap) {
		j = i - gap;
		key = arr[i];
		mvcount++;
		cmpcount++;
		while (j >= first && arr[j] > key) {
			cmpcount++;
			arr[j + gap] = arr[j];
			mvcount++;
			j = j - gap;
		}
		arr[j + gap] = key;
		mvcount++;
	}
}
void shell_sort(int* arr, int size){//쉘정렬
	int gap;
	for (gap = size / 2; gap > 0; gap = gap / 2) {
		if (gap % 2 == 0)
			gap++;
		for (int i = 0; i < gap; i++) {
			shell_insert(arr, i, size - 1, gap);
		}
	}
}

//히프정렬
typedef struct heaptype {//히프 정의
	int heap[MAX];
	int heap_size;
}heaptype;
heaptype* create()//히프 생성
{
	return (heaptype*)malloc(sizeof(heaptype));
}
void init(heaptype* h){//히프 초기화
	h->heap_size = 0;
}
void insert_min_heap(heaptype* h, int item){//히프 삽입 함수
	int i;
	i = ++(h->heap_size);
	while ((i != 1) && (item < h->heap[i / 2])) {
		h->heap[i] = h->heap[i / 2];
		i /= 2;
	}
	h->heap[i] = item;
}
int delete_min_heap(heaptype* h){//히프 삭제 함수
	int parent = 1, child = 2;
	int item, temp;

	item = h->heap[1];
	temp = h->heap[(h->heap_size)--];

	while (child <= h->heap_size) {
		cmpcount++;
		if ((child < h->heap_size) && h->heap[child] > h->heap[child + 1]) {
			child++;
		}
		cmpcount++;
		if (temp < h->heap[child]) {
			break;
		}
		h->heap[parent] = h->heap[child];
		mvcount++;
		parent = child;
		child *= 2;
	}
	h->heap[parent] = temp;
	mvcount++;
	return item;
}
void heap_sort(int arr[], int size){//히프 정렬
	int i;
	heaptype h;//히프 h를 이용하여 정렬
	init(&h);
	for (int i = 0; i < size; i++) {
		insert_min_heap(&h, arr[i]);
	}
	for (int i = 0; i < size; i++) {
		arr[i] = delete_min_heap(&h);
	}
}

//합병정렬
void merge(int* arr, int low, int mid, int high){//병합
	int b1, b2, e1, e2, indx;
	b1 = low;
	e1 = mid;
	b2 = mid + 1;
	e2 = high;
	indx = low;
	int sorted[MAX] = { 0 };
	while (b1 <= e1 && b2 <= e2) {
		cmpcount++;
		if (arr[b1] < arr[b2]) {
			sorted[indx++] = arr[b1++];
			mvcount++;
		}
		else{
			sorted[indx++] = arr[b2++];
			mvcount++;
		}
	}
	if (b1 > e1) {
		for (int i = b2; i <= e2; i++) {
			sorted[indx++] = arr[i];
			mvcount++;
		}
	}
	else if (b2 > e2) {
		for (int i = b1; i <= e1; i++) {
			sorted[indx++] = arr[i];
			mvcount++;
		}
	}
	for (int i = low; i <= high; i++) {
		arr[i] = sorted[i];
		mvcount++;
	}
}
void merge_sort(int* arr, int left, int right){//합병 정렬
	int mid;
	if (left < right) {
		mid = (left + right) / 2;
		merge_sort(arr, left, mid);
		merge_sort(arr, mid + 1, right);
		merge(arr, left, mid, right);
	}
}

//퀵정렬
int partition(int* arr, int left, int right)
{
	int pivot, low, high;
	low = left, high = right + 1;
	pivot = arr[left];
	do {
		do {
			low++;
			cmpcount++;
		} while (low <= right && arr[low] < pivot);
		do {
			high--;
			cmpcount++;
		} while (high >= left && arr[high] > pivot);
		if (low < high)
		{
			swap(&arr[low], &arr[high]);
			mvcount = mvcount + 3;
		}
		cmpcount++;
	} while (low < high);
	cmpcount++;
	swap(&arr[left], &arr[high]);
	mvcount = mvcount + 3;
	return high;
}
void quick_sort(int* arr, int left, int right)
{
	if (left < right)
	{
		int q = partition(arr, left, right);
		quick_sort(arr, left, q - 1);
		quick_sort(arr, q + 1, right);
	}
}

int fun(int x, int y, int s){
	clock_t start, end;
	double elapsed;
	make_arr(s);
	mvcount = 0, cmpcount = 0;
	start = clock();
	if(y == 0) {//선택 정렬 실행
		selection_sort(arr1,s);
		avglist[many].move[x][y] = mvcount;
		avglist[many].comp[x][y] = cmpcount;
	}
	if(y == 1) {//삽입 정렬 실행
		insert_sort(arr2,s);
		avglist[many].move[x][y] = mvcount;
		avglist[many].comp[x][y] = cmpcount;
	}
	if(y == 2) {//버블 정렬 실행
		bubble_sort(arr3,s);
		avglist[many].move[x][y] = mvcount;
		avglist[many].comp[x][y] = cmpcount;
	}
	if(y == 3) {//쉘 정렬 실행
		shell_sort(arr4,s);
		avglist[many].move[x][y] = mvcount;
		avglist[many].comp[x][y] = cmpcount;
	}
	if(y == 4) {//히프 정렬 실행
		heap_sort(arr5,s);
		avglist[many].move[x][y] = mvcount;
		avglist[many].comp[x][y] = cmpcount;
	}
	if(y == 5) {//합병 정렬 실행
		merge_sort(arr6,0,s-1);
		avglist[many].move[x][y] = mvcount;
		avglist[many].comp[x][y] = cmpcount;
	}
	if(y == 6) {   //퀵  정렬 실행
		quick_sort(arr7,0,s-1);
		avglist[many].move[x][y] = mvcount;
		avglist[many].comp[x][y] = cmpcount;
	}
	end = clock();
	//실행시간 계산, 저장
	elapsed = (double)((end - start) * 1000) / CLOCKS_PER_SEC;
	avglist[many].clocktime[x][y] = elapsed;
}

void calcStatistics(){//연산 호출
	for(int tryn = 0; tryn < 5; tryn++) {
		printf("★%d번째 조사 시작★\n\n", tryn+1);
		for(int i = 0; i < 5; i++) {
			int rsize = 20000 * (i+1);
			for(int j = 0; j < 7; j++)
				fun(i, j, rsize);
		}
		many++;
		printf("\n★%d번째 조사 종료★\n\n", tryn+1);
	}
}

void cmpSortByTable(){
	for(int tryn = 0; tryn < 5; tryn++) {
		printf("★%d번 조사 결과 출력 시작★\n\n",tryn+1);
		for(int i = 0; i < 5; i++) {
			int rsize = 20000 * (i+1);//초기배열사이즈 설정
			printf("-----크기 = %d--------------------------------------------\n",rsize);
			printf("\t       소요시간|         비교회수|        이동회수|\n");
			for(int j = 0; j < 7; j++) {
				switch(j+1) {
				case 1:
					printf("선택 정렬\t");
					break;
				case 2:
					printf("삽입 정렬\t");
					break;
				case 3:
					printf("버블 정렬\t");
					break;
				case 4:
					printf("쉘   정렬\t");
					break;
				case 5:
					printf("히프 정렬\t");
					break;
				case 6:
					printf("합병 정렬\t");
					break;
				case 7:
					printf("퀵   정렬\t");
					break;
				}
				printf("%15.1f | %15.1f | %15.1f|\n", avglist[tryn].clocktime[i][j],
				       avglist[tryn].comp[i][j],avglist[tryn].move[i][j]);
			}
		}
		printf("\n★%d번째 조사 결과 출력 종료★\n\n", tryn+1);
	}
	printf("\n★평균출력★\n");
	for(int i = 0; i < 5; i++) {
		printf("★%d 사이즈의 평균★\n",20000*(i+1));
		for(int j = 0; j < 7; j++) {
			double mavg,cavg,tavg;
			mavg = cavg = tavg = 0;
			for(int k = 0; k < 5; k++) {
				mavg += avglist[k].move[i][j];
				cavg += avglist[k].comp[i][j];
				tavg += avglist[k].clocktime[i][j];
			}
			switch(j+1) {
			case 1:
				printf("선택 정렬 평균\n");
				break;
			case 2:
				printf("삽입 정렬 평균\n");
				break;
			case 3:
				printf("버블 정렬 평균\n");
				break;
			case 4:
				printf("쉘   정렬 평균\n");
				break;
			case 5:
				printf("히프 정렬 평균\n");
				break;
			case 6:
				printf("합병 정렬 평균\n");
				break;
			case 7:
				printf("퀵   정렬 평균\n");
				break;
			}
			printf("        소요시간|         비교회수|        이동회수|\n");
			printf("%15.1f | %15.1f | %15.1f|\n", tavg/5, cavg/5, mavg/5);
		}
	}
}

int main(void){
	calcStatistics();
	cmpSortByTable();
	//cmpSortByGraph(stat)
	//그래프는 직접 그리겠습니다.
}
