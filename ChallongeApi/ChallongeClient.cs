using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChallongeApi
{
    public class ChallongeClient
    {
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// The Challonge API version this wrapper is updated for
        /// </summary>
        private const string ApiVersion = "v1";

        private const string BaseApiUrl = "https://api.challonge.com/" + ApiVersion + "/";

        private readonly string _apiKey;

        public ChallongeClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Gets a tournament by id
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="includeParticipants"></param>
        /// <param name="includeMatches"></param>
        /// <returns></returns>
        public Task<IChallongeResponse> GetTournament(
            int tournamentId,
            bool includeParticipants = false,
            bool includeMatches = false)
        {
            return GetTournament<int>(tournamentId, includeParticipants, includeMatches);
        }

        public Task<IChallongeResponse> GetTournament(
            string tournamentId,
            bool includeParticipants = false,
            bool includeMatches = false)
        {
            return GetTournament<string>(tournamentId, includeParticipants, includeMatches);
        }

        private async Task<IChallongeResponse> GetTournament<T>(
            T tournamentId,
            bool includeParticipants = false,
            bool includeMatches = false)
        {
            var getTournamentUri = new Uri($"{BaseApiUrl}tournaments/{tournamentId}.json?api_key={_apiKey}");

            using HttpResponseMessage response = await Client.GetAsync(getTournamentUri);

            if (response.IsSuccessStatusCode)
            {
                return await ChallongeResponse.FromHttpResponseMessageAsync<TournamentResponse>(response);
            }

            return response.StatusCode == HttpStatusCode.UnprocessableEntity
                ? await ChallongeResponse.FromHttpResponseMessageAsync<ErrorResponse>(response)
                : ErrorResponse.GetEmptyError(response.StatusCode);
        }

        public Task<IChallongeResponse> BulkAddParticipants(int tournamentId, IEnumerable<IParticipant> participants)
        {
            return BulkAddParticipants<int>(tournamentId, participants);
        }

        public Task<IChallongeResponse> BulkAddParticipants(string tournamentId, IEnumerable<IParticipant> participants)
        {
            return BulkAddParticipants<string>(tournamentId, participants);
        }

        private async Task<IChallongeResponse> BulkAddParticipants<T>(T tournamentId, IEnumerable<IParticipant> participants)
        {
            var postParticipantsUri = new Uri($"{BaseApiUrl}tournaments/{tournamentId}/participants/bulk_add.json?api_key={_apiKey}");

            var payload = new ParticipantPayload(participants);

            string serializedPayload = JsonConvert.SerializeObject(payload);

            var jsonContent = new StringContent(serializedPayload, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await Client.PostAsync(
                postParticipantsUri,
                jsonContent);

            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                return await ChallongeResponse.FromHttpResponseMessageAsync<ErrorResponse>(response);
            }

            return response.IsSuccessStatusCode
                ? await ChallongeResponse.FromHttpResponseMessageAsync<ChallongeResponse>(response)
                : ErrorResponse.GetEmptyError(response.StatusCode);
        }

        private class ParticipantPayload
        {
            [JsonProperty("participants")]
            public IEnumerable<IParticipant> Participants { get; set; }

            public ParticipantPayload(IEnumerable<IParticipant> participants)
            {
                Participants = participants;
            }
        }

        public Task<IEnumerable<IChallongeResponse>> GetAllParticipants(int tournamentId)
        {
            return GetAllParticipants<int>(tournamentId);
        }

        public Task<IEnumerable<IChallongeResponse>> GetAllParticipants(string tournamentId)
        {
            return GetAllParticipants<string>(tournamentId);
        }

        private async Task<IEnumerable<IChallongeResponse>> GetAllParticipants<T>(T tournamentId)
        {
            var getAParticipantUri = new Uri($"{BaseApiUrl}tournaments/{tournamentId}/participants.json?api_key={_apiKey}");

            using HttpResponseMessage response = await Client.GetAsync(getAParticipantUri);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ParticipantResponse>>(await response.Content.ReadAsStringAsync());
            }

            if (response.StatusCode != HttpStatusCode.UnprocessableEntity)
            {
                return new[]
                {
                    ErrorResponse.GetEmptyError(response.StatusCode)
                };
            }

            ErrorResponse errorResponse = await ChallongeResponse.FromHttpResponseMessageAsync<ErrorResponse>(response);

            return new[]
            {
                errorResponse
            };
        }

        public Task<IChallongeResponse> GetAParticipant(int tournamentId, int participantId)
        {
            return GetAParticipant<int>(tournamentId, participantId);
        }

        public Task<IChallongeResponse> GetAParticipant(string tournamentId, int participantId)
        {
            return GetAParticipant<string>(tournamentId, participantId);
        }

        public static IEnumerable<IParticipant> AssignSeeds(IEnumerable<IParticipant> participants)
        {
            return participants.Select((p, i) =>
            {
                p.Seed = i;

                return p;
            });
        }

        private async Task<IChallongeResponse> GetAParticipant<T>(T tournamentId, int participantId)
        {
            var getAParticipantUri = new Uri($"{BaseApiUrl}tournaments/{tournamentId}/participants/{participantId}.json?api_key={_apiKey}");

            using HttpResponseMessage response = await Client.GetAsync(getAParticipantUri);

            if (response.IsSuccessStatusCode)
            {
                return await ChallongeResponse.FromHttpResponseMessageAsync<ParticipantResponse>(response);
            }

            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                return await ChallongeResponse.FromHttpResponseMessageAsync<ErrorResponse>(response);
            }

            return ErrorResponse.GetEmptyError(response.StatusCode);
        }
    }

    public interface IParticipant
    {
        [JsonProperty("name")]
        string Name { get; set; }

        [JsonProperty("invite_name_or_email")]
        string InviteNameOrEmail { get; set; }

        [JsonProperty("seed")]
        int Seed { get; set; }

        /// <summary>
        /// Can be a maximum of 255 characters long
        /// </summary>
        [JsonProperty("misc")]
        string Miscellaneous { get; set; }
    }
}
