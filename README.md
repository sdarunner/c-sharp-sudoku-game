# C# Sudoku Game - Windows Application

A fully functional Sudoku game with a graphical user interface built in C# using Windows Forms.

## Features

- **9x9 Interactive Sudoku Grid**: Click and type to fill cells
- **Game Controls**:
  - **Check**: Verify if your solution is correct
  - **Clear**: Clear all user-entered numbers
  - **Solve**: Auto-solve the puzzle
  - **New Game**: Generate a new puzzle
- **Visual Feedback**:
  - Pre-filled numbers appear in light gray (read-only)
  - User-entered numbers appear in blue
  - Solved numbers appear in green
- **Input Validation**: Only accepts digits 1-9
- **Automatic Puzzle Generation**: Uses backtracking algorithm to generate unique puzzles

## Requirements

- .NET 6.0 or higher
- Windows OS (7 or later)
- Visual Studio 2022, VS Code, or any C# IDE with .NET support

## Installation & Running

### Option 1: Using Visual Studio
1. Clone the repository
2. Open the project in Visual Studio
3. Build: `Ctrl+Shift+B`
4. Run: `F5` or `Ctrl+F5`

### Option 2: Using Command Line
```bash
# Clone the repository
git clone https://github.com/sdarunner/c-sharp-sudoku-game.git
cd c-sharp-sudoku-game

# Build
dotnet build

# Run
dotnet run
```

## How to Play

1. Launch the application
2. Click on any empty cell (white background)
3. Type a number between 1-9
4. Fill the entire 9x9 grid following Sudoku rules:
   - Each row must contain digits 1-9 without repetition
   - Each column must contain digits 1-9 without repetition
   - Each 3x3 box must contain digits 1-9 without repetition
5. **Check**: Click to verify your solution
6. **Clear**: Remove all your entries and start over
7. **Solve**: Reveal the complete solution
8. **New Game**: Generate a new puzzle to play

## Code Structure

```
├── Program.cs              - Application entry point
├── SudokuGame.cs           - Main form with GUI components
├── SudokuBoard.cs          - Core game logic and puzzle generation
├── SudokuGame.csproj       - Project configuration
└── README.md               - This file
```

## Algorithm Details

### Puzzle Generation
1. **Random Diagonal Fill**: Fills the three diagonal 3x3 boxes with random numbers 1-9
2. **Backtracking Solver**: Recursively solves the puzzle with randomized number selection
3. **Strategic Removal**: Removes approximately 40 numbers to create a playable puzzle

### Validation
- **Row Validation**: Checks for duplicate numbers in rows
- **Column Validation**: Checks for duplicate numbers in columns
- **3x3 Box Validation**: Checks for duplicate numbers in 3x3 boxes

## UI Components

- **TextBox Grid**: 9x9 array of input fields with borders
- **Check Button**: Validates if the current solution is correct
- **Clear Button**: Resets all user entries
- **Solve Button**: Displays the solution in green
- **New Game Button**: Generates a new puzzle

## Customization

To adjust puzzle difficulty, modify the `RemoveNumbers` parameter in `SudokuBoard.cs`:
```csharp
RemoveNumbers(initialBoard, 30);  // Easier puzzle - fewer blanks
RemoveNumbers(initialBoard, 50);  // Harder puzzle - more blanks
RemoveNumbers(initialBoard, 40);  // Current - medium difficulty
```

## License

MIT License - Feel free to modify and use as needed!

## Future Enhancements

- [ ] Difficulty levels (Easy, Medium, Hard)
- [ ] Timer for speed solving
- [ ] Score tracking
- [ ] Hint system
- [ ] Dark theme
- [ ] Save/Load game state
- [ ] Keyboard navigation

## Screenshots

The application displays:
- A 9x9 grid divided into 3x3 boxes with black borders
- Preset numbers in light gray (unchangeable)
- User input in blue
- Solution numbers in green
- Control buttons below the grid

## Contributing

Feel free to fork this repository and submit pull requests for improvements!
