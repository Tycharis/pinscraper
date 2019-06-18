using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallongeApi;
using ChallongeApi.Responses;
using Newtonsoft.Json;

namespace ChallongeApiTests
{
    /// <summary>
    /// Quick and dirty testing area for experimenting, I will do some actual unit testing later
    /// </summary>
    internal class TestSandbox
    {
        public static async Task Main(string[] args)
        {
            const string API_KEY = "j5DC3td0sunw11mOHdDVwR6XnLLOuTXmJE8WLFRI";

            var client = new ChallongeClient(API_KEY);

            const string TOURNAMENT_URL = "testshitjasdfoasihdfasdf";

            Console.WriteLine($"Getting tournament {TOURNAMENT_URL}");

            IChallongeResponse tournament = await client.GetTournament(TOURNAMENT_URL);

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

                ConsoleWriteFormattedJson(tournamentResponse);
            }

            IEnumerable<IParticipant> participants = ChallongeClient.AssignSeeds(
                new[] {
                    new MyParticipant
                    {
                        Name = "TestGuyasdf334",
                        InviteNameOrEmail = "test@example.com"
                    },
                    new MyParticipant
                    {
                        Name = "NewPlayer213",
                        InviteNameOrEmail = "asdf@asdffff.com"
                    },
                    new MyParticipant
                    {
                        Name = "CoolDudexd412",
                        InviteNameOrEmail = "iiii@asdfffffuu.com"
                    },
                    new MyParticipant
                    {
                        Name = "Joshy54100asdf",
                        InviteNameOrEmail = "joshuahallmail@gmail.com"
                    }
                });

            IChallongeResponse addParticipantsResponse = await client.BulkAddParticipants(TOURNAMENT_URL, participants);

            ConsoleWriteFormattedJson(addParticipantsResponse);

            IEnumerable<IChallongeResponse> participantsResponse = await client.GetAllParticipants(TOURNAMENT_URL);

            if (participantsResponse is IEnumerable<ParticipantResponse> validParticipantResponse)
            {
                IEnumerable<Participant> asParticipantObjects = validParticipantResponse.Select(res => res.Participant);

                Console.WriteLine($"Found all of the participants for tournament: {TOURNAMENT_URL}");

                ConsoleWriteFormattedJson(asParticipantObjects);

                Participant firstParticipant = asParticipantObjects.FirstOrDefault();

                IChallongeResponse singleParticipantResponse = await client.GetAParticipant(TOURNAMENT_URL, firstParticipant.Id);

                if (singleParticipantResponse is ParticipantResponse validSingleParticipantResponse)
                {
                    Console.WriteLine($"Found participant with id: {firstParticipant.Id}");

                    ConsoleWriteFormattedJson(validSingleParticipantResponse);
                }
            }

            Console.ReadLine();
        }

        private static void ConsoleWriteFormattedJson(object value)
        {
            Console.WriteLine(JsonConvert.SerializeObject(value, Formatting.Indented));
        }
    }

    public class MyParticipant : IParticipant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("seed")]
        public int Seed { get; set; }

        [JsonProperty("invite_name_or_email")]
        public string InviteNameOrEmail { get; set; }

        [JsonProperty("misc")]
        public string Miscellaneous { get; set; }
    }
}
