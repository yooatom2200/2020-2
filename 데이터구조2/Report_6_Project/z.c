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
    int n; // 정점의 개수
    int weight[MAX_VERTICES][MAX_VERTICES];
} GraphType;

typedef struct heap {
    element heap[MAX_VERTICES];
    int heap_size;
} heap; //힙타입 정의

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
        printf("---------정점 %d 가 기준-------\n", i);    //부터 모든 정점과의 거리 출력
        int *distance = (int *)malloc(sizeof(int) * g.n);  //노드의 거리 정보
        int *selelcted = (int *)malloc(sizeof(int) * g.n); //마지막 선택된 노드
        dijkstra(&g, distance, selelcted, i);              //다익스트라 함수 호출
        printVisitedNode(&g, distance, selelcted);         //정점 출력 함수 호출
        free(distance);                                    //노드 거리정보 메모리 운영체제에 반환
        free(selelcted);                                   //마지막 방문 노드 메모리 운영체제 반환
        printf("-----------------\n");
    }
}

void heap_init(heap_ptr h) { //힙 초기화 함수
    h->heap_size = 0;        //힙의 크기를 0으로 설정
}

heap_ptr create_heap() {                         //힙 생성 함수
    heap_ptr h = (heap_ptr)malloc(sizeof(heap)); //동적할당으로 힙 생성후 포인터 반환
    heap_init(h);
    return h;
}

void insert_min_heap(heap_ptr h, element item) { //최소힙 삽입
    if ((h->heap_size + 1) == (MAX_VERTICES)) {  //힙을 더이상 삽입 불가능하면
        printf("더이상 힙에 삽입할수 없습니다!");
        exit(0); //프로그램 종료
    }

    int i = ++(h->heap_size); //힙이 삽입되므로 1 추가
    //맨 아래에 삽입해야 함으로 i 는 최하단 위치
    while (i != 1 && item.weight < h->heap[i / 2].weight) {
        //부모가 더 크면 교환을 계속 진행한다. 여기선 가중치가 key
        h->heap[i] = h->heap[i / 2];
        i /= 2;
    }
    h->heap[i] = item; //정위치를 찾았으면 데이터를 삽입
}

int is_empty(heap_ptr h) {
    return (h->heap_size == 0);
}

element delete_min_heap(heap_ptr h) {             //최소힙 삭제
    if (h->heap_size == 0) {                      //힙을 삭제할수 없으면
        printf("더이상 힙을 삭제할수 없습니다!"); //오류, 프로그램 종료
        exit(0);
    }
    int parent, child; //부모 자식 노드의 인덱스
    element item, temp;
    item = h->heap[1];                //삭제 당할 노드
    temp = h->heap[(h->heap_size)--]; //상위로 올릴노드
    //삭제가 되면 노드가 한개 삭제됨으로, h->heap_size 에 1을 뺀다.
    parent = 1;                                                                          //부모 노드의 위치
    child = 2;                                                                           //자식 노드의 위치. paret *2 가 자식노드임
    while (child <= h->heap_size) {                                                      //자식노드는 힙에 저장된 값을 넘어가면 종료해야함.
        if (child < h->heap_size && h->heap[child].weight > h->heap[child + 1].weight) { //가중치가 여기선 key
                                                                                         //자식 노드중, 우측 노드가 더 작으면 1을 더하여 우측노드로 설정
            child++;
        }
        if (temp.weight <= h->heap[child].weight) { //임시 노드가 제 위치를 찾아갔으면, 종료
            break;
        }
        h->heap[parent] = h->heap[child]; //부모 노드랑 자식 노드랑 교환
        parent = child;                   //부모 노드랑 자식 노드랑 교환
        child *= 2;
    }
    h->heap[parent] = temp; //현재 부모 위치에 temp 를 씌워서 값을 정상적으로 설정
    return item;            //반환할 데이터 반환
}

void dijkstra(GraphType *g, int *distances, int *selected, int start) {
    heap_ptr h = create_heap();
    for (int i = 0; i < g->n; i++) { //거리 저장 배열 초기화
        distances[i] = INF;
    }
    distances[start] = 0;    //시작점 - 시작점 거리는 0
    selected[start] = start; //선택 정점 =
    element e = {start, distances[start]};
    insert_min_heap(h, e);                                    //현재 이어진 정점인 start를 우선순위 큐에 삽입
    while (!is_empty(h)) {                                    //힙이 비어있지 않을때 까지 반복
        element delelted = delete_min_heap(h);                //힙에서 거리가 최소인 정점과 거리를 가져옴
        int current_distance = delelted.weight;               //가져온 거리
        int current_node = delelted.node;                     //가져온 노드
        for (int adjacent = 0; adjacent < g->n; adjacent++) { //인접정점
            if (g->weight[current_node][adjacent] == INF || g->weight[current_node][adjacent] == 0) {
                //인접 정점이 아닌경우는 가중치가 무한이거나, 자기 자신인 경우
                continue; //인접 정점이 아니므로 다른 인접 정점을 찾음
            }

            int weight = g->weight[current_node][adjacent];
            int distance = current_distance + weight; //힙에서 가져온 거리 + 인접정점의 거리 즉, 인접정점 경유한 길이
            if (distance < distances[adjacent]) {     //인접정점 경유 길이가 배열에 저장된것보다 작다면
                distances[adjacent] = distance;       //배열 값 업데이트
                element t = {adjacent, distance};     //큐에 정점과 거리 삽입
                insert_min_heap(h, t);
                selected[adjacent] = current_node; //마지막 방문 노드 업데이트
            }
        }
    }
    free(h);
}

void printVisitedNode(GraphType *g, int *distances, int *selected) {
    printf("정점 | 경로 (거리) | [최단거리]\n");
    printf("===============\n");
    for (int i = 0; i < g->n; i++) {
        int now = i;
        printf("정점 %d :", i);                                         //현재 정점 출력
        while (g->weight[now][selected[now]] != 0) {                    //다음 정점의 가중치가 0이면
            printf("%d - (%d) - ", now, g->weight[now][selected[now]]); //현재 정점과 다음 정점의 가중치 출력
            now = selected[now];                                        //현재 정점은 현재정점이 마지막에 방문한 정점으로 수정
        }
        printf("%d | [%d]\n", now, distances[i]); //마지막 정점 출
    }
}