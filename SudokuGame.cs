using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuGame
{
    public partial class SudokuGame : Form
    {
        private SudokuBoard sudokuBoard;
        private TextBox[,] textBoxes;
        private const int GridSize = 9;
        private const int CellSize = 40;
        private const int Padding = 10;

        public SudokuGame()
        {
            InitializeComponent();
            this.Text = "Sudoku Game";
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            sudokuBoard = new SudokuBoard();
            textBoxes = new TextBox[GridSize, GridSize];

            CreateSudokuGrid();
            CreateButtons();
        }

        private void CreateSudokuGrid()
        {
            Panel gridPanel = new Panel();
            gridPanel.Location = new Point(Padding, Padding);
            gridPanel.Size = new Size(GridSize * CellSize + 10, GridSize * CellSize + 10);
            gridPanel.BackColor = Color.Black;
            this.Controls.Add(gridPanel);

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Location = new Point(col * CellSize + 1, row * CellSize + 1);
                    textBox.Size = new Size(CellSize - 2, CellSize - 2);
                    textBox.Font = new Font("Arial", 14, FontStyle.Bold);
                    textBox.TextAlign = HorizontalAlignment.Center;
                    textBox.CharacterCasing = CharacterCasing.Upper;
                    textBox.MaxLength = 1;

                    int cellValue = sudokuBoard.GetInitialValue(row, col);
                    if (cellValue != 0)
                    {
                        textBox.Text = cellValue.ToString();
                        textBox.ReadOnly = true;
                        textBox.BackColor = Color.LightGray;
                        textBox.ForeColor = Color.Black;
                    }
                    else
                    {
                        textBox.BackColor = Color.White;
                        textBox.ForeColor = Color.Blue;
                    }

                    textBox.KeyPress += (s, e) =>
                    {
                        if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                        {
                            e.Handled = true;
                        }
                    };

                    textBoxes[row, col] = textBox;
                    gridPanel.Controls.Add(textBox);
                }
            }
        }

        private void CreateButtons()
        {
            int buttonY = GridSize * CellSize + Padding * 3;

            Button checkButton = new Button();
            checkButton.Text = "Check";
            checkButton.Location = new Point(Padding, buttonY);
            checkButton.Size = new Size(100, 40);
            checkButton.Font = new Font("Arial", 10, FontStyle.Bold);
            checkButton.Click += (s, e) => CheckSolution();
            this.Controls.Add(checkButton);

            Button clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new Point(120 + Padding, buttonY);
            clearButton.Size = new Size(100, 40);
            clearButton.Font = new Font("Arial", 10, FontStyle.Bold);
            clearButton.Click += (s, e) => ClearBoard();
            this.Controls.Add(clearButton);

            Button solveButton = new Button();
            solveButton.Text = "Solve";
            solveButton.Location = new Point(240 + Padding, buttonY);
            solveButton.Size = new Size(100, 40);
            solveButton.Font = new Font("Arial", 10, FontStyle.Bold);
            solveButton.Click += (s, e) => SolveBoard();
            this.Controls.Add(solveButton);

            Button newGameButton = new Button();
            newGameButton.Text = "New Game";
            newGameButton.Location = new Point(360 + Padding, buttonY);
            newGameButton.Size = new Size(100, 40);
            newGameButton.Font = new Font("Arial", 10, FontStyle.Bold);
            newGameButton.Click += (s, e) => NewGame();
            this.Controls.Add(newGameButton);
        }

        private void CheckSolution()
        {
            int[,] playerBoard = GetBoardFromUI();
            if (sudokuBoard.IsValid(playerBoard) && sudokuBoard.IsSolved(playerBoard))
            {
                MessageBox.Show("Congratulations! You solved the Sudoku!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The solution is incorrect. Try again!", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearBoard()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (!textBoxes[row, col].ReadOnly)
                    {
                        textBoxes[row, col].Text = "";
                    }
                }
            }
        }

        private void SolveBoard()
        {
            int[,] solution = sudokuBoard.GetSolution();
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (!textBoxes[row, col].ReadOnly)
                    {
                        textBoxes[row, col].Text = solution[row, col].ToString();
                        textBoxes[row, col].ForeColor = Color.Green;
                    }
                }
            }
        }

        private void NewGame()
        {
            sudokuBoard = new SudokuBoard();
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    int cellValue = sudokuBoard.GetInitialValue(row, col);
                    if (cellValue != 0)
                    {
                        textBoxes[row, col].Text = cellValue.ToString();
                        textBoxes[row, col].ReadOnly = true;
                        textBoxes[row, col].BackColor = Color.LightGray;
                        textBoxes[row, col].ForeColor = Color.Black;
                    }
                    else
                    {
                        textBoxes[row, col].Text = "";
                        textBoxes[row, col].ReadOnly = false;
                        textBoxes[row, col].BackColor = Color.White;
                        textBoxes[row, col].ForeColor = Color.Blue;
                    }
                }
            }
        }

        private int[,] GetBoardFromUI()
        {
            int[,] board = new int[GridSize, GridSize];
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (int.TryParse(textBoxes[row, col].Text, out int value))
                    {
                        board[row, col] = value;
                    }
                }
            }
            return board;
        }

        private void InitializeComponent()
        {
            // Auto-generated component initialization
        }
    }
}
