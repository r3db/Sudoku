﻿using System;

namespace Sudoku.Prompt
{
    internal static class Program
    {
        private static void Main()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
	            {0, 0, 3, 0, 9, 5, 0, 0, 0},
	            {0, 0, 6, 2, 0, 0, 4, 0, 0},
	            {0, 0, 0, 1, 0, 0, 0, 8, 7},
	            {0, 0, 4, 7, 0, 0, 5, 0, 3},
	            {0, 2, 0, 0, 0, 0, 0, 4, 0},
	            {3, 0, 7, 0, 0, 6, 1, 0, 0},
	            {2, 4, 0, 0, 0, 1, 0, 0, 0},
	            {0, 0, 9, 0, 0, 2, 8, 0, 0},
	            {0, 0, 0, 5, 8, 0, 9, 0, 0},
            });

            Console.WriteLine(puzzle);
            Console.WriteLine("Pieces Left: " + puzzle.RemainingPieces);

            puzzle.Solve();

            Console.WriteLine(puzzle);
            Console.WriteLine("Pieces Left: " + puzzle.RemainingPieces);

            Console.ReadLine();
        }
    }
}
