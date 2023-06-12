namespace Projekt.Interfaces
{
    public interface IGameEngine
    {
        void Initialize(int rows, int columns);
        void DrawBoard();
        bool IsMoveValid(string Cords, string Cords1);
        void MakeMove(string Cords, string Cords1,  string playerName);
        bool IsGameOver();
        string GetWinner();
        string GetCurrentPlayer();
        void HighLight(int X, int Y);
        void CheckBoard();
    }
}
