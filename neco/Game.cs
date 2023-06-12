using Projekt.Interfaces;

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
        var currentPlayer = _player1;

        _gameEngine.Initialize(rows, columns);

        while (!_gameEngine.IsGameOver())
        {
            _gameEngine.DrawBoard();

            Console.WriteLine($"Current player: {_player2.GetName()}");

            var nextMove = currentPlayer.GetNextMove();

            if (_gameEngine.IsMoveValid(nextMove.Cords, nextMove.Cords1))
            {
                _gameEngine.MakeMove(nextMove.Cords, nextMove.Cords1, currentPlayer.GetName());
                _gameEngine.CheckBoard();

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
    }


    private IPlayer GetCurrentPlayer(IPlayer currentPlayer)
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
