#pragma warning(disable: 4996)

#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define TRUE 1
#define FALSE 0
#define MAX_VERTICES 100
#define INF 1000000 //���Ѵ�. (������ ���� ���)

//�迭������ ���� ������ �̸� ���
void ret_name(int i) {
    switch (i) {
    case 0:
        printf("��â������");
        break;
    case 1:
        printf("���̼�");
        break;
    case 2:
        printf("�����");
        break;
    case 3:
        printf("�����Ϸ���");
        break;
    case 4:
        printf("�߱���");
        break;
    case 5:
        printf("�Ե�����");
        break;
    case 6:
        printf("���и�Ʈ");
        break;
    case 7:
        printf("����Į����");
        break;
    case 8:
        printf("��������");
        break;
    case 9:
        printf("��Ƽ�̵���");
        break;
    default :
        printf("Error!");
        break;
    }
}

//�迭 ������ ���� �������� ������ȣ�� ���
int ret_num(char* a) {
    if (strcmp(a,"��â������") == 0) return 0;
    else if (strcmp(a, "���̼�") == 0) return 1;
    else if (strcmp(a, "�����") == 0) return 2;
    else if (strcmp(a, "�����Ϸ���") == 0) return 3;
    else if (strcmp(a, "�߱���") == 0) return 4;
    else if (strcmp(a, "�Ե�����") == 0) return 5;
    else if (strcmp(a, "���и�Ʈ") == 0) return 6;
    else if (strcmp(a, "����Į����") == 0) return 7;
    else if (strcmp(a, "��������") == 0) return 8;
    else if (strcmp(a, "��Ƽ�̵���") == 0) return 9;
    else printf("error!");
    return;
}

typedef struct GraphType {
    int n; //������ ����
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
            printf(" ���� ");
            ret_name(u);
            printf(" ������ ��Ʈ : ");
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
            printf("\t| ��� : % d\n", distance[u]);

            for (int k = 0; k < g->n; k++) {
                distance[k] = g->weight[start][k];
                found[k] = FALSE;
            }
        }
        found[u] = TRUE;
        for (w = 0; w < g->n; w++) {//����ġ���
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
        printf("\n��������� �̸��� �Է��� �ּ���!(��� = -1) : ");
        printf("\n(��â������, ���̼�, �����, �����Ϸ���, �߱���)\n(�Ե�����, ���и�Ʈ, ����Į����, ��������, ��Ƽ�̵���) : ");
        scanf("%s", start);
        if (strcmp(start, "-1") == 0)
            break;

        printf("\n���������� �̸��� �Է��� �ּ���(��� = -1) : ");
        printf("\n(��â������, ���̼�, �����, �����Ϸ���, �߱���)\n(�Ե�����, ���и�Ʈ, ����Į����, ��������, ��Ƽ�̵���) : ");
        scanf("%s", end);
        if (strcmp(end, "-1") == 0)
            break;

        //�Է¿���Ȯ��
        if (strcmp(start, end) == 0) {
            printf("\n���� ���� �����ϼ̽��ϴ�. ���� �ٸ� ���� �Է��� �ּ���!\n");
        }
        else if ((strcmp(start, "��â������") == 0 ||
            strcmp(start, "���̼�") == 0 ||
            strcmp(start, "�����") == 0 ||
            strcmp(start, "�����Ϸ���") == 0 ||
            strcmp(start, "�߱���") == 0 ||
            strcmp(start, "�Ե�����") == 0 ||
            strcmp(start, "���и�Ʈ") == 0 ||
            strcmp(start, "����Į����") == 0 ||
            strcmp(start, "��������") == 0 ||
            strcmp(start, "��Ƽ�̵���") == 0) &&
            (strcmp(end, "��â������") == 0 ||
                strcmp(end, "���̼�") == 0 ||
                strcmp(end, "�����") == 0 ||
                strcmp(end, "�����Ϸ���") == 0 ||
                strcmp(end, "�߱���") == 0 ||
                strcmp(end, "�Ե�����") == 0 ||
                strcmp(end, "���и�Ʈ") == 0 ||
                strcmp(end, "����Į����") == 0 ||
                strcmp(end, "��������") == 0 ||
                strcmp(end, "��Ƽ�̵���") == 0))
            shortest_path(&g, ret_num(start), ret_num(end));
        else
            printf("\n��Ȯ�� ������ �Է��� �ּ���!\n");
    }
    return 0;
}
