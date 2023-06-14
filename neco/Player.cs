namespace Projekt
{
    public class Player
    {
        private readonly string _name;
        public int _score;

        public Player(string name, int score)
        {
            _name = name;
            _score = score;
        }

        public string GetName()
        {
            return _name;
        }
        public int GetScore()
        {
            return _score;
        }

        public (string Cords, string Cords1) GetNextMove()
        {
            Console.WriteLine("Enter the two coordinates (Row, Column):");
            string? Cords = Console.ReadLine();
            string? Cords1 = Console.ReadLine();
            return (Cords, Cords1);
        }

    }
}

