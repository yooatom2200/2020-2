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
        print(str(i) + "\n")
        if i not in [0, 1, 2]:
            result.append(self.get_new_board(i, i - 3, moves))
        if i not in [0, 3, 6]:
            result.append(self.get_new_board(i, i - 1, moves))
        if i not in [2, 5, 8]:
            result.append(self.get_new_board(i, i + 1, moves))
        if i not in [6, 7, 8]:
            result.append(self.get_new_board(i, i + 3, moves))
        return result

    def __str__(self):
        return str(self.board[:3]) + "\n" + str(self.board[3:6]) + "\n" + str(self.board[6:9]) + "\n" + "------------------"

#초기 