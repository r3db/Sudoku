using System;
using System.Linq;
using NUnit.Framework;

namespace Sudoku.NUnit
{
    [TestFixture]
    public class SudokuPuzzleSpecs
    {
        #region Methods

        [Test]
        public void AssertCanSolvePuzzle001()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
                {0, 0, 0, 9, 4, 0, 0, 0, 3},
                {3, 0, 0, 0, 0, 0, 2, 8, 7},
                {0, 0, 1, 0, 0, 0, 0, 5, 0},
                {0, 0, 0, 1, 5, 0, 0, 0, 6},
                {0, 8, 0, 2, 0, 9, 0, 4, 0},
                {5, 0, 0, 0, 8, 6, 0, 0, 0},
                {0, 9, 0, 0, 0, 0, 4, 0, 0},
                {8, 7, 2, 0, 0, 0, 0, 0, 1},
                {4, 0, 0, 0, 3, 2, 0, 0, 0},
            });

            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {2, 5, 8, 9, 4, 7, 6, 1, 3},
                {3, 4, 9, 5, 6, 1, 2, 8, 7},
                {7, 6, 1, 3, 2, 8, 9, 5, 4},
                {9, 2, 4, 1, 5, 3, 8, 7, 6},
                {1, 8, 6, 2, 7, 9, 3, 4, 5},
                {5, 3, 7, 4, 8, 6, 1, 9, 2},
                {6, 9, 3, 7, 1, 5, 4, 2, 8},
                {8, 7, 2, 6, 9, 4, 5, 3, 1},
                {4, 1, 5, 8, 3, 2, 7, 6, 9},
            });

            AssertIsSolved(puzzle, solution);
        }

        [Test]
        public void AssertCanSolvePuzzle002()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
                {5, 9, 0, 0, 0, 6, 0, 0, 1},
                {0, 4, 0, 5, 0, 0, 0, 6, 7},
                {0, 0, 0, 0, 3, 0, 0, 0, 0},
                {4, 0, 0, 0, 0, 0, 0, 9, 0},
                {0, 0, 3, 0, 7, 0, 8, 0, 0},
                {0, 2, 0, 0, 0, 0, 0, 0, 5},
                {0, 0, 0, 0, 8, 0, 0, 0, 0},
                {9, 1, 0, 0, 0, 5, 0, 7, 0},
                {6, 0, 0, 7, 0, 0, 0, 1, 4},
            });

            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {5, 9, 7, 8, 2, 6, 4, 3, 1},
                {3, 4, 8, 5, 1, 9, 2, 6, 7},
                {2, 6, 1, 4, 3, 7, 5, 8, 9},
                {4, 7, 6, 2, 5, 8, 1, 9, 3},
                {1, 5, 3, 9, 7, 4, 8, 2, 6},
                {8, 2, 9, 1, 6, 3, 7, 4, 5},
                {7, 3, 4, 6, 8, 1, 9, 5, 2},
                {9, 1, 2, 3, 4, 5, 6, 7, 8},
                {6, 8, 5, 7, 9, 2, 3, 1, 4},
            });

            AssertIsSolved(puzzle, solution);
        }

        [Test]
        public void AssertCanSolvePuzzle003()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
                {0, 0, 0, 7, 1, 6, 0, 0, 0},
                {6, 0, 0, 9, 0, 2, 0, 0, 7},
                {0, 0, 8, 0, 0, 0, 9, 0, 0},
                {2, 0, 1, 0, 0, 0, 5, 0, 6},
                {0, 8, 0, 0, 0, 0, 0, 1, 0},
                {4, 0, 5, 0, 0, 0, 8, 0, 2},
                {0, 0, 3, 0, 0, 0, 1, 0, 0},
                {1, 0, 0, 4, 0, 9, 0, 0, 3},
                {0, 0, 0, 1, 3, 5, 0, 0, 0},
            });


            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {5, 3, 9, 7, 1, 6, 2, 4, 8},
                {6, 1, 4, 9, 8, 2, 3, 5, 7},
                {7, 2, 8, 5, 4, 3, 9, 6, 1},
                {2, 7, 1, 8, 9, 4, 5, 3, 6},
                {3, 8, 6, 2, 5, 7, 4, 1, 9},
                {4, 9, 5, 3, 6, 1, 8, 7, 2},
                {9, 4, 3, 6, 7, 8, 1, 2, 5},
                {1, 5, 7, 4, 2, 9, 6, 8, 3},
                {8, 6, 2, 1, 3, 5, 7, 9, 4},
            });

            AssertIsSolved(puzzle, solution);
        }

        [Test]
        public void AssertCanSolvePuzzle004()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
                {0, 4, 0, 1, 0, 0, 0, 0, 0},
                {0, 0, 3, 0, 8, 0, 0, 0, 5},
                {0, 0, 7, 0, 9, 2, 8, 6, 0},
                {0, 0, 4, 0, 0, 0, 0, 0, 1},
                {0, 3, 9, 0, 1, 0, 6, 8, 0},
                {2, 0, 0, 0, 0, 0, 3, 0, 0},
                {0, 6, 1, 5, 7, 0, 2, 0, 0},
                {3, 0, 0, 0, 4, 0, 9, 0, 0},
                {0, 0, 0, 0, 0, 3, 0, 1, 0},
            });

            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {6, 4, 8, 1, 3, 5, 7, 2, 9},
                {9, 2, 3, 6, 8, 7, 1, 4, 5},
                {1, 5, 7, 4, 9, 2, 8, 6, 3},
                {7, 8, 4, 3, 2, 6, 5, 9, 1},
                {5, 3, 9, 7, 1, 4, 6, 8, 2},
                {2, 1, 6, 9, 5, 8, 3, 7, 4},
                {4, 6, 1, 5, 7, 9, 2, 3, 8},
                {3, 7, 2, 8, 4, 1, 9, 5, 6},
                {8, 9, 5, 2, 6, 3, 4, 1, 7},
            });

            AssertIsSolved(puzzle, solution);
        }

        [Test]
        public void AssertCanSolvePuzzle005()
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

            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {4, 7, 3, 8, 9, 5, 2, 1, 6},
                {8, 1, 6, 2, 3, 7, 4, 5, 9},
                {9, 5, 2, 1, 6, 4, 3, 8, 7},
                {1, 9, 4, 7, 2, 8, 5, 6, 3},
                {6, 2, 5, 3, 1, 9, 7, 4, 8},
                {3, 8, 7, 4, 5, 6, 1, 9, 2},
                {2, 4, 8, 9, 7, 1, 6, 3, 5},
                {5, 3, 9, 6, 4, 2, 8, 7, 1},
                {7, 6, 1, 5, 8, 3, 9, 2, 4},
            });

            AssertIsSolved(puzzle, solution);
        }

        [Test]
        public void AssertCanSolvePuzzle006()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
                {8, 0, 0, 0, 0, 0, 3, 0, 1},
                {0, 0, 1, 5, 0, 4, 0, 0, 0},
                {5, 0, 0, 0, 0, 2, 0, 0, 0},
                {0, 0, 7, 0, 0, 0, 0, 9, 0},
                {0, 8, 9, 4, 7, 3, 5, 6, 0},
                {0, 2, 0, 0, 0, 0, 8, 0, 0},
                {0, 0, 0, 6, 0, 0, 0, 0, 4},
                {0, 0, 0, 3, 0, 8, 2, 0, 0},
                {9, 0, 6, 0, 0, 0, 0, 0, 5},
            });

            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {8, 4, 2, 7, 6, 9, 3, 5, 1},
                {7, 9, 1, 5, 3, 4, 6, 2, 8},
                {5, 6, 3, 1, 8, 2, 7, 4, 9},
                {6, 5, 7, 8, 2, 1, 4, 9, 3},
                {1, 8, 9, 4, 7, 3, 5, 6, 2},
                {3, 2, 4, 9, 5, 6, 8, 1, 7},
                {2, 7, 8, 6, 1, 5, 9, 3, 4},
                {4, 1, 5, 3, 9, 8, 2, 7, 6},
                {9, 3, 6, 2, 4, 7, 1, 8, 5},
            });

            AssertIsSolved(puzzle, solution);
        }

        [Test]
        public void AssertCanSolvePuzzle007()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
                {9, 0, 0, 4, 1, 2, 0, 0, 3},
                {0, 4, 0, 0, 0, 8, 5, 0, 0},
                {0, 0, 0, 0, 5, 0, 0, 0, 0},
                {8, 0, 1, 0, 0, 0, 0, 5, 0},
                {0, 3, 4, 0, 0, 0, 9, 6, 0},
                {0, 9, 0, 0, 0, 0, 4, 0, 7},
                {0, 0, 0, 0, 2, 0, 0, 0, 0},
                {0, 0, 7, 1, 0, 0, 0, 2, 0},
                {3, 0, 0, 7, 6, 9, 0, 0, 5},
            });

            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {9, 6, 5, 4, 1, 2, 8, 7, 3},
                {7, 4, 2, 3, 9, 8, 5, 1, 6},
                {1, 8, 3, 6, 5, 7, 2, 9, 4},
                {8, 7, 1, 9, 4, 6, 3, 5, 2},
                {2, 3, 4, 8, 7, 5, 9, 6, 1},
                {5, 9, 6, 2, 3, 1, 4, 8, 7},
                {6, 1, 9, 5, 2, 4, 7, 3, 8},
                {4, 5, 7, 1, 8, 3, 6, 2, 9},
                {3, 2, 8, 7, 6, 9, 1, 4, 5},
            });

            AssertIsSolved(puzzle, solution);
        }

        [Test]
        public void AssertCanSolvePuzzle008()
        {
            SudokuPuzzle puzzle = SudokuFactory.Create(new[,]
            {
                {0, 0, 0, 0, 1, 5, 0, 8, 0},
                {0, 6, 0, 0, 9, 0, 0, 0, 3},
                {0, 9, 0, 3, 0, 0, 0, 0, 5},
                {0, 1, 2, 0, 0, 0, 0, 7, 0},
                {0, 0, 0, 8, 0, 4, 0, 0, 0},
                {0, 4, 0, 0, 0, 0, 9, 3, 0},
                {6, 0, 0, 0, 0, 3, 0, 2, 0},
                {7, 0, 0, 0, 6, 0, 0, 4, 0},
                {0, 8, 0, 5, 7, 0, 0, 0, 0},
            });

            SudokuPuzzle solution = SudokuFactory.Create(new[,]
            {
                {3, 2, 7, 6, 1, 5, 4, 8, 9},
                {4, 6, 5, 7, 9, 8, 2, 1, 3},
                {1, 9, 8, 3, 4, 2, 7, 6, 5},
                {5, 1, 2, 9, 3, 6, 8, 7, 4},
                {9, 7, 3, 8, 2, 4, 6, 5, 1},
                {8, 4, 6, 1, 5, 7, 9, 3, 2},
                {6, 5, 9, 4, 8, 3, 1, 2, 7},
                {7, 3, 1, 2, 6, 9, 5, 4, 8},
                {2, 8, 4, 5, 7, 1, 3, 9, 6},
            });

            AssertIsSolved(puzzle, solution);
        }

        #endregion

        #region Helpers

        private static void AssertIsSolved(SudokuPuzzle puzzle, SudokuPuzzle solution)
        {
            // Pre-Assert
            Assert.AreNotEqual(0, puzzle.RemainingPieces);

            // Act
            puzzle.Solve();

            // Assert
            Assert.AreEqual(0, puzzle.RemainingPieces);
            Assert.AreEqual(true, IsPuzzleValid(puzzle));

            Assert.AreEqual(0, solution.RemainingPieces);
            Assert.AreEqual(true, IsPuzzleValid(solution));

            var ps = solution.GetPuzzle();
            var pp = puzzle.GetPuzzle();

            for (int i = 0; i < 9; ++i)
            {
                for (int k = 0; k < 9; ++k)
                {
                    Assert.AreEqual(pp[i, k].Value, ps[i, k].Value);
                    Assert.AreEqual(true, ps[i, k].PossibleValues == null);
                    Assert.AreEqual(true, pp[i, k].PossibleValues == null);
                }
            }
        }

        public static bool IsPuzzleValid(SudokuPuzzle puzzle)
        {
            int sum(SudokuPiece[,] p)
            {
                int result = 0;

                for (int i = 0; i < 3; ++i)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        result += p[i, k].Value;
                    }
                }
                return result;
            }

            for (int i = 0; i < 9; ++i)
            {
                if (puzzle.GetRow(i).Sum(x => x.Value) != 45 || puzzle.GetColumn(i).Sum(x => x.Value) != 45 || sum(puzzle.GetArea(i)) != 45)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}