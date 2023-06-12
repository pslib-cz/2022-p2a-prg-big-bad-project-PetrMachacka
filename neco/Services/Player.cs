using Projekt.Interfaces;
using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

public class Player : IPlayer
{
    private readonly string _name;

    public Player(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }

    public (string? Cords, string? Cords1) GetNextMove()
    {
        Console.WriteLine("Enter the two coordinates (Row, Column):");
        string? Cords = Console.ReadLine();
        string? Cords1 = Console.ReadLine();
        return (Cords, Cords1);
    }

}
