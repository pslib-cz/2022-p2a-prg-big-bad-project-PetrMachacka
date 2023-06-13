using Projekt;
using System.Data;

namespace Projekt
{
    public class Game
    {
        readonly Player _player1 = new Player("1");
        readonly Player _player2 = new Player("2");
        readonly GameMechanics _gameEngine = new();
        public void Start()
        {
            int rows = 5;
            int columns = 5;
            _gameEngine.Initialize(rows, columns);
            if (_gameEngine.Started())
            {
                _gameEngine.Load();
            }
            else
            {
                Console.WriteLine("Welcome to Dots and Boxes!");
                Console.WriteLine("Enter the number of rows and columns for the game board:");
                rows = ReadIntegerInput("Rows: ");
                columns = ReadIntegerInput("Columns: ");
                _gameEngine.Initialize(rows, columns);
            }



            var currentPlayer = _player1;


            
            while (!_gameEngine.IsGameOver())
            {
                
                _gameEngine.DrawBoard();
                //_gameEngine.SaveBoard();
                //_gameEngine.Start(true);
                Console.WriteLine($"Current player: {currentPlayer.GetName()}");

                var nextMove = currentPlayer.GetNextMove();

                if (_gameEngine.IsMoveValid(nextMove.Cords, nextMove.Cords1))
                {
                    _gameEngine.MakeMove(nextMove.Cords, nextMove.Cords1, currentPlayer.GetName());
                    _gameEngine.CheckBoard(currentPlayer);
                    currentPlayer = GetCurrentPlayer(currentPlayer);
                }
                else
                {
                    Console.WriteLine("Invalid move! Please try again.");
                }
            }

            _gameEngine.DrawBoard();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Winner: {_gameEngine.GetWinner()}");

            Console.WriteLine("Player 1: "  + _gameEngine.GetScore("1") + " Player 2: " + _gameEngine.GetScore(""));
        }


        private Player GetCurrentPlayer(Player currentPlayer)
        {
            return currentPlayer == _player1 ? _player2 : _player1;
        }

        private int ReadIntegerInput(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                    break;
                Console.WriteLine("Invalid input! Please enter an integer value.");
            }
            return value;
        }
    }
}

