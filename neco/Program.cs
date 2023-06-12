using Microsoft.Extensions.DependencyInjection;
using Projekt.Interfaces;

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
                .AddSingleton<IPlayer>(Player1 => new Player("Player 1"))
                .AddSingleton<IPlayer>(Player2 => new Player("Player 2"))
                .AddSingleton<Game>()
                .BuildServiceProvider();
        }
    }
}
