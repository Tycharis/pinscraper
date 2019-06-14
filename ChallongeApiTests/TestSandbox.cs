using System;
using System.Linq;
using ChallongeApi;
using Newtonsoft.Json;

namespace ChallongeApiTests
{
    internal class TestSandbox
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const string USERNAME = "JoshuaHall";

            const string API_KEY = "j5DC3td0sunw11mOHdDVwR6XnLLOuTXmJE8WLFRI";

            var client = new ChallongeClient(API_KEY);

            const string TOURNAMENT_URL = "testurlstrthingy";

            Console.WriteLine($"Getting tournament {TOURNAMENT_URL}");

            ChallongeResponse tournament = client.GetTournament(TOURNAMENT_URL).Result;

            Console.WriteLine("Done!");

            if (tournament is ErrorResponse response)
            {
                if (response.Errors.Any())
                {
                    Console.WriteLine("Validation error(s) occurred:");

                    foreach (string error in response.Errors)
                    {
                        Console.WriteLine(error);
                    }
                }
                else
                {
                    Console.WriteLine("Error occured.");
                    Console.WriteLine($"Status code: {response.StatusCode}");
                }
            }
            else
            {
                Console.WriteLine("Success!");

                var tournamentResponse = (TournamentResponse)tournament;

                Console.WriteLine(JsonConvert.SerializeObject(tournamentResponse, Formatting.Indented));
            }

            Console.ReadLine();
        }
    }
}
