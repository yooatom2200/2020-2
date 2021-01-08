#include <limits.h>
#include <stdio.h>
#include <stdlib.h>

#define TRUE 1
#define FALSE 0
#define MAX_VERTICES 100
#define INF 1000000 //무한대. (연결이 없는 경우)

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

void shortest_path(GraphType *g) {
    int i, u, w, start;
    for (int s = 0; s < g->n; s++) {
        start = s;
        for (i = 0; i < g->n; i++) {
            distance[i] = g->weight[start][i];
            found[i] = FALSE;
            if (g->weight[start][i] != INF)
                trace[i] = start;
        }
        distance[start] = 0;
        for (int tmp = 0; tmp < g->n; tmp++) {
            for (i = 0; i < g->n; i++) {
                u = choose(distance, g->n, found);
                if (tmp == u) {
                    if (start == u)
                        continue;
                    printf("%d 에서 %d 까지 루트 : %d", start, u, start);
                    int n_route = trace[u];
                    int q = 0;
                    while (1) {
                        if (n_route == start) {
                            while (q > 0) {
                                printf("-%d", r_trace[--q]);
                            }
                            break;
                        }
                        r_trace[q++] = n_route;
                        n_route = trace[n_route];
                    }
                    printf("-%d \t비용 : %d\n", u, distance[u]);
                    for (int k = 0; k < g->n; k++) {
                        distance[k] = g->weight[start][k];
                        found[k] = FALSE;
                    }
                    break;
                }
                found[u] = TRUE;
                for (w = 0; w < g->n; w++) {
                    if (!found[w]) {
                        if (distance[u] + g->weight[u][w] < distance[w]) {
                            distance[w] = distance[u] + g->weight[u][w];
                            trace[w] = u;
                        }
                    }
                }
            }
        }
        printf("\n%d번시작 출력 종료-----------------------------\n\n", s);
    }
}

int main(void) {
    GraphType g = {7,
                   {{0, 7, INF, INF, 3, 10, INF},
                    {7, 0, 4, 10, 2, 6, INF},
                    {INF, 4, 0, 2, INF, INF, INF},
                    {INF, 10, 2, 0, 11, 9, 4},
                    {3, 2, INF, 11, 0, INF, 5},
                    {10, 6, INF, 9, INF, 0, INF},
                    {INF, INF, INF, 4, 5, INF, 0}}};
    shortest_path(&g);
    return 0;
}