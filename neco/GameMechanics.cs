using System.Text.RegularExpressions;

namespace Projekt
{
    public class GameMechanics
    {
        private int _rows;
        private int _columns;
        private List<List<int?>> _board;
        private List<List<int>> _connections;
        private List<List<int>> _Points;
        private string? _currentPlayer;
        private string? _fullPathBoard;
        private string? _fullPathScore;
        private string? _fullPathStart;
        public void Initialize(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _Points = new List<List<int>>();
            _connections = new List<List<int>>();
            _board = new List<List<int?>>();
            _currentPlayer = "Player 1";
            _fullPathBoard = InitializePath("board.txt");
            _fullPathScore = InitializePath("score.txt");
            _fullPathStart = InitializePath("Start.txt");
            InitializeBoard();
        }
        public string GetScore(string Name)
        {
            using (StreamReader reader = new StreamReader(_fullPathScore))
            {
                string firstLine = reader.ReadLine();
                string P1 = firstLine.Substring(0, 1);
                string P2 = firstLine.Substring(1, 1);
                return Name == "1" ? P1 : P2;
            }
        }
        public void WriteScore(int p1, int p2)
        {
            StreamWriter wr = new StreamWriter(_fullPathScore);
            wr.Write(p1);
            wr.Write(p2);
            wr.Close();
        }
        public static string InitializePath(string relative)
        {
            string relativePath = $@"files\{relative}";
            string Path1 = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            string fullPath = Path1.Replace(@"\bin\Debug\net6.0", "");
            return fullPath;
        }
        public bool Started()
        {
            using (StreamReader reader = new StreamReader(_fullPathStart))
            {
                string firstLine = reader.ReadLine();

                return firstLine.Contains("1");
            }

        }
        public void Load()
        {

            using (StreamReader sr = new StreamReader(_fullPathBoard))
            {
                string line;
                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);

                    string[] Line;
                    Line = line.Split(";");

                    if (counter == 0)
                    {

                        for (int row = 0; row < Line.Count(); row++)
                        {
                            List<char> charList = Line[row].ToList();
                            for (int column = 0; column < charList.Count(); column++)
                            {
                                Console.Write(charList[column]);
                                _board[row][column] = charList[column] == 3 ? null : charList[column];
                            }
                            Console.Write(";");
                        }
                        counter++;
                    }
                    if (counter == 1)
                    {
                        for (int row = 0; row < Line.Count() + 1; row++)
                        {
                            List<char> charList = Line[row].ToList();
                            for (int column = 0; column < charList.Count(); column++)
                            {
                                Console.Write(charList[column]);
                                _Points[row][column] = charList[column] == 3 ? 0 : charList[column];
                            }
                            Console.Write(";");
                        }
                        counter++;
                    }
                    if (counter == 2)
                    {
                        for (int row = 0; row < Line.Count(); row++)
                        {
                            List<char> charList = Line[row].ToList();
                            for (int column = 0; column < charList.Count(); column++)
                            {
                                Console.Write(charList[column]);
                                _connections[row][column] = charList[column] == 3 ? 0 : charList[column];
                            }
                            Console.Write(";");
                        }
                        counter++;

                    }
                    counter++;
                }

            }
        }
        public void Start(bool start)
        {
            using (StreamWriter wr = new StreamWriter(_fullPathStart))
            {
                wr.Write(start == true ? "1" : "0");
            }
        }
        public void SaveBoard()
        {
            StreamWriter wr = new StreamWriter(_fullPathBoard);

            for (int row = 0; row < _board.Count; row++)
            {
                for (int column = 0; column < _board[row].Count; column++)
                {
                    wr.Write(_board[row][column] is null ? 3 : _Points[row][column]);
                }
                wr.Write(';');
            }
            wr.Write("\n");
            for (int row = 0; row < _Points.Count; row++)
            {
                for (int column = 0; column < _Points[row].Count; column++)
                {
                    wr.Write(_Points[row][column] == 0 ? 3 : _Points[row][column]);
                }
                wr.Write(';');
            }
            wr.Write("\n");
            for (int row = 0; row < _connections.Count; row++)
            {
                for (int column = 0; column < _connections[row].Count; column++)
                {
                    wr.Write(_connections[row][column] == 0 ? 3 : _Points[row][column]);
                }
                wr.Write(';');
            }
            wr.Close();
        }
        private void InitializeBoard()
        {
            for (int row = 0; row < _rows + 1; row++)
            {
                List<int?> rowList = new List<int?>();
                for (int col = 0; col < _columns + 1; col++)
                {
                    rowList.Add(null);
                }
                _board.Add(rowList);
            }

            for (int row = 0; row < _rows * 2 + 1; row++)
            {
                List<int> rowListC = new List<int>();
                for (int col = 0; col < _columns + 1; col++)
                {
                    rowListC.Add(0);
                }
                _connections.Add(rowListC);
            }
            for (int row = 0; row < _rows + 1; row++)
            {
                List<int> rowListP = new List<int>();
                for (int col = 0; col < _columns; col++)
                {
                    rowListP.Add(0);
                }
                _Points.Add(rowListP);
            }
        }

        public void DrawBoard()
        {
            Console.WriteLine("Game Board:");
            for (int row = 0; row < _rows * 2 + 1; row++)
            {
                if (row % 2 == 0)
                {
                    for (int column = 0; column < _columns + 1; column++)
                    {
                        Console.BackgroundColor = _board[row / 2][column] == 1 ? ConsoleColor.Magenta : ConsoleColor.Yellow;
                        Console.Write(" ");
                        Console.ResetColor();
                        Console.BackgroundColor = _connections[row][column] == 1 ? ConsoleColor.Magenta : ConsoleColor.Black;
                        if (column != _columns)
                        {
                            Console.Write($"   ");
                        }
                        Console.ResetColor();

                    }
                }
                else
                {
                    for (int column = 0; column < _columns + 1; column++)
                    {
                        Console.BackgroundColor = _connections[row][column] == 1 ? ConsoleColor.Magenta : ConsoleColor.Black;
                        Console.Write(" ");
                        Console.ResetColor();
                        if (column < _columns)
                        {
                            switch (_Points[row / 2][column])
                            {
                                case 1:
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    break;
                                case 2:
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    break;
                            }
                            Console.Write("   ");
                            Console.ResetColor();
                        }
                    }

                }
                Console.Write("\n");
            }
        }
        public static bool ValidateCoordinates(string input)
        {
            Console.WriteLine(input);
            string pattern = @"^\d+\s+\d+$";
            return Regex.IsMatch(input, pattern);
        }
        public bool IsMoveValid(string Cords, string Cords1)
        {
            string[] pos = Cords.Split(' ');
            string[] pos1 = Cords1.Split(' ');
            if (ValidateCoordinates(Cords) && ValidateCoordinates(Cords1) && int.TryParse(pos[0], out int X) && int.TryParse(pos[1], out int Y) && int.TryParse(pos1[0], out int X1) && int.TryParse(pos1[1], out int Y1) && Math.Abs(X - X1) < 2 && Math.Abs(Y - Y1) < 2 && (X1 == X ^ Y1 == Y) && X < _rows * 2 - 1 && X1 < _rows * 2 - 1 && Y < _columns * 2 - 1 && Y1 < _columns * 2 - 1)
            {
                return true;
            }
            return false;
        }
        public void HighLight(int X, int Y)
        {
            _board[X][Y] = _board[X][Y] == 1 ? 0 : 1;
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
                    _connections[X * 2][Y] = 1;
                }
                else
                {
                    _connections[X * 2][Y - 1] = 1;
                }
            }
            else
            {
                if (X1 > X)
                {
                    _connections[X * 2 + 1][Y] = 1;
                }
                else
                {
                    _connections[X * 2 - 1][Y] = 1;
                }
            }

            _board[X][Y] = 1;
            _board[X1][Y1] = 1;

            // Update current player
            _currentPlayer = playerName == "Player 1" ? "Player 2" : "Player 1";
        }

        public void CheckBoard(Player CurrentPlayer)
        {
            int value = int.Parse(CurrentPlayer.GetName());
            int Y = 0;
            for (int i = 0; i < _connections.Count - 1; i = i + 2)
            {
                for (int x = 0; x < _connections[i].Count - 1; x++)
                {
                    if (_connections[i + 1][x] == 1 & _connections[i + 1][x + 1] == 1 & _connections[i + 2][x] == 1 & _connections[i][x] == 1)
                    {
                        if (_Points[Y][x] == 0)
                        {
                            _Points[Y][x] = value;
                        }
                    }
                }
                Y++;
            }
        }

        public bool IsGameOver()
        {
            for (int row = 1; row < _rows; row += 2)
            {
                for (int column = 1; column < _columns; column += 2)
                {
                    if (_Points[row][column] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public string GetWinner()
        {
            int player1Score = 0;
            int player2Score = 0;

            for (int row = 0; row < _Points.Count; row++)
            {
                for (int column = 0; column < _Points[row].Count; column += 2)
                {
                    if (_Points[row][column] == 1)
                    {
                        player1Score++;
                    }
                    else
                    {
                        player2Score++;
                    }
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

}
