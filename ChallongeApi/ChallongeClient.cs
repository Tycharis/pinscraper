using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChallongeApi.Enums;
using ChallongeApi.Responses;
using JetBrains.Annotations;
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
        [PublicAPI]
        public Task<IChallongeResponse> GetTournament(
            int tournamentId,
            bool includeParticipants = false,
            bool includeMatches = false)
        {
            return GetTournament<int>(tournamentId, includeParticipants, includeMatches);
        }

        [PublicAPI]
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
            where T : IConvertible
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

        [PublicAPI]
        public Task<IChallongeResponse> BulkAddParticipants(int tournamentId, IEnumerable<IParticipant> participants)
        {
            return BulkAddParticipants<int>(tournamentId, participants);
        }

        [PublicAPI]
        public Task<IChallongeResponse> BulkAddParticipants(string tournamentId, IEnumerable<IParticipant> participants)
        {
            return BulkAddParticipants<string>(tournamentId, participants);
        }

        private async Task<IChallongeResponse> BulkAddParticipants<T>(T tournamentId, IEnumerable<IParticipant> participants)
            where T : IConvertible
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

        [PublicAPI]
        public Task<IEnumerable<IChallongeResponse>> GetAllParticipants(int tournamentId)
        {
            return GetAllParticipants<int>(tournamentId);
        }

        [PublicAPI]
        public Task<IEnumerable<IChallongeResponse>> GetAllParticipants(string tournamentId)
        {
            return GetAllParticipants<string>(tournamentId);
        }

        private async Task<IEnumerable<IChallongeResponse>> GetAllParticipants<T>(T tournamentId)
            where T : IConvertible
        {
            var getAParticipantUri = new Uri($"{BaseApiUrl}tournaments/{tournamentId}/participants.json?api_key={_apiKey}");

            using HttpResponseMessage response = await Client.GetAsync(getAParticipantUri);

            if (response.IsSuccessStatusCode)
            {
                var participantResponses = JsonConvert.DeserializeObject<IEnumerable<ParticipantResponse>>(await response.Content.ReadAsStringAsync());

                return participantResponses.Set(participantResponse => participantResponse.StatusCode = HttpStatusCode.OK);
            }

            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                return new[]
                {
                    await ChallongeResponse.FromHttpResponseMessageAsync<ErrorResponse>(response)
                };
            }

            return new[]
            {
                ErrorResponse.GetEmptyError(response.StatusCode)
            };
        }

        [PublicAPI]
        public Task<IChallongeResponse> GetAParticipant(int tournamentId, int participantId)
        {
            return GetAParticipant<int>(tournamentId, participantId);
        }

        [PublicAPI]
        public Task<IChallongeResponse> GetAParticipant(string tournamentId, int participantId)
        {
            return GetAParticipant<string>(tournamentId, participantId);
        }

        [PublicAPI]
        public static IEnumerable<IParticipant> AssignSeeds(IEnumerable<IParticipant> participants)
        {
            return participants.Set((participant, i) => participant.Seed = i + 1);
        }

        private async Task<IChallongeResponse> GetAParticipant<T>(T tournamentId, int participantId)
            where T : IConvertible
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

        public Task<IEnumerable<IChallongeResponse>> GetAllMatches(
            int tournamentId,
            GetMatchState state = GetMatchState.All,
            int? participantId = null)
        {
            return GetAllMatches<int>(tournamentId, state, participantId);
        }

        public Task<IEnumerable<IChallongeResponse>> GetAllMatches(
            string tournamentId,
            GetMatchState state = GetMatchState.All,
            int? participantId = null)
        {
            return GetAllMatches<string>(tournamentId, state, participantId);
        }

        private async Task<IEnumerable<IChallongeResponse>> GetAllMatches<T>(
            T tournamentId,
            GetMatchState state = GetMatchState.All,
            int? participantId = null)
            where T : IConvertible
        {
            string allMatchesUriStr = $"{BaseApiUrl}tournaments/{tournamentId}/matches.json?api_key={_apiKey}";

            if (state != GetMatchState.All)
            {
                allMatchesUriStr += $"&state={state.AsString()}";
            }

            if (participantId != null)
            {
                allMatchesUriStr += $"&participant_id={participantId.Value}";
            }

            var getAllMatchesUri = new Uri(allMatchesUriStr);

            using HttpResponseMessage response = await Client.GetAsync(getAllMatchesUri);

            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();

                var matchesResponse = JsonConvert.DeserializeObject<IEnumerable<MatchResponse>>(responseStr);

                return matchesResponse.Set(x => x.StatusCode = HttpStatusCode.OK);
            }

            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                return new[]
                {
                    await ChallongeResponse.FromHttpResponseMessageAsync<ErrorResponse>(response)
                };
            }

            return new[]
            {
                ErrorResponse.GetEmptyError(response.StatusCode)
            };
        }

        public Task<IChallongeResponse> GetAMatch(
            int tournamentId,
            int matchId,
            bool includeAttachments = false)
        {
            return GetAMatch<int>(tournamentId, matchId, includeAttachments);
        }

        public Task<IChallongeResponse> GetAMatch(
            string tournamentId,
            int matchId,
            bool includeAttachments = false)
        {
            return GetAMatch<string>(tournamentId, matchId, includeAttachments);
        }

        private async Task<IChallongeResponse> GetAMatch<T>(
            T tournamentId,
            int matchId,
            bool includeAttachments = false)
            where T : IConvertible
        {
            var getAMatchUri = new Uri($"{BaseApiUrl}tournaments/{tournamentId}/matches/{matchId}.json?api_key={_apiKey}");

            using HttpResponseMessage response = await Client.GetAsync(getAMatchUri);

            return await HandleSingleMatchResponse(response);
        }

        [PublicAPI]
        public Task<IChallongeResponse> UpdateAMatch(
            int tournamentId,
            int matchId,
            [NotNull] SetScores setScores,
            int? playerOneVotes = null,
            int? playerTwoVotes = null)
        {
            return UpdateAMatch<int>(
                tournamentId,
                matchId,
                setScores,
                playerOneVotes,
                playerTwoVotes);
        }

        [PublicAPI]
        public Task<IChallongeResponse> UpdateAMatch(
            string tournamentId,
            int matchId,
            [NotNull] SetScores setScores,
            int? playerOneVotes = null,
            int? playerTwoVotes = null)
        {
            return UpdateAMatch<string>(
                tournamentId,
                matchId,
                setScores,
                playerOneVotes,
                playerTwoVotes);
        }

        [PublicAPI]
        public Task<IChallongeResponse> UpdateAMatch(
            int tournamentId,
            int matchId,
            [NotNull] MatchScore matchScore,
            int? playerOneVotes = null,
            int? playerTwoVotes = null)
        {
            return UpdateAMatch<int>(
                tournamentId,
                matchId,
                matchScore,
                playerOneVotes,
                playerTwoVotes);
        }

        [PublicAPI]
        public Task<IChallongeResponse> UpdateAMatch(
            string tournamentId,
            int matchId,
            [NotNull] MatchScore matchScore,
            int? playerOneVotes = null,
            int? playerTwoVotes = null)
        {
            return UpdateAMatch<string>(
                tournamentId,
                matchId,
                matchScore,
                playerOneVotes,
                playerTwoVotes);
        }

        private async Task<IChallongeResponse> UpdateAMatch<T>(
            T tournamentId,
            int matchId,
            [NotNull] MatchScore matchScore,
            int? playerOneVotes = null,
            int? playerTwoVotes = null)
            where T : IConvertible
        {
            Uri updateAMatchUri = GetUpdateAMatchUri(tournamentId, matchId);

            int? winnerId = matchScore.GetWinnerId();

            string winnerIdStr = winnerId == null
                ? "tie"
                : winnerId.Value.ToString();

            var payload = new UpdateMatchPayload(
                new MatchPayload(
                    matchScore.ToString(),
                    winnerIdStr,
                    playerOneVotes,
                    playerTwoVotes));

            string serializedPayload = JsonConvert.SerializeObject(payload);

            var jsonContent = new StringContent(serializedPayload, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await Client.PutAsync(updateAMatchUri, jsonContent);

            return await HandleSingleMatchResponse(response);
        }

        private async Task<IChallongeResponse> UpdateAMatch<T>(
            T tournamentId,
            int matchId,
            [NotNull] SetScores setScores,
            int? playerOneVotes = null,
            int? playerTwoVotes = null)
            where T : IConvertible
        {
            Uri updateAMatchUri = GetUpdateAMatchUri(tournamentId, matchId);

            int? winnerId = setScores.GetWinnerId();

            string winnerIdStr = winnerId == null
                ? "tie"
                : winnerId.Value.ToString();

            var payload = new UpdateMatchPayload(
                new MatchPayload(
                    setScores.GetSetScoresString(),
                    winnerIdStr,
                    playerOneVotes,
                    playerTwoVotes));

            string serializedPayload = JsonConvert.SerializeObject(payload);

            var jsonContent = new StringContent(serializedPayload, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await Client.PutAsync(updateAMatchUri, jsonContent);

            return await HandleSingleMatchResponse(response);
        }

        private Uri GetUpdateAMatchUri<T>(T tournamentId, int matchId)
            where T : IConvertible
        {
            return new Uri($"{BaseApiUrl}tournaments/{tournamentId}/matches/{matchId}.json?api_key={_apiKey}");
        }

        private class UpdateMatchPayload
        {
            [JsonProperty("match")]
            public MatchPayload Match { get; }

            public UpdateMatchPayload(MatchPayload match)
            {
                Match = match;
            }
        }

        private class MatchPayload
        {
            [JsonProperty("scores_csv")]
            public string ScoresCsv { get; }

            [JsonProperty("winner_id")]
            public string WinnerId { get; }

            [JsonProperty("player1_votes")]
            public int? PlayerOneVotes { get; }

            [JsonProperty("player2_votes")]
            public int? PlayerTwoVotes { get; }

            public MatchPayload(string scores, string winnerId, int? oneVotes, int? twoVotes)
            {
                ScoresCsv = scores;
                WinnerId = winnerId;
                PlayerOneVotes = oneVotes;
                PlayerTwoVotes = twoVotes;
            }
        }

        private static async Task<IChallongeResponse> HandleSingleMatchResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await ChallongeResponse.FromHttpResponseMessageAsync<MatchResponse>(response);
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
        /// <summary>
        /// The display name used in the bracket.
        /// </summary>
        [JsonProperty("name")]
        string Name { get; set; }

        /// <summary>
        /// The Challonge username or email to invite a user with.
        /// </summary>
        [JsonProperty("invite_name_or_email")]
        string InviteNameOrEmail { get; set; }

        /// <summary>
        /// Seed used in the bracket.
        /// </summary>
        /// <remarks>
        /// Minimum of 1.
        /// </remarks>
        [JsonProperty("seed")]
        int Seed { get; set; }

        /// <summary>
        /// Miscellaneous data, never displayed anywhere, only stored in Challonge's database.
        /// </summary>
        /// <remarks>
        /// Can be a maximum of 255 characters long.
        /// </remarks>
        [JsonProperty("misc")]
        string Miscellaneous { get; set; }
    }
}
