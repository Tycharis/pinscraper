using System;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
    public class Match
    {
        // TODO: type unknown
        [JsonProperty("attachment_count")]
        public object AttachmentCount { get; set; }

        public DateTime CreatedAt { get; set; }

        // TODO: type unknown
        [JsonProperty("group_id")]
        public object GroupId { get; set; }

        [JsonProperty("has_attachment")]
        public bool HasAttachment { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        // TODO: type unknown
        [JsonProperty("location")]
        public object Location { get; set; }

        [JsonProperty("loser_id")]
        public int? LoserId { get; set; }

        [JsonProperty("player1_id")]
        public int PlayerOneId { get; set; }

        [JsonProperty("player1_is_prereq_match_loser")]
        public bool PlayerOneIsPrerequisiteMatchLoser { get; set; }

        // TODO: type unknown
        [JsonProperty("player1_prereq_match_id")]
        public object PlayerOnePrerequisiteMatchId { get; set; }

        // TODO: type unknown
        [JsonProperty("player1_votes")]
        public object PlayerOneVotes { get; set; }

        [JsonProperty("player2_id")]
        public int PlayerTwoId { get; set; }

        [JsonProperty("player2_is_prereq_match_loser")]
        public bool PlayerTwoIsPrerequisiteMatchLoser { get; set; }

        // TODO: type unknown
        [JsonProperty("player2_prereq_match_id")]
        public object PlayerTwoPrerequisiteMatchId { get; set; }

        // TODO: type unknown
        [JsonProperty("player2_votes")]
        public object PlayerTwoVotes { get; set; }

        [JsonProperty("round")]
        public int Round { get; set; }

        [JsonProperty("scheduled_time")]
        public DateTime? ScheduledTime { get; set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("underway_at")]
        public object UnderwayAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("winner_id")]
        public int? WinnerId { get; set; }

        [JsonProperty("prerequisite_match_ids_csv")]
        public string PrerequisiteMatchIdsCsv { get; set; }

        [JsonProperty("scores_csv")]
        public string ScoresCsv { get; set; }
    }

    public class MatchResponse : ChallongeResponse
    {
        public Match Match { get; set; }
    }
}
