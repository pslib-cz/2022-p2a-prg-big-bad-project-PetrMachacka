namespace Projekt.Interfaces
{
    public interface IPlayer
    {
        string GetName();
        (string? Cords, string? Cords1) GetNextMove();
    }
}
