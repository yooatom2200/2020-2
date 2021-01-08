#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#define FALSE 0
#define MAX_VERTICES 100
#define INF 1000000

typedef struct {
    int node;
    int weight;
} element;

typedef struct GraphType {
    int n; // ������ ����
    int weight[MAX_VERTICES][MAX_VERTICES];
} GraphType;

typedef struct heap {
    element heap[MAX_VERTICES];
    int heap_size;
} heap; //��Ÿ�� ����

typedef heap *heap_ptr;

void heap_init(heap_ptr h);
heap_ptr create_heap();
void insert_min_heap(heap_ptr h, element item);
int is_empty(heap_ptr h);
element delete_min_heap(heap_ptr h);
void dijkstra(GraphType *g, int *distances, int *selected, int start);
void printVisitedNode(GraphType *g, int *distances, int *selected);

int main() {
    GraphType g = {7,
                   {{0, 7, INF, INF, 3, 10, INF},
                    {7, 0, 4, 10, 2, 6, INF},
                    {INF, 4, 0, 2, INF, INF, INF},
                    {INF, 10, 2, 0, 11, 9, 4},
                    {3, 2, INF, 11, 0, INF, 5},
                    {10, 6, INF, 9, INF, 0, INF},
                    {INF, INF, INF, 4, 5, INF, 0}}};

    for (int i = 0; i < g.n; i++) {
        printf("---------���� %d �� ����-------\n", i);    //���� ��� �������� �Ÿ� ���
        int *distance = (int *)malloc(sizeof(int) * g.n);  //����� �Ÿ� ����
        int *selelcted = (int *)malloc(sizeof(int) * g.n); //������ ���õ� ���
        dijkstra(&g, distance, selelcted, i);              //���ͽ�Ʈ�� �Լ� ȣ��
        printVisitedNode(&g, distance, selelcted);         //���� ��� �Լ� ȣ��
        free(distance);                                    //��� �Ÿ����� �޸� �ü���� ��ȯ
        free(selelcted);                                   //������ �湮 ��� �޸� �ü�� ��ȯ
        printf("-----------------\n");
    }
}

void heap_init(heap_ptr h) { //�� �ʱ�ȭ �Լ�
    h->heap_size = 0;        //���� ũ�⸦ 0���� ����
}

heap_ptr create_heap() {                         //�� ���� �Լ�
    heap_ptr h = (heap_ptr)malloc(sizeof(heap)); //�����Ҵ����� �� ������ ������ ��ȯ
    heap_init(h);
    return h;
}

void insert_min_heap(heap_ptr h, element item) { //�ּ��� ����
    if ((h->heap_size + 1) == (MAX_VERTICES)) {  //���� ���̻� ���� �Ұ����ϸ�
        printf("���̻� ���� �����Ҽ� �����ϴ�!");
        exit(0); //���α׷� ����
    }

    int i = ++(h->heap_size); //���� ���ԵǹǷ� 1 �߰�
    //�� �Ʒ��� �����ؾ� ������ i �� ���ϴ� ��ġ
    while (i != 1 && item.weight < h->heap[i / 2].weight) {
        //�θ� �� ũ�� ��ȯ�� ��� �����Ѵ�. ���⼱ ����ġ�� key
        h->heap[i] = h->heap[i / 2];
        i /= 2;
    }
    h->heap[i] = item; //����ġ�� ã������ �����͸� ����
}

int is_empty(heap_ptr h) {
    return (h->heap_size == 0);
}

element delete_min_heap(heap_ptr h) {             //�ּ��� ����
    if (h->heap_size == 0) {                      //���� �����Ҽ� ������
        printf("���̻� ���� �����Ҽ� �����ϴ�!"); //����, ���α׷� ����
        exit(0);
    }
    int parent, child; //�θ� �ڽ� ����� �ε���
    element item, temp;
    item = h->heap[1];                //���� ���� ���
    temp = h->heap[(h->heap_size)--]; //������ �ø����
    //������ �Ǹ� ��尡 �Ѱ� ����������, h->heap_size �� 1�� ����.
    parent = 1;                                                                          //�θ� ����� ��ġ
    child = 2;                                                                           //�ڽ� ����� ��ġ. paret *2 �� �ڽĳ����
    while (child <= h->heap_size) {                                                      //�ڽĳ��� ���� ����� ���� �Ѿ�� �����ؾ���.
        if (child < h->heap_size && h->heap[child].weight > h->heap[child + 1].weight) { //����ġ�� ���⼱ key
                                                                                         //�ڽ� �����, ���� ��尡 �� ������ 1�� ���Ͽ� �������� ����
            child++;
        }
        if (temp.weight <= h->heap[child].weight) { //�ӽ� ��尡 �� ��ġ�� ã�ư�����, ����
            break;
        }
        h->heap[parent] = h->heap[child]; //�θ� ���� �ڽ� ���� ��ȯ
        parent = child;                   //�θ� ���� �ڽ� ���� ��ȯ
        child *= 2;
    }
    h->heap[parent] = temp; //���� �θ� ��ġ�� temp �� ������ ���� ���������� ����
    return item;            //��ȯ�� ������ ��ȯ
}

void dijkstra(GraphType *g, int *distances, int *selected, int start) {
    heap_ptr h = create_heap();
    for (int i = 0; i < g->n; i++) { //�Ÿ� ���� �迭 �ʱ�ȭ
        distances[i] = INF;
    }
    distances[start] = 0;    //������ - ������ �Ÿ��� 0
    selected[start] = start; //���� ���� =
    element e = {start, distances[start]};
    insert_min_heap(h, e);                                    //���� �̾��� ������ start�� �켱���� ť�� ����
    while (!is_empty(h)) {                                    //���� ������� ������ ���� �ݺ�
        element delelted = delete_min_heap(h);                //������ �Ÿ��� �ּ��� ������ �Ÿ��� ������
        int current_distance = delelted.weight;               //������ �Ÿ�
        int current_node = delelted.node;                     //������ ���
        for (int adjacent = 0; adjacent < g->n; adjacent++) { //��������
            if (g->weight[current_node][adjacent] == INF || g->weight[current_node][adjacent] == 0) {
                //���� ������ �ƴѰ��� ����ġ�� �����̰ų�, �ڱ� �ڽ��� ���
                continue; //���� ������ �ƴϹǷ� �ٸ� ���� ������ ã��
            }

            int weight = g->weight[current_node][adjacent];
            int distance = current_distance + weight; //������ ������ �Ÿ� + ���������� �Ÿ� ��, �������� ������ ����
            if (distance < distances[adjacent]) {     //�������� ���� ���̰� �迭�� ����Ȱͺ��� �۴ٸ�
                distances[adjacent] = distance;       //�迭 �� ������Ʈ
                element t = {adjacent, distance};     //ť�� ������ �Ÿ� ����
                insert_min_heap(h, t);
                selected[adjacent] = current_node; //������ �湮 ��� ������Ʈ
            }
        }
    }
    free(h);
}

void printVisitedNode(GraphType *g, int *distances, int *selected) {
    printf("���� | ��� (�Ÿ�) | [�ִܰŸ�]\n");
    printf("===============\n");
    for (int i = 0; i < g->n; i++) {
        int now = i;
        printf("���� %d :", i);                                         //���� ���� ���
        while (g->weight[now][selected[now]] != 0) {                    //���� ������ ����ġ�� 0�̸�
            printf("%d - (%d) - ", now, g->weight[now][selected[now]]); //���� ������ ���� ������ ����ġ ���
            now = selected[now];                                        //���� ������ ���������� �������� �湮�� �������� ����
        }
        printf("%d | [%d]\n", now, distances[i]); //������ ���� ��
    }
}