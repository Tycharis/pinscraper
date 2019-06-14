using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
        /// <param name="id"></param>
        /// <param name="includeParticipants"></param>
        /// <param name="includeMatches"></param>
        /// <returns></returns>
        public Task<IChallongeResponse> GetTournament(
            int id,
            bool includeParticipants = false,
            bool includeMatches = false)
        {
            return GetTournament<int>(id, includeParticipants, includeMatches);
        }

        public Task<IChallongeResponse> GetTournament(
            string stringId,
            bool includeParticipants = false,
            bool includeMatches = false)
        {
            return GetTournament<string>(stringId, includeParticipants, includeMatches);
        }

        private async Task<IChallongeResponse> GetTournament<T>(
            T tournamentIdentifier,
            bool includeParticipants = false,
            bool includeMatches = false)
        {
            var getTournamentUri = new Uri($"{BaseApiUrl}tournaments/{tournamentIdentifier}.json?api_key={_apiKey}");

            using HttpResponseMessage response = await Client.GetAsync(getTournamentUri);

            if (response.IsSuccessStatusCode)
            {
                return await ChallongeResponse.FromHttpResponseMessageAsync<TournamentResponse>(response);
            }

            return response.StatusCode == HttpStatusCode.UnprocessableEntity
                ? await ChallongeResponse.FromHttpResponseMessageAsync<ErrorResponse>(response)
                : ErrorResponse.GetEmptyError(response.StatusCode);
        }

        public Task<IChallongeResponse> BulkAddParticipants(int id)
        {
            return BulkAddParticipants(id, participants);
        }

        private async Task<IChallongeResponse> BulkAddParticipants<T>(T tournamentIdentifier, ICollection<IParticipant> participants)
        {
            var postParticipantsUri = new Uri($"{BaseApiUrl}tournaments/{tournamentIdentifier}/participants/bulk_add.json?api_key={_apiKey}");

            using HttpResponseMessage response = await Client.PostAsync(postParticipantsUri);

            return response.IsSuccessStatusCode
                ? await ChallongeResponse.FromHttpResponseMessageAsync<ChallongeResponse>(response)
                : ErrorResponse.GetEmptyError(response.StatusCode);
        }
    }

    public interface IParticipant
    {
        string Name { get; set; }

        string InviteNameOrEmail { get; set; }

        int Seed { get; set; }

        /// <summary>
        /// Can be a maximum of 255 characters long
        /// </summary>
        string Miscellaneous { get; set; }
    }
}
