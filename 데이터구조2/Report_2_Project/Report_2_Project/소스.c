#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h> 
#include <string.h> 

typedef struct treenode {
    int weight;
    char ch;
    struct treenode* left;
    struct treenode* right;
}treenode;

typedef struct element {
    treenode* ptree;
    char ch;
    int key;
}element;

typedef struct heaptype {
    element heap[100];
    int heap_size;
}heaptype;

heaptype* create() {
    return (heaptype*)malloc(sizeof(heaptype));
}

void init(heaptype* h) {
    h->heap_size = 0;
}

treenode* make_tree(treenode* left, treenode* right) {
    treenode* node = (treenode*)malloc(sizeof(treenode));
    node->left = left;
    node->right = right;
    return node;
}

void destroy_tree(treenode* root) {
    if (root == NULL)
        return;
    destroy_tree(root->left);
    destroy_tree(root->right);
    free(root);
}

int is_leaf(treenode* root) {
    return !(root->left) && !(root->right);
}

element delete_min_heap(heaptype* h) {
    int parent = 1, child = 2;
    element item, temp;

    item = h->heap[1];
    temp = h->heap[(h->heap_size)--];
    while (child <= h->heap_size) {
        if ((child < h->heap_size) && h->heap[child].key > h->heap[child + 1].key)
            child++;
        if (temp.key < h->heap[child].key)
            break;
        h->heap[parent] = h->heap[child];
        parent = child;
        child += 2;
    }
    h->heap[parent] = temp;
    return item;
}

void insert_min_heap(heaptype* h, element item) {
    int i;
    i = ++(h->heap_size);
    while ((i != 1) && (item.key < h->heap[i / 2].key)) {
        h->heap[i] = h->heap[i / 2];
        i /= 2;
    }
    h->heap[i] = item;
}

void make_codes(treenode* root, int code[], int top, FILE* fp) {
    if (root->left) {
        code[top] = 1;
        make_codes(root->left, code, top + 1, fp);
    }
    if (root->right) {
        code[top] = 0;
        make_codes(root->right, code, top + 1, fp);
    }
    if (is_leaf(root)) {
        char set[100] = { 0 };
        char cc[4];
        printf("%c : ", root->ch);
        for (int i = 0; i < top; i++) {
            printf("%d", code[i]);
            sprintf(cc, "%d", code[i]);
            strcat(set, cc);
        }
        printf("\n");

        fprintf(fp, "%c %s\n", root->ch, set);
    }
}

void preOrder(treenode* root) {//����Ž��
    if (root != NULL) {
        printf("%d\n", root->weight);
        preOrder(root->left);
        preOrder(root->right);
    }
}
int main() {
    int fre[30] = { 0 };//�󵵼� ����
    char ch;//���� �б�

    FILE* fp_huff = fopen("huff.txt", "r");//huff.txt���� �б�
    printf("------���� ����-----\n");
    while (1) {//huff.txt���� ���
        char tmp = fgetc(fp_huff);
        printf("%c", tmp);
        if (tmp == EOF)
            break;
    }
    fseek(fp_huff, 0, SEEK_SET);//huff���� ������ �ʱ�ȭ

    printf("\n\n------�� ������ Ƚ��-----\n");
    do {//EOF�� ���ö����� ������ ���� �б�
        ch = fgetc(fp_huff);
        if (ch == ' ') {
            fre[26]++;//���� 26���ڸ� ����
            continue;
        }
        if (ch == '\n') {
            fre[27]++;//�ٹٲ� 27���ڸ� ����
            continue;
        }
        if (ch == '.') {
            fre[28]++;//��ħǥ 28���ڸ� ����
            continue;
        }
        if (ch == EOF) {
            fre[29]++;//EOF 29���ڸ� ����
            continue;
        }
        fre[ch - 'a']++;//a���� z���� ASCII�� �̿��Ͽ� 0~25���� ���� �Ҵ��Ͽ� �󵵼� ����

    } while (ch != EOF);//EOF�� ���ö����� ������ ���� �б�
    fclose(fp_huff);//huff.txt ����

    FILE* fp_freq = fopen("freq.txt", "w+");//freq.txt �������
    int line = 0;//�ؽ�Ʈ���� ���μ� üũ
    for (int i = 0; i < 30; i++) {//��ü ��� ���ڼ���ŭ �ݺ�, ���ڼ� ǥ��
        if (fre[i]) {//�ش� ���ڰ� 1���̻� �����ϸ� ����, ���ڹݺ��� ���
            line++;//�ؽ�Ʈ���� ���μ� ����
            if (i == 26) {
                printf("����(-)�� Ƚ�� : %d\n", fre[i]);//cmdȭ�����
                fprintf(fp_freq, "- %d\n", fre[i]);//ȭ�����
                continue;
            }
            if (i == 27) {
                printf("����(!)�� Ƚ�� : %d\n", fre[i]);
                fprintf(fp_freq, "! %d\n", fre[i]);
                continue;
            }
            if (i == 28) {
                printf("��ħǥ(.)�� Ƚ�� : %d\n", fre[i]);
                fprintf(fp_freq, ". %d\n", fre[i]);
                continue;
            }
            if (i == 29) {
                printf("EOF(+)�� Ƚ�� : %d\n", fre[i]);
                fprintf(fp_freq, "+ %d", fre[i]);
                continue;
            }
            printf("%c�� Ƚ�� : %d\n", 'a' + i, fre[i]);
            fprintf(fp_freq, "%c %d\n", i + 'a', fre[i]);
        }
    }

    fseek(fp_freq, 0, SEEK_SET);//freq���� ������ �ʱ�ȭ
    char alp2[30];//freq�� �ҷ��� ���� ���ĺ� �迭
    int fre2[30];//freq�� �ҷ��� ���� �󵵼� �迭
    for (int i = 0; i < line; i++)
        fscanf(fp_freq, "%c %d\n", &alp2[i], &fre2[i]);//���� �б�. ���ĺ�,�󵵼�

    treenode* node, * x;
    heaptype* heap;
    heap = create();
    init(heap);
    element e, e1, e2;
    //������
    for (int i = 0; i < line; i++) {
        node = make_tree(NULL, NULL);
        e.ch = node->ch = alp2[i];
        e.key = node->weight = fre2[i];
        e.ptree = node;
        insert_min_heap(heap, e);//�ּ����ķ� �����Ͽ� ����.
    }
    printf("\n----Ʈ�� ����-----\n");
    for (int i = 1; i < line; i++) {
        e1 = delete_min_heap(heap);
        e2 = delete_min_heap(heap);
        x = make_tree(e1.ptree, e2.ptree);
        e.key = x->weight = e1.key + e2.key;
        e.ptree = x;
        printf("%d + %d -> %d\n", e1.key, e2.key, e.key);
        insert_min_heap(heap, e);
    }

    printf("\n----Ʈ�� ���� Ž��-----\n");
    preOrder(e.ptree);

    int code[100], top = 0;
    element huffmancode;
    FILE* fp_codes = fopen("codes.txt", "w+");//codes.txt �������
    huffmancode = delete_min_heap(heap);
    printf("\n----������ �ڵ� ���-----\n");
    make_codes(huffmancode.ptree, code, top, fp_codes);//�������ڵ� ���
    fseek(fp_codes, 0, SEEK_SET);//huff���� ������ �ʱ�ȭ

    char hword[30], hcode[30][100];//������ �ڵ带 �迭�� ����
    for (int i = 0; i < line; i++) {
        fscanf(fp_codes, "%c %s\n", &hword[i], hcode[i]);
    }

    //������ ���ڵ�
    FILE* fp_msg = fopen("msg.txt", "r");//�޼������� �б�
    FILE* fp_encodedMsg = fopen("encodedMsg.txt", "w+");//���ڵ����� ����
    char a, compare, encmsg[10000] = { 0 };//txt������ ���� �޾ƿ���, �񱳴��, ���ڵ��ȸ޼���
    do {
        a = fgetc(fp_msg);
        if (a == ' ') {
            compare = '-';
            for (int i = 0; i < line; i++) {
                if (hword[i] == compare)
                    strcat(encmsg, hcode[i]);
            }
            continue;
        }
        if (a == '\n') {
            compare = '!';
            for (int i = 0; i < line; i++) {
                if (hword[i] == compare)
                    strcat(encmsg, hcode[i]);
            }
            continue;
        }
        if (a == EOF) {
            compare = '+';
            for (int i = 0; i < line; i++) {
                if (hword[i] == compare)
                    strcat(encmsg, hcode[i]);
            }
            continue;
        }
        compare = a;
        for (int i = 0; i < line; i++) {
            if (hword[i] == compare) {
                strcat(encmsg, hcode[i]);
                continue;
            }
        }
    } while (a != EOF);
    fprintf(fp_encodedMsg, "%s", encmsg);

    printf("\n-----msg.txt ���ڵ� ���-----\n%s\n\n%s\n", encmsg, "-----���ڵ� ����-----");
    //������ ���ڵ�
    fseek(fp_encodedMsg, 0, SEEK_SET);
    char before_decmsg[10000] = { 0 };
    fscanf(fp_encodedMsg, "%s", &before_decmsg);

    treenode* plot = e.ptree;
    fseek(fp_encodedMsg, 0, SEEK_SET);
    for (int i = 0; i < strlen(before_decmsg) + 1; i++) {
        char codeone = fgetc(fp_encodedMsg);
        if (is_leaf(plot)) {
            printf("%c", plot->ch);
            plot = e.ptree;
        }
        if (codeone == '0')
            plot = plot->right;
        else if (codeone == '1')
            plot = plot->left;
    }
    //Ư����ȣ ����
    printf("\n\n-----Ư����ȣ ����-----\n");
    plot = e.ptree;
    fseek(fp_encodedMsg, 0, SEEK_SET);
    for (int i = 0; i < strlen(before_decmsg) + 1; i++) {
        char codeone = fgetc(fp_encodedMsg);
        if (is_leaf(plot)) {
            if (plot->ch == '-')
                printf(" ");
            else if (plot->ch == '!')
                printf("\n");
            else if (plot->ch == '+')
                printf("��");
            else
                printf("%c", plot->ch);
            plot = e.ptree;
        }
        if (codeone == '0')
            plot = plot->right;
        else if (codeone == '1')
            plot = plot->left;
    }

    fclose(fp_codes);
    fclose(fp_encodedMsg);
    fclose(fp_freq);
    fclose(fp_huff);
    fclose(fp_msg);

    return 0;
}
