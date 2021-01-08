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

// curr가 속하는 집합을 반환한다
int set_find(int curr) {
    if (parent[curr] == -1)
        return curr;
    while (parent[curr] != -1)
        curr = parent[curr];
    return curr;
}

// 두개의 원속가 속한 집합을 합친다.
void set_union(int a, int b) {
    int root1 = set_find(a);
    int root2 = set_find(b);
    if (root1 != root2)
        parent[root1] = root2;
}

struct Edge { // 간선을 나타내는 구조체
    int start, end, weight;
};

typedef struct GraphType {
    int v; // 정점의 개수
    int n; // 간선의 개수
    struct Edge edges[MAX_VERTICES];
} GraphType;

// 그래프 초기화
void graph_init(GraphType *g) {
    g->v = 0;
    g->n = 0;
    for (int i = 0; i < MAX_VERTICES; i++) {
        g->edges[i].start = 0;
        g->edges[i].end = 0;
        g->edges[i].weight = INF;
    }
}

// 정점삽입연산
void insert_vertex(GraphType *g) {
    if (((g->v) + 1) > MAX_VERTICES) {
        fprintf(stderr, "그래프 : 정점의 개수 초과");
        return;
    }
    g->v++;
}

// 간섭 삽입 연산
void insert_edge(GraphType *g, int start, int end, int w) {
    if (start >= g->v || end >= g->v || start < 0 || end < 0) {
        fprintf(stderr, "그래프 : 정점 번호 오류\n");
        return;
    }
    g->edges[g->n].start = start;
    g->edges[g->n].end = end;
    g->edges[g->n].weight = w;
    g->n++;
}

void gen_graph(GraphType *g) {
    int tmp_v, max_n = 0;
    printf("정점의 개수를 입력해 주세요 : ");
    scanf("%d", &tmp_v);
    if (tmp_v <= 1 || tmp_v > MAX_VERTICES)
        printf("입력오류");

    for (int i = 0; i < tmp_v; i++)
        insert_vertex(g); //간선 입력

    for (int i = 0; i <= tmp_v - 1; i++)
        max_n += i; //최대 간선 개수

    for (int i = 0; i < max_n; i++) {
        int a, b, c;
        printf("간선의 각 정점과 비용을 입력해 주세요(취소 = 0 0 0) : ");
        scanf("%d %d %d", &a, &b, &c);
        if (a == 0 && b == 0 && c == 0)
            break;
        insert_edge(g, a, b, c);
    }
}

typedef struct {
    int key; // 간선의 가중치
    int u;   // 정점 1
    int v;   // 정점 2
} element;

typedef struct {
    element heap[MAX_VERTICES];
    int heap_size;
} HeapType;

// 초기화 함수
void init(HeapType *h) {
    h->heap_size = 0;
}

// 삽입 함수
void insert_min_heap(HeapType *h, element item) {
    int i;
    i = ++(h->heap_size);
    //  트리를 거슬러 올라가면서 부모 노드와 비교하는 과정
    while ((i != 1) && (item.key < h->heap[i / 2].key)) {
        h->heap[i] = h->heap[i / 2];
        i /= 2;
    }
    h->heap[i] = item; // 새로운 노드를 삽입
}
// 삭제 함수
element delete_min_heap(HeapType *h) {
    int parent, child;
    element item, temp;
    item = h->heap[1];
    temp = h->heap[(h->heap_size)--];
    parent = 1;
    child = 2;
    while (child <= h->heap_size) {
        // 현재 노드의 자식노드중 더 작은 자식노드를 찾는다.
        if ((child < h->heap_size) &&
            (h->heap[child].key) > h->heap[child + 1].key)
            child++;
        if (temp.key <= h->heap[child].key)
            break;
        // 한단계 아래로 이동
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

// kruskal의 최소비용 신장트리 프로그램
element *Kruskal(GraphType *g) {
    printf("kruskal 시작\n");

    static element tmp[MAX_VERTICES];
    int heap_tmp = 0, uset, vset;
    element e;
    HeapType *h; // 최소 히프
    h = (HeapType *)malloc(sizeof(HeapType));

    init(h); // 히프 초기화
    for (int i = 0; i < g->n; i++)
        insert_heap_edge(h, g->edges[i].start, g->edges[i].end, g->edges[i].weight);
    set_init(g->n);

    for (int i = 0; i < g->n; i++) {
        e = delete_min_heap(h); // 최소 히프에서 삭제
        uset = set_find(e.u);   // 정점 u의 집합 번호
        vset = set_find(e.v);   // 정점 v의 집합 번호
        if (uset != vset) {     // 서로 속한 집합이 다르면
            tmp[heap_tmp++] = e;
            set_union(uset, vset); // 두개의 집합을 합친다.
        }
    }
    free(h);
    return tmp;
}

void print(element T[]) { //출력
    printf("결과 출력\n");
    for (int i = 0; i < MAX_VERTICES; i++) {
        if (T[i].key == 0 && T[i].u == 0 && T[i].v == 0)
            break;
        printf("(%d, %d), %d\n", T[i].v, T[i].u, T[i].key);
    }
    printf("출력 종료\n");
}

int main() {
    GraphType *g;
    g = (GraphType *)malloc(sizeof(GraphType)); //그래프생성
    graph_init(g);                              //그래프 초기화
    gen_graph(g);                               //그래프 데이터 입력
    element *T = Kruskal(g);
    print(T);
    free(g);
    return 0;
}