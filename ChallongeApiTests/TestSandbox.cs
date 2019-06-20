using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallongeApi;
using ChallongeApi.Matches;
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
            MyParticipant[] participants =
            {
                new MyParticipant
                {
                    Name = "asdf"
                },
                new MyParticipant
                {
                    Name = "foo"
                },
                new MyParticipant
                {
                    Name = "bar"
                }
            };

            foreach (MyParticipant participant in participants.Set(x => x.Name += " addedText"))
            {
                Console.WriteLine(participant.Name);
            }

            await TestChallongeApi();
        }

        private static async Task TestChallongeApi()
        {
            const string API_KEY = "8nwaN3JaAO3Tf1EwNRFCQ6343tokAW4FXKUwwVMM";

            var client = new ChallongeClient(API_KEY);

            const string TOURNAMENT_URL = "testurlstrthingy";

            Console.WriteLine($"Getting tournament {TOURNAMENT_URL}");

            IChallongeResponse tournament = await client.TournamentApi.GetTournament(TOURNAMENT_URL);

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

            IChallongeResponse addParticipantsResponse = await client.ParticipantApi.BulkAddParticipants(TOURNAMENT_URL, participants);

            ConsoleWriteFormattedJson(addParticipantsResponse);

            IEnumerable<IChallongeResponse> participantsResponse = await client.ParticipantApi.GetAllParticipants(TOURNAMENT_URL);

            if (participantsResponse is IEnumerable<ParticipantResponse> validParticipantResponse)
            {
                IEnumerable<Participant> asParticipantObjects = validParticipantResponse.Select(res => res.Participant);

                Console.WriteLine($"Found all of the participants for tournament: {TOURNAMENT_URL}");

                ConsoleWriteFormattedJson(asParticipantObjects);

                Participant firstParticipant = asParticipantObjects.FirstOrDefault();

                IChallongeResponse singleParticipantResponse = await client.ParticipantApi.GetAParticipant(TOURNAMENT_URL, firstParticipant.Id);

                if (singleParticipantResponse is ParticipantResponse validSingleParticipantResponse)
                {
                    Console.WriteLine($"Found participant with id: {firstParticipant.Id}");

                    ConsoleWriteFormattedJson(validSingleParticipantResponse);
                }
            }

            IEnumerable<IChallongeResponse> matchesResponse = await client.MatchApi.GetAllMatches(TOURNAMENT_URL);

            if (matchesResponse is IEnumerable<MatchResponse> validMatchesResponse)
            {
                ConsoleWriteFormattedJson(validMatchesResponse);

                Match firstMatch = validMatchesResponse.FirstOrDefault()?.Match;

                if (firstMatch != null)
                {
                    IChallongeResponse matchResponse = await client.MatchApi.GetAMatch(
                        TOURNAMENT_URL,
                        firstMatch.Id,
                        true);

                    ConsoleWriteFormattedJson(matchResponse);

                    var matchScore = new MatchScore(
                        new MatchParticipant(firstMatch.PlayerOneId.Value, 3),
                        new MatchParticipant(firstMatch.PlayerTwoId.Value, 5));

                    IChallongeResponse updateFirstMatchResponse = await client.MatchApi.UpdateAMatch(
                        TOURNAMENT_URL,
                        firstMatch.Id,
                        matchScore);

                    ConsoleWriteFormattedJson(updateFirstMatchResponse);

                    IEnumerable<IChallongeResponse> attachments = await client.AttachmentApi.GetAllAttachments(
                        TOURNAMENT_URL,
                        firstMatch.Id);

                    ConsoleWriteFormattedJson(attachments);

                    if (attachments is IEnumerable<MatchAttachmentResponse> validAttachmentResponses)
                    {
                        MatchAttachmentResponse attachment = validAttachmentResponses.FirstOrDefault();

                        if (attachment == null)
                        {
                            Console.WriteLine("attachment not found!!!!!!!!!!!");
                        }
                        else
                        {
                            IChallongeResponse getAttachmentResponse = await client.AttachmentApi.GetAttachment(
                                TOURNAMENT_URL,
                                firstMatch.Id,
                                attachment.MatchAttachment.Id);

                            ConsoleWriteFormattedJson(getAttachmentResponse);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n\nFIRST MATCH NOT FOUND\n\n");
                }
            }
            else
            {
                ConsoleWriteFormattedJson(matchesResponse);
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
