using PAS.Interfaces;
using PAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PAS.DAO;

namespace PAS
{
    class Program
    {
        private static IPlayerAuctionSystemClient _playerAutctionSystemClient;
        static void Main(string[] args)
        {
            try
            {
                var collection = new ServiceCollection();
                collection.AddScoped<IPlayerAuctionSystemClient, PlayerAuctionSystemClient>();
                collection.AddScoped<IPlayerAuctionSystemManager, PlayerAuctionSystemManager>();
                collection.AddScoped<IGenericRepository<Player>, GenericRepository<Player>>();
                collection.AddScoped<IGenericRepository<Team>, GenericRepository<Team>>();
                collection.AddScoped<IGenericRepository<Team_Player>, GenericRepository<Team_Player>>();
                // ...
                // Add other services
                // ...
                var serviceProvider = collection.BuildServiceProvider();
                var service = serviceProvider.GetService<IPlayerAuctionSystemClient>();
                showMenu(service);
              
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private static void showMenu(IPlayerAuctionSystemClient playerAuctionSystemClient)
        {
            Console.WriteLine("Player Auction System");
            Console.WriteLine();
            Console.WriteLine("1. Add a Player");
            Console.WriteLine("2. Display Player");
            Console.WriteLine("3. Exit");
            var result = Console.ReadLine();
            switch (Convert.ToInt32(result))
            {
                case 1:
                    AddPlayer(playerAuctionSystemClient);
                    break;
                case 2:
                    DisplayPlayers(playerAuctionSystemClient);
                    break;
                case 3:
                    Exit();
                    break;
                default:
                    break;
            }
        }


        private static void AddPlayer(IPlayerAuctionSystemClient playerAuctionSystemClient)
        {
            TeamPlayerObject teamPlayerObject = new TeamPlayerObject();
            Console.WriteLine("Add a player!!");
            Console.WriteLine("-----------------");
            Console.Write("Enter Player Name:");
            teamPlayerObject.Player_Name = Console.ReadLine();
            Console.Write("Enter Category:");
            teamPlayerObject.Category = Console.ReadLine();
            Console.Write("Enter Highest Score:");
            teamPlayerObject.HighestScore = Convert.ToInt16(Console.ReadLine());
            Console.Write("Enter Best Figure");
            teamPlayerObject.BestFigure = Console.ReadLine();
            Console.Write("Enter Team Name:");
            teamPlayerObject.TeamName = Console.ReadLine();
            int playerNo = _playerAutctionSystemClient.AddPlayer(teamPlayerObject);

            Console.WriteLine("Player added sucessfully with player No:" + playerNo);
        }

        private static void DisplayPlayers(IPlayerAuctionSystemClient playerAuctionSystemClient)
        {
            List<Player> players = GetPlayers(playerAuctionSystemClient);
            showPlayers(players);
        }

        private static List<Player> GetPlayers(IPlayerAuctionSystemClient playerAuctionSystemClient)
        {
            Console.WriteLine("Display Players!!");
            Console.WriteLine("-----------------");
            Console.WriteLine("Enter Team Name:");
            string teamName = Console.ReadLine();
            return playerAuctionSystemClient.DisplayPlayer(teamName);
        }

        private static void showPlayers(List<Player> players)
        {
            Console.WriteLine("Player Name       Category");
            Console.WriteLine("----------------------------");
            foreach (Player player in players)
            {
                Console.WriteLine(player.Player_Name + "          " + player.Category);
            }
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
