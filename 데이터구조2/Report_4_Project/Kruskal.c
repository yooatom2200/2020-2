#define _CRT_SECURE_NO_WARNINGS
#define MAX_VERTICES 100
#define INF 1000
#include <stdio.h>
#include <stdlib.h>

int parent[MAX_VERTICES];

void set_init(int n) {
    for (int i = 0; i < n; i++)
        parent[i] = -1;
}

// curr�� ���ϴ� ������ ��ȯ�Ѵ�
int set_find(int curr) {
    if (parent[curr] == -1)
        return curr;
    while (parent[curr] != -1)
        curr = parent[curr];
    return curr;
}

// �ΰ��� ���Ӱ� ���� ������ ��ģ��.
void set_union(int a, int b) {
    int root1 = set_find(a);
    int root2 = set_find(b);
    if (root1 != root2)
        parent[root1] = root2;
}

struct Edge { // ������ ��Ÿ���� ����ü
    int start, end, weight;
};

typedef struct GraphType {
    int v; // ������ ����
    int n; // ������ ����
    struct Edge edges[MAX_VERTICES];
} GraphType;

// �׷��� �ʱ�ȭ
void graph_init(GraphType *g) {
    g->v = 0;
    g->n = 0;
    for (int i = 0; i < MAX_VERTICES; i++) {
        g->edges[i].start = 0;
        g->edges[i].end = 0;
        g->edges[i].weight = INF;
    }
}

// �������Կ���
void insert_vertex(GraphType *g) {
    if (((g->v) + 1) > MAX_VERTICES) {
        fprintf(stderr, "�׷��� : ������ ���� �ʰ�");
        return;
    }
    g->v++;
}

// ���� ���� ����
void insert_edge(GraphType *g, int start, int end, int w) {
    if (start >= g->v || end >= g->v || start < 0 || end < 0) {
        fprintf(stderr, "�׷��� : ���� ��ȣ ����\n");
        return;
    }
    g->edges[g->n].start = start;
    g->edges[g->n].end = end;
    g->edges[g->n].weight = w;
    g->n++;
}

void gen_graph(GraphType *g) {
    int tmp_v, max_n = 0;
    printf("������ ������ �Է��� �ּ��� : ");
    scanf("%d", &tmp_v);
    if (tmp_v <= 1 || tmp_v > MAX_VERTICES)
        printf("�Է¿���");

    for (int i = 0; i < tmp_v; i++)
        insert_vertex(g); //���� �Է�

    for (int i = 0; i <= tmp_v - 1; i++)
        max_n += i; //�ִ� ���� ����

    for (int i = 0; i < max_n; i++) {
        int a, b, c;
        printf("������ �� ������ ����� �Է��� �ּ���(��� = 0 0 0) : ");
        scanf("%d %d %d", &a, &b, &c);
        if (a == 0 && b == 0 && c == 0)
            break;
        insert_edge(g, a, b, c);
    }
}

typedef struct {
    int key; // ������ ����ġ
    int u;   // ���� 1
    int v;   // ���� 2
} element;

typedef struct {
    element heap[MAX_VERTICES];
    int heap_size;
} HeapType;

// �ʱ�ȭ �Լ�
void init(HeapType *h) {
    h->heap_size = 0;
}

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
            (h->heap[child].key) > h->heap[child + 1].key)
            child++;
        if (temp.key <= h->heap[child].key)
            break;
        // �Ѵܰ� �Ʒ��� �̵�
        h->heap[parent] = h->heap[child];
        parent = child;
        child *= 2;
    }
    h->heap[parent] = temp;
    return item;
}

void insert_heap_edge(HeapType *h, int u, int v, int weight) {
    element e;
    e.u = u;
    e.v = v;
    e.key = weight;
    insert_min_heap(h, e);
}

// kruskal�� �ּҺ�� ����Ʈ�� ���α׷�
element *Kruskal(GraphType *g) {
    printf("kruskal ����\n");

    static element tmp[MAX_VERTICES];
    int heap_tmp = 0, uset, vset;
    element e;
    HeapType *h; // �ּ� ����
    h = (HeapType *)malloc(sizeof(HeapType));

    init(h); // ���� �ʱ�ȭ
    for (int i = 0; i < g->n; i++)
        insert_heap_edge(h, g->edges[i].start, g->edges[i].end, g->edges[i].weight);
    set_init(g->n);

    for (int i = 0; i < g->n; i++) {
        e = delete_min_heap(h); // �ּ� �������� ����
        uset = set_find(e.u);   // ���� u�� ���� ��ȣ
        vset = set_find(e.v);   // ���� v�� ���� ��ȣ
        if (uset != vset) {     // ���� ���� ������ �ٸ���
            tmp[heap_tmp++] = e;
            set_union(uset, vset); // �ΰ��� ������ ��ģ��.
        }
    }
    free(h);
    return tmp;
}

void print(element T[]) { //���
    printf("��� ���\n");
    for (int i = 0; i < MAX_VERTICES; i++) {
        if (T[i].key == 0 && T[i].u == 0 && T[i].v == 0)
            break;
        printf("(%d, %d), %d\n", T[i].v, T[i].u, T[i].key);
    }
    printf("��� ����\n");
}

int main() {
    GraphType *g;
    g = (GraphType *)malloc(sizeof(GraphType)); //�׷�������
    graph_init(g);                              //�׷��� �ʱ�ȭ
    gen_graph(g);                               //�׷��� ������ �Է�
    element *T = Kruskal(g);
    print(T);
    free(g);
    return 0;
}