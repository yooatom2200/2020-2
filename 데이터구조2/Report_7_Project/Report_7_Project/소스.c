#pragma warning(disable: 4996)

#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define TRUE 1
#define FALSE 0
#define MAX_VERTICES 100
#define INF 1000000 //무한대. (연결이 없는 경우)

//배열순서에 따른 목적지 이름 출력
void ret_name(int i) {
    switch (i) {
    case 0:
        printf("신창정류소");
        break;
    case 1:
        printf("다이소");
        break;
    case 2:
        printf("자취방");
        break;
    case 3:
        printf("세븐일레븐");
        break;
    case 4:
        printf("중국집");
        break;
    case 5:
        printf("롯데리아");
        break;
    case 6:
        printf("대학마트");
        break;
    case 7:
        printf("밀터칼국수");
        break;
    case 8:
        printf("신한은행");
        break;
    case 9:
        printf("멀티미디어관");
        break;
    default :
        printf("Error!");
        break;
    }
}

//배열 순서에 따른 목적지의 순서번호값 출력
int ret_num(char* a) {
    if (strcmp(a,"신창정류소") == 0) return 0;
    else if (strcmp(a, "다이소") == 0) return 1;
    else if (strcmp(a, "자취방") == 0) return 2;
    else if (strcmp(a, "세븐일레븐") == 0) return 3;
    else if (strcmp(a, "중국집") == 0) return 4;
    else if (strcmp(a, "롯데리아") == 0) return 5;
    else if (strcmp(a, "대학마트") == 0) return 6;
    else if (strcmp(a, "밀터칼국수") == 0) return 7;
    else if (strcmp(a, "신한은행") == 0) return 8;
    else if (strcmp(a, "멀티미디어관") == 0) return 9;
    else printf("error!");
    return;
}

typedef struct GraphType {
    int n; //정점의 개수
    int weight[MAX_VERTICES][MAX_VERTICES];
} GraphType;

int distance[MAX_VERTICES];
int found[MAX_VERTICES];

int trace[MAX_VERTICES];
int r_trace[MAX_VERTICES];

int choose(int distance[], int n, int found[]) {
    int i, min, minpos;
    min = INT_MAX;
    minpos = -1;
    for (i = 0; i < n; i++) {
        if (distance[i] < min && !found[i]) {
            min = distance[i];
            minpos = i;
        }
    }
    return minpos;
}

void shortest_path(GraphType* g, int start, int end) {
    int i, u, w;

    for (i = 0; i < g->n; i++) {
        distance[i] = g->weight[start][i];
        found[i] = FALSE;
        if (g->weight[start][i] != INF)
            trace[i] = start;
    }
    distance[start] = 0;
    for (i = 0; i < g->n; i++) {
        u = choose(distance, g->n, found);
        if (end == u) {
            if (start == u)
                continue;
            printf("\n");
            ret_name(start);
            printf(" 에서 ");
            ret_name(u);
            printf(" 까지의 루트 : ");
            ret_name(start);

            int n_route = trace[u];
            int q = 0;
            while (1) {
                if (n_route == start) {
                    while (q > 0) {
                        printf("-");
                        ret_name(r_trace[--q]);

                    }
                    break;
                }
                r_trace[q++] = n_route;
                n_route = trace[n_route];
            }
            printf("-");
            ret_name(u);
            printf("\t| 비용 : % d\n", distance[u]);

            for (int k = 0; k < g->n; k++) {
                distance[k] = g->weight[start][k];
                found[k] = FALSE;
            }
        }
        found[u] = TRUE;
        for (w = 0; w < g->n; w++) {//가중치계산
            if (!found[w]) {
                if (distance[u] + g->weight[u][w] < distance[w]) {
                    distance[w] = distance[u] + g->weight[u][w];
                    trace[w] = u;
                }
            }
        }
    }
}

int main(void) {
    GraphType g = { 10,
                   {{0, 85, 100, INF, INF, INF, INF, INF, INF, INF},
                    {85, 0, 140, INF, INF, INF, INF, INF, INF, INF},
                    {100, 140, 0, 220, 180, INF, INF, INF, INF, INF},
                    {INF, INF, 220, 0, INF, INF, INF, 210, INF, INF},
                    {INF, INF, 180, INF, 0, 80, 77, INF, INF, INF},
                    {INF, INF, INF, INF, 80, 0, INF, INF, INF, INF},
                    {INF, INF, INF, INF, 77, INF, 0, 232, 324, 515},
                    {INF, INF, INF, 210, INF, INF, 232, 0, INF, 319},
                    {INF, INF, INF, INF, INF, INF, 324, INF, 0, 254},
                    {INF, INF, INF, INF, INF, INF, 515, 319, 254, 0}} };
    char start[100], end[100];
    while (1) {
        printf("\n출발지점의 이름을 입력해 주세요!(취소 = -1) : ");
        printf("\n(신창정류소, 다이소, 자취방, 세븐일레븐, 중국집)\n(롯데리아, 대학마트, 밀터칼국수, 신한은행, 멀티미디어관) : ");
        scanf("%s", start);
        if (strcmp(start, "-1") == 0)
            break;

        printf("\n도착지점의 이름을 입력해 주세요(취소 = -1) : ");
        printf("\n(신창정류소, 다이소, 자취방, 세븐일레븐, 중국집)\n(롯데리아, 대학마트, 밀터칼국수, 신한은행, 멀티미디어관) : ");
        scanf("%s", end);
        if (strcmp(end, "-1") == 0)
            break;

        //입력오류확인
        if (strcmp(start, end) == 0) {
            printf("\n같은 곳을 선택하셨습니다. 서로 다른 곳을 입력해 주세요!\n");
        }
        else if ((strcmp(start, "신창정류소") == 0 ||
            strcmp(start, "다이소") == 0 ||
            strcmp(start, "자취방") == 0 ||
            strcmp(start, "세븐일레븐") == 0 ||
            strcmp(start, "중국집") == 0 ||
            strcmp(start, "롯데리아") == 0 ||
            strcmp(start, "대학마트") == 0 ||
            strcmp(start, "밀터칼국수") == 0 ||
            strcmp(start, "신한은행") == 0 ||
            strcmp(start, "멀티미디어관") == 0) &&
            (strcmp(end, "신창정류소") == 0 ||
                strcmp(end, "다이소") == 0 ||
                strcmp(end, "자취방") == 0 ||
                strcmp(end, "세븐일레븐") == 0 ||
                strcmp(end, "중국집") == 0 ||
                strcmp(end, "롯데리아") == 0 ||
                strcmp(end, "대학마트") == 0 ||
                strcmp(end, "밀터칼국수") == 0 ||
                strcmp(end, "신한은행") == 0 ||
                strcmp(end, "멀티미디어관") == 0))
            shortest_path(&g, ret_num(start), ret_num(end));
        else
            printf("\n정확한 지명을 입력해 주세요!\n");
    }
    return 0;
}
