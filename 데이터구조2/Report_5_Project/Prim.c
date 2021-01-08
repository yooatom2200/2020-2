#include <stdio.h>
#include <stdlib.h>
#define TRUE 1
#define FALSE 2
#define MAX_VERTICES 100
#define INF 1000

typedef struct GraphType {
    int n; //������ ����
    int weight[MAX_VERTICES][MAX_VERTICES];
} GraphType;

typedef struct {
    int key;      // ������ �ּҰ���ġ
    int selected; // ���ÿ���
    int parent;   //�θ�
    int self;     //�ڽ� ���
} element;

typedef struct {
    element heap[MAX_VERTICES];
    int heap_size;
} HeapType;

// ���� �Լ�
void insert_min_heap(HeapType *h, element item) {
    int i;
    i = ++(h->heap_size);
    //  Ʈ���� �Ž��� �ö󰡸鼭 �θ� ���� ���ϴ� ����
    while ((i != 1) && (item.key < h->heap[i / 2].key)) {
        h->heap[i] = h->heap[i / 2];
        i /= 2;
    }
    h->heap[i] = item; // ���ο� ��带 ����
}
// ���� �Լ�
element delete_min_heap(HeapType *h) {
    int parent, child;
    element item, temp;
    item = h->heap[1];
    temp = h->heap[(h->heap_size)--];
    parent = 1;
    child = 2;
    while (child <= h->heap_size) {
        // ���� ����� �ڽĳ���� �� ���� �ڽĳ�带 ã�´�.
        if ((child < h->heap_size) &&
            h->heap[child].key > h->heap[child + 1].key)
            child++;
        if (temp.key < h->heap[child].key)
            break;
        // �Ѵܰ� �Ʒ��� �̵�
        h->heap[parent] = h->heap[child];
        parent = child;
        child *= 2;
    }
    h->heap[parent] = temp;
    return item;
}

HeapType *prim(GraphType *g, HeapType *tree) {
    int i, s = 0, p = 0; //s-��������, p-�θ�����
    element tmp;
    tree->heap_size = 0;

    printf("----��� �Է�----\n");
    for (int k = 0; k < g->n; k++) {
        tmp.key = g->weight[s][k];
        tmp.selected = 0;
        tmp.self = k;
        tmp.parent = MAX_VERTICES;
        insert_min_heap(tree, tmp);
        printf("%d %d %d %d\n", tmp.self, tmp.key, tmp.parent, tmp.selected);
    }

    printf("\n----����ġ �ּҺ�����Ʈ�� ��ȭ�ܰ�----\n");
    printf("  ����ȣ, ����ġ, �θ���, ���ÿ���\n");
    for (int sta = 0; sta < g->n; sta++) {
        element *tmp_store = (element *)malloc(sizeof(element) * 4);
        HeapType *tree2 = (HeapType *)malloc(sizeof(HeapType) * 4);
        HeapType *tree_init = (HeapType *)malloc(sizeof(HeapType));
        tree2->heap_size = 0;
        int tmp_num = 0;
        for (int j = 0; j < g->n; j++) {
            tmp = delete_min_heap(tree);
            if (tmp.selected == 0 && tmp.key != INF)
                break;
            tmp_store[tmp_num++] = tmp;
        }
        tmp.selected = 1;
        tmp.parent = p;
        p = tmp.self;
        printf("\n");

        insert_min_heap(tree, tmp);
        for (int j = 0; j < tmp_num; j++) {
            insert_min_heap(tree, tmp_store[j]);
        }

        for (i = 0; i < g->n; i++) {
            element af = delete_min_heap(tree);
            insert_min_heap(tree2, af);
        }

        tree = tree2;
        tree2 = tree_init;
        for (i = 0; i < g->n; i++) {
            for (int j = 0; j < g->n; j++) {
                if (tree->heap[i + 1].self == j)
                    if (tree->heap[i + 1].key > g->weight[p][j])
                        if (g->weight[p][j] && tree->heap[i + 1].selected != 1)
                            tree->heap[i + 1].key = g->weight[p][j];
            }
        }

        for (i = 0; i < g->n; i++) {
            printf("%d %d %d %d | ", tree->heap[i + 1].self, tree->heap[i + 1].key,
                   tree->heap[i + 1].parent, tree->heap[i + 1].selected);
        }
    }
    return tree;
}

void print(HeapType *tree) {
    printf("\n\n-----���� ��� ����-----\n");
    for (int i = 0; i < tree->heap_size; i++) {
        printf("%d. �� : %d, ����ġ : %d\n", i, tree->heap[i + 1].self, tree->heap[i + 1].key);
    }
}

int main(void) {
    //1�� ����
    /*GraphType g = {7,
                   {{0, 29, INF, INF, INF, 10, INF},
                    {29, 0, 16, INF, INF, INF, 15},
                    {INF, 16, 0, 12, INF, INF, INF},
                    {INF, INF, 12, 0, 22, INF, 18},
                    {INF, INF, INF, 22, 0, 27, 25},
                    {10, INF, INF, INF, 27, 0, INF},
                    {INF, 15, INF, 18, 25, INF, 0}}};*/
    //2�� ����
    GraphType g = {8,
                   {{0, 10, INF, 6, INF, INF, INF, 1},
                    {10, 0, 4, INF, INF, 2, INF, INF},
                    {INF, 4, 0, 11, INF, 7, INF, INF},
                    {6, INF, 11, 0, INF, INF, INF, 3},
                    {INF, INF, INF, INF, 0, 5, INF, 8},
                    {INF, 2, 7, INF, 5, 0, 9, INF},
                    {INF, INF, INF, INF, INF, 9, 0, 12},
                    {1, INF, INF, 3, 8, INF, 12, 0}}};

    HeapType *tree = (HeapType *)malloc(sizeof(HeapType) * 4);
    tree = prim(&g, tree);
    print(tree);
    free(tree);
    return 0;
}