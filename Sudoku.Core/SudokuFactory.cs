using System;

namespace Sudoku
{
    public static class SudokuFactory
    {
        public static SudokuPuzzle Create(int[,] board)
        {
            if (board == null || board.GetUpperBound(0) != 8 || board.GetUpperBound(1) != 8)
            {
                throw new ArgumentException("board");
            }

            int size = board.GetUpperBound(0) + 1;

            SudokuPiece[,] puzzle = new SudokuPiece[size, size];

            for (int i = 0; i < size; ++i)
            {
                for (int k = 0; k < size; ++k)
                {
                    int value = board[i, k];
                    puzzle[i, k] = new SudokuPiece(value);
                }
            }

            return new SudokuPuzzle(puzzle);
        }
    }
}
