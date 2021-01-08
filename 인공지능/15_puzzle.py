class State:
    def __init__(self, board, goal, moves = 0):
        self.board = board
        self.moves = moves
        self.goal = goal

    def get_new_board(self, i1, i2 ,moves):
        new_board = self.board[:]
        new_board[i1], new_board[i2] = new_board[i2], new_board[i1]
        return State(new_board, self.goal, moves)

    def expand(self, moves):
        result = []
        i = self.board.index(0)
        if i not in [0, 1, 2, 3]:
            result.append(self.get_new_board(i, i - 4, moves))
        if i not in [0, 4, 8, 12]:
            result.append(self.get_new_board(i, i - 1, moves))
        if i not in [3, 7, 11, 15]:
            result.append(self.get_new_board(i, i + 1, moves))
        if i not in [12, 13, 14, 15]:
            result.append(self.get_new_board(i, i + 4, moves))
        return result

    def __str__(self):
        return str(self.board[:4]) + "\n" + str(self.board[4:8]) + "\n" + str(self.board[8:12]) + "\n" + str(self.board[12:16]) + "\n" + "------------------"

#초기 상태
puzzle = [1,2,3,4,5,6,7,8,9,10,11,12,0,13,14,15]
#목표 상태
goal = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,0]

open_queue = []
open_queue.append(State(puzzle,goal))
closed_queue = []
moves = 0

while len(open_queue) != 0:
    current = open_queue.pop(0)
    print(current)
    if(current.board == goal): #목표 상태 확인
        print("탐색 성공")
        break
    moves = current.moves+1
    closed_queue.append(moves)
    for state in current.expand(moves):
        if(state in closed_queue) or (state in open_queue):
            continue
        else:
            open_queue.append(state)