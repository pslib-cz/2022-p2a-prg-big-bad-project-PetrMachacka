using Microsoft.Extensions.DependencyInjection;
using Projekt.Interfaces;
using System;

namespace Projekt
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices();
            var game = serviceProvider.GetService<Game>();
            game.Start();
        }

        static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IGameEngine, GameEngine>()
                .AddSingleton<IPlayer>(provider => new Player("Player 1")) 
                .AddSingleton<IPlayer>(provider => new Player("Player 2")) 
                .AddSingleton<Game>()
                .BuildServiceProvider();
        }
    }
}
