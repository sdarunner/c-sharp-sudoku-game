using System;
using System.Linq;

namespace SudokuGame
{
    public class SudokuBoard
    {
        private int[,] solution;
        private int[,] initialBoard;
        private const int GridSize = 9;
        private const int BoxSize = 3;
        private Random random = new Random();

        public SudokuBoard()
        {
            GeneratePuzzle();
        }

        private void GeneratePuzzle()
        {
            solution = new int[GridSize, GridSize];
            FillDiagonal();
            Solve(solution);

            // Store the complete solution
            int[,] completeSolution = (int[,])solution.Clone();

            // Create initial board by removing numbers
            initialBoard = (int[,])solution.Clone();
            RemoveNumbers(initialBoard, 40); // Remove 40 numbers for difficulty

            solution = completeSolution;
        }

        private void FillDiagonal()
        {
            for (int box = 0; box < GridSize; box += BoxSize)
            {
                FillBox(box, box);
            }
        }

        private void FillBox(int row, int col)
        {
            int[] nums = Enumerable.Range(1, GridSize).OrderBy(x => random.Next()).ToArray();
            int index = 0;

            for (int i = row; i < row + BoxSize; i++)
            {
                for (int j = col; j < col + BoxSize; j++)
                {
                    solution[i, j] = nums[index++];
                }
            }
        }

        private bool Solve(int[,] board)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (board[row, col] == 0)
                    {
                        int[] nums = Enumerable.Range(1, GridSize).OrderBy(x => random.Next()).ToArray();
                        foreach (int num in nums)
                        {
                            if (IsValid(board, row, col, num))
                            {
                                board[row, col] = num;

                                if (Solve(board))
                                    return true;

                                board[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsValid(int[,] board, int row, int col, int num)
        {
            // Check row
            for (int x = 0; x < GridSize; x++)
            {
                if (board[row, x] == num)
                    return false;
            }

            // Check column
            for (int x = 0; x < GridSize; x++)
            {
                if (board[x, col] == num)
                    return false;
            }

            // Check 3x3 box
            int startRow = row - row % BoxSize;
            int startCol = col - col % BoxSize;

            for (int i = startRow; i < startRow + BoxSize; i++)
            {
                for (int j = startCol; j < startCol + BoxSize; j++)
                {
                    if (board[i, j] == num)
                        return false;
                }
            }

            return true;
        }

        private void RemoveNumbers(int[,] board, int count)
        {
            int removed = 0;
            while (removed < count)
            {
                int row = random.Next(GridSize);
                int col = random.Next(GridSize);

                if (board[row, col] != 0)
                {
                    board[row, col] = 0;
                    removed++;
                }
            }
        }

        public int GetInitialValue(int row, int col)
        {
            return initialBoard[row, col];
        }

        public int[,] GetSolution()
        {
            return (int[,])solution.Clone();
        }

        public bool IsValid(int[,] board)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    int num = board[row, col];
                    if (num != 0)
                    {
                        board[row, col] = 0;
                        if (!IsValid(board, row, col, num))
                        {
                            board[row, col] = num;
                            return false;
                        }
                        board[row, col] = num;
                    }
                }
            }
            return true;
        }

        public bool IsSolved(int[,] board)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (board[row, col] == 0)
                        return false;
                }
            }
            return IsValid(board);
        }
    }
}
