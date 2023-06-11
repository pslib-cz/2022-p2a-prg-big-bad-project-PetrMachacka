using Projekt.Interfaces;
using System;

public class Game
{
    private readonly IGameEngine _gameEngine;
    private readonly IPlayer _player1;
    private readonly IPlayer _player2;

    public Game(IGameEngine gameEngine, IPlayer player1, IPlayer player2)
    {
        _gameEngine = gameEngine;
        _player1 = player1;
        _player2 = player2;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to Dots and Boxes!");
        Console.WriteLine("Enter the number of rows and columns for the game board:");

        int rows = ReadIntegerInput("Rows: ");
        int columns = ReadIntegerInput("Columns: ");

        _gameEngine.Initialize(rows, columns);

        while (!_gameEngine.IsGameOver())
        {
            _gameEngine.DrawBoard();

            var currentPlayer = GetCurrentPlayer();
            Console.WriteLine($"Current player: {currentPlayer.GetName()}");

            var nextMove = currentPlayer.GetNextMove();

            if (_gameEngine.IsMoveValid(nextMove.Cords, nextMove.Cords1))
            {
                _gameEngine.MakeMove  (nextMove.Cords, nextMove.Cords1, currentPlayer.GetName());
            }
            else
            {
                Console.WriteLine("Invalid move! Please try again.");
                continue;
            }
        }

        Console.WriteLine("Game Over!");
        Console.WriteLine($"Winner: {_gameEngine.GetWinner()}");
    }

    private IPlayer GetCurrentPlayer()
    {
        return _gameEngine.GetCurrentPlayer() == _player1.GetName() ? _player1 : _player2;
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
