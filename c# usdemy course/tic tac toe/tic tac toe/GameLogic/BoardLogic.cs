namespace GameLogic
{
    public class Board
    {
        public bool Player1Turn { get; private set; }
        private readonly Cell[,] board;

        public Board()
        {
            board = new Cell[3, 3];
            ResetBoard();
            Player1Turn = true;

        }
        /// <summary>
        /// if set cell was succsessful the the return value is true, otherwise false
        /// if its secssessful the cell wa set .
        /// </summary>
        /// <param name="i">coordinate</param>
        /// <param name="j">coordinate</param>
        /// <param name="cell"></param>
        /// <returns></returns>

        public bool SetCell(int i, int j)
        {
            if (i < 3 && j < 3)
            {
                if (board[i, j] == Cell.EMPTY)
                {
                    board[i, j] = Player1Turn ? Cell.X : Cell.O;
                    Player1Turn = Player1Turn ? false : true;
                    return true;
                }
            }
            return false;
        }

       

       public bool isWinner()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if ((board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    && (board[i, 0] == Cell.X|| board[i, 0] == Cell.O))
                {
                    return true;
                }
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i]
                    && (board[0, i] == Cell.X || board[0, i] == Cell.O))
                {
                    return true;
                }

            }
            // Check main diagonal (top-left to bottom-right)
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]
                && (board[1, 1] == Cell.X || board[1, 1] == Cell.O))
            {
                return true;
            }

            // Check other diagonal (top-right to bottom-left)
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]
                && (board[0, 2] == Cell.X || board[0, 2] == Cell.O))
            {
                return true;
            }
            return false;

        }

        void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    board[i, j] = Cell.EMPTY;
            }
        }
        public string GetValueAt(Coordinate c)
        {
            return  this.board[c.x, c.y].ToString();
        }
        



    }
}
