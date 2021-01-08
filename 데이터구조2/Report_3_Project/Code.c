#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#define MAX_ELEMENT 100000

double moving;
double compare;

int arr1[MAX_ELEMENT];
int arr2[MAX_ELEMENT];
int arr3[MAX_ELEMENT];
int arr4[MAX_ELEMENT];
int arr5[MAX_ELEMENT];
int arr6[MAX_ELEMENT];
int arr7[MAX_ELEMENT];
typedef struct {
	int heap[MAX_ELEMENT];
	int heap_size;
}heaptype;
struct avg {
	double move[7][7];
	double comp[7][7];
	double clocktime[7][7];
};
struct avg avglist[5];
double move[7][5] = { 0 };
double comp[7][5] = { 0 };
double clocktime[7][5] = { 0 };

heaptype* create()
{
	return (heaptype*)malloc(sizeof(heaptype));
}

void init(heaptype* h)
{
	h->heap_size = 0;
}
void insert_min_heap(heaptype* h, int item)
{
	int i;
	i = ++(h->heap_size);
	while ((i != 1) && (item < h->heap[i / 2]))
	{
		h->heap[i] = h->heap[i / 2];
		i /= 2;
	}
	h->heap[i] = item;
}


int delete_min_heap(heaptype* h)
{
	int parent = 1, child = 2;
	int item, temp;

	item = h->heap[1];
	temp = h->heap[(h->heap_size)--];

	while (child <= h->heap_size)
	{
		compare++;
		if ((child < h->heap_size) && h->heap[child] > h->heap[child + 1])
		{
			child++;
		}
		compare++;
		if (temp < h->heap[child])
		{
			break;
		}
		h->heap[parent] = h->heap[child];
		moving++;
		parent = child;
		child *= 2;
	}
	h->heap[parent] = temp;
	moving++;
	return item;
}
void heap_sort(int arr[], int size)
{
	int i;
	heaptype h;
	init(&h);
	for (int i = 0; i < size; i++)
	{
		insert_min_heap(&h, arr[i]);
	}
	for (int i = 0; i < size; i++)
	{
		arr[i] = delete_min_heap(&h);
	}
}

void swap(int* x, int* y)
{
	int temp;
	temp = *x;
	*x = *y;
	*y = temp;
}
void selectionsort(int* arr, int size)//선택 정렬
{
	for (int i = 0; i < size - 1; i++)
	{
		int least = i;
		for (int j = i + 1; j < size; j++)
		{
			compare++;
			if (arr[j] < arr[least])
			{
				least = j;
			}
		}
		swap(&arr[least], &arr[i]);
		moving = moving + 3;  //이동 횟수
	}
}
int insertsort(int* arr, int size)
{
	int key;
	for (int i = 1; i < size; i++)
	{
		int j = i - 1;
		key = arr[i];
		moving++;
		compare++;
		while (j >= 0 && arr[j] > key)
		{
			compare++;
			arr[j + 1] = arr[j];
			moving++;
			j = j - 1;
		}
		arr[j + 1] = key;
		moving++;
	}
}

void shell_sort(int* arr, int size)
{
	int gap;
	for (gap = size / 2; gap > 0; gap = gap / 2)
	{
		if (gap % 2 == 0)
		{
			gap++;
		}
		for (int i = 0; i < gap; i++)
		{
			insertsort2(arr, i, size - 1, gap);
		}
	}
}

int insertsort2(int* arr, int first, int last, int gap)
{
	int key;
	int j;
	for (int i = first + gap; i <= last; i = i + gap)
	{
		j = i - gap;
		key = arr[i];
		moving++;
		compare++;
		while (j >= first && arr[j] > key)
		{
			compare++;
			arr[j + gap] = arr[j];
			moving++;
			j = j - gap;
		}
		arr[j + gap] = key;
		moving++;
	}
}
void bubblesort(int* arr, int size)
{
	for (int i = size - 1; i > 0; i--)
	{
		for (int j = 0; j < i; j++)
		{
			compare++;
			if (arr[j] > arr[j + 1])
			{
				swap(&arr[j], &arr[j + 1]);
				moving = moving + 3;
			}
		}
	}
}

void merge(int* arr, int low, int mid, int high)
{
	int b1, b2, e1, e2, indx;
	b1 = low;
	e1 = mid;
	b2 = mid + 1;
	e2 = high;
	indx = low;
	int sorted[MAX_ELEMENT] = { 0 };
	while (b1 <= e1 && b2 <= e2)
	{
		compare++;
		if (arr[b1] < arr[b2])
		{
			sorted[indx++] = arr[b1++];
			moving++;
		}
		else
		{
			sorted[indx++] = arr[b2++];
			moving++;
		}
	}
	if (b1 > e1)
	{
		for (int i = b2; i <= e2; i++)
		{
			sorted[indx++] = arr[i];
			moving++;
		}
	}
	else if (b2 > e2)
	{
		for (int i = b1; i <= e1; i++)
		{
			sorted[indx++] = arr[i];
			moving++;
		}
	}
	for (int i = low; i <= high; i++)
	{
		arr[i] = sorted[i];
		moving++;
	}
}
void merge_sort(int* arr, int left, int right)
{
	int mid;
	if (left < right)
	{
		mid = (left + right) / 2;
		merge_sort(arr, left, mid);
		merge_sort(arr, mid + 1, right);
		merge(arr, left, mid, right);
	}
}
int partition(int* arr, int left, int right)
{
	int pivot, low, high;
	low = left, high = right + 1;
	pivot = arr[left];
	do {
		do {
			low++;
			compare++;
		} while (low <= right && arr[low] < pivot);
		do {
			high--;
			compare++;
		} while (high >= left && arr[high] > pivot);
		if (low < high)
		{
			swap(&arr[low], &arr[high]);
			moving = moving + 3;
		}
		compare++;
	} while (low < high);
	compare++;
	swap(&arr[left], &arr[high]);
	moving = moving + 3;
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
void make_arr(int size)
{
	int x;
	srand(time(NULL));
	for (int i = 0; i < size; i++)
	{
		x = rand() % 999+1;
		arr1[i] = x;
		arr2[i] = x;
		arr3[i] = x;
		arr4[i] = x;
		arr5[i] = x;
		arr6[i] = x;
		arr7[i] = x;
	}
}

int x = 0;
void caclStatistics()
{
	for (int j = 0; j < 5; j++)
	{
		printf("--------------배열 사이즈 20000일 경우------------------\n");
		for (int i = 1; i < 8; i++)
		{
			x = 0;
			fun(i, 20000, j);
		}
		printf("--------------배열 사이즈 40000일 경우------------------\n");
		for (int i = 1; i < 8; i++)
		{
			x = 1;
			fun(i, 40000, j);
		}
		printf("--------------배열 사이즈 60000일 경우------------------\n");
		for (int i = 1; i < 8; i++)
		{
			x = 2;
			fun(i, 60000, j);
		}
		printf("--------------배열 사이즈 80000일 경우------------------\n");
		for (int i = 1; i < 8; i++)
		{
			x = 3;
			fun(i, 80000, j);
		}
		printf("--------------배열 사이즈 100000일 경우------------------\n");
		for (int i = 1; i < 8; i++)
		{
			x = 4;
			fun(i, 100000, j);
		}
		printf("\n------------------------------------%d회 측정완료------------------------------------\n", j + 1);
	}
	for (int a = 0; a < 5; a++)
	{
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				avglist[a].move[i][5] += avglist[a].move[i][j];
				avglist[a].comp[i][5] += avglist[a].comp[i][j];
				avglist[a].clocktime[i][5] += avglist[a].clocktime[i][j];
			}
			avglist[a].move[i][6] = avglist[a].move[i][5] / 5;
			avglist[a].comp[i][6] = avglist[a].comp[i][5] / 5;
			avglist[a].clocktime[i][6] = avglist[a].clocktime[i][5] / 5;
			printf("------------------%d번째 사이즈 평균 값------------------------------- \n", a + 1);
			printf("움직임 : %lf  \n", avglist[a].move[i][6]);
			printf("비교 : %lf  \n", avglist[a].comp[i][6]);
			printf("시간 : %lf  \n", avglist[a].clocktime[i][6]);
			printf("----------------------------------------------------------------------\n");
		}
	}
	printf("\n");
}
int main(void)
{
	caclStatistics();
}
int fun(int i, int size, int j)
{
	clock_t start, end;
	double elapsed;
	moving = 0, compare = 0;
	make_arr(size);
	start = clock();
	switch (i)
	{
	case 1:
		selectionsort(arr1, size);
		avglist[x].move[i - 1][j] = moving;
		avglist[x].comp[i - 1][j] = compare;
		printf("선택 정렬 ");
		break;
	case 2:
		insertsort(arr2, size);
		avglist[x].move[i - 1][j] = moving;
		avglist[x].comp[i - 1][j] = compare;
		printf("삽입 정렬 ");
		break;
	case 3:
		bubblesort(arr3, size);
		avglist[x].move[i - 1][j] = moving;
		avglist[x].comp[i - 1][j] = compare;
		printf("버블 정렬 ");
		break;
	case 4:
		shell_sort(arr4, size);
		avglist[x].move[i - 1][j] = moving;
		avglist[x].comp[i - 1][j] = compare;
		printf("쉘 정렬 ");
		break;
	case 5:
		heap_sort(arr5, size);
		avglist[x].move[i - 1][j] = moving;
		avglist[x].comp[i - 1][j] = compare;
		printf("힙 정렬 ");
		break;
	case 6:
		merge_sort(arr6, 0, size - 1);
		avglist[x].move[i - 1][j] = moving;
		avglist[x].comp[i - 1][j] = compare;
		printf("합병 정렬 ");
		break;
	case 7:
		quick_sort(arr7, 0, size - 1);
		avglist[x].move[i - 1][j] = moving;
		avglist[x].comp[i - 1][j] = compare;
		printf("퀵 정렬 ");
		break;
	}
	end = clock();
	elapsed = (double)((end - start) * 1000) / CLOCKS_PER_SEC;
	avglist[x].clocktime[i-1][j] = elapsed;
	printf("실행시간 : %.5f\n", elapsed);
}
