using Projekt.Interfaces;
using System.Text.RegularExpressions;

public class GameEngine : IGameEngine
{
    private int _rows;
    private int _columns;
    private List<List<int?>> _board;
    private List<List<int?>> _connections;
    private string _currentPlayer;

    public void Initialize(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        _connections = new List<List<int?>>();
        _board = new List<List<int?>>();
        _currentPlayer = "Player 1";
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int row = 0; row < _rows + 1; row++)
        {
            List<int?> rowList = new List<int?>();
            List<int?> rowListC = new List<int?>();
            for (int col = 0; col < _columns + 1; col++)
            {
                rowList.Add(null);
                rowListC.Add(null);
            }
            _board.Add(rowList);
            _connections.Add(rowListC);
        }
        Console.WriteLine(_board.Count);
    }

    public void DrawBoard()
    {
        //Console.Clear();
        Console.WriteLine("Game Board:");
        for (int row = 0; row < _rows * 2 + 1; row++)
        {
            if (row % 2 == 0)
            {
                for (int column = 0; column < _columns + 1; column++)
                {
                    Console.BackgroundColor = _board[row / 2][column] == 1 ? ConsoleColor.Red : ConsoleColor.Blue;
                    Console.Write(" ");
                    Console.ResetColor();
                    Console.BackgroundColor = _connections[row / 2][column] == 1 ? ConsoleColor.Red : ConsoleColor.Blue;
                    if (column != _columns)
                    {
                        Console.Write($" {(_connections[row / 2][column] == null ? 0 : 1)} ");
                    }
                    Console.ResetColor();

                }
            }
            else
            {
                for (int column = 0; column < _columns + 1; column++)
                {
                    Console.BackgroundColor = _connections[row / 2][column] == 1 ? ConsoleColor.Red : ConsoleColor.Blue;
                    Console.Write((_connections[row / 2][column] == null ? 0 : 1));
                    Console.ResetColor();
                    Console.Write("   ");
                }

            }
            Console.Write("\n");
        }
    }
    public bool ValidateCoordinates(string input)
    {
        Regex regex = new Regex(@"^\d+\s+\d+$");
        return regex.IsMatch(input);
    }
    public bool IsMoveValid(string Cords, string Cords1)
    {
        if (ValidateCoordinates(Cords) & ValidateCoordinates(Cords1))
        {
            string[] pos = Cords.Split(' ');
            string[] pos1 = Cords1.Split(' ');
            if (_board[int.Parse(pos[0])][int.Parse(pos[1])] == null | _board[int.Parse(pos1[0])][int.Parse(pos1[1])] == null)
            {
                return true;
            }
        }
        return false;
    }
    public void MakeMove(string Cords, string Cords1, string playerName)
    {
        string[] pos = Cords.Split(' ');
        string[] pos1 = Cords1.Split(' ');
        int X = int.Parse(pos[0]);
        int Y = int.Parse(pos[1]);
        int X1 = int.Parse(pos1[0]);
        int Y1 = int.Parse(pos1[1]);

        if (Y1 != Y)
        {
            if (Y1 > Y)
            {
                _connections[X][Y] = 1;
            }
            else
            {
                _connections[X][Y - 1] = 1;
            }
        }
        else
        {
            if (X1 > X)
            {
                _connections[X][Y] = 1;
            }
            else
            {
                _connections[X - 1][Y1] = 1;
            }
        }


        _board[X][Y] = 1;
        _board[X1][Y1] = 1;
        _currentPlayer = (_currentPlayer == "Player 1") ? "Player 2" : "Player 1";

    }


    public bool IsGameOver()
    {
        for (int row = 1; row < _rows * 2 + 1; row += 2)
        {
            for (int column = 1; column < _columns * 2 + 1; column += 2)
            {
                /*if (_board[row, column] == null)
                {
                    return false; // Found an empty box, game not over
                }*/
            }
        }

        return false; // All boxes are filled, game over
    }

    public string GetWinner()
    {
        int player1Score = 0;
        int player2Score = 0;

        for (int row = 1; row < _rows * 2 + 1; row += 2)
        {
            for (int column = 1; column < _columns * 2 + 1; column += 2)
            {
                /*if (_board[row, column - 1] != null && _board[row, column + 1] != null &&
                    _board[row - 1, column] != null && _board[row + 1, column] != null)
                {
                    // All lines around the box are occupied
                    if (_board[row, column] == "Player 1")
                    {
                        player1Score++;
                    }
                    else if (_board[row, column] == "Player 2")
                    {
                        player2Score++;
                    }
                }*/
            }
        }

        if (player1Score > player2Score)
        {
            return "Player 1";
        }
        else if (player2Score > player1Score)
        {
            return "Player 2";
        }
        else
        {
            return "Draw";
        }
    }

    public string GetCurrentPlayer()
    {
        return _currentPlayer;
    }
}
