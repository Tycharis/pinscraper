using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
    public class Match
    {
        [JsonProperty("attachment_count")]
        public int? AttachmentCount { get; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; }

        // TODO: type unknown
        [JsonProperty("group_id")]
        public object GroupId { get; }

        [JsonProperty("has_attachment")]
        public bool HasAttachment { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("identifier")]
        public string Identifier { get; }

        // TODO: type unknown
        [JsonProperty("location")]
        public object Location { get; }

        [JsonProperty("loser_id")]
        public int? LoserId { get; }

        [JsonProperty("player1_id")]
        public int? PlayerOneId { get; }

        [JsonProperty("player1_is_prereq_match_loser")]
        public bool PlayerOneIsPrerequisiteMatchLoser { get; }

        // TODO: type unknown
        [JsonProperty("player1_prereq_match_id")]
        public object PlayerOnePrerequisiteMatchId { get; }

        // TODO: type unknown
        [JsonProperty("player1_votes")]
        public object PlayerOneVotes { get; }

        [JsonProperty("player2_id")]
        public int? PlayerTwoId { get; }

        [JsonProperty("player2_is_prereq_match_loser")]
        public bool PlayerTwoIsPrerequisiteMatchLoser { get; }

        // TODO: type unknown
        [JsonProperty("player2_prereq_match_id")]
        public object PlayerTwoPrerequisiteMatchId { get; }

        // TODO: type unknown
        [JsonProperty("player2_votes")]
        public object PlayerTwoVotes { get; }

        [JsonProperty("round")]
        public int Round { get; }

        [JsonProperty("scheduled_time")]
        public DateTime? ScheduledTime { get; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; }

        [JsonProperty("state")]
        public MatchState State { get; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; }

        // TODO: type unknown
        [JsonProperty("underway_at")]
        public object UnderwayAt { get; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; }

        [JsonProperty("winner_id")]
        public int? WinnerId { get; }

        [JsonProperty("prerequisite_match_ids_csv")]
        public string PrerequisiteMatchIdsCsv { get; }

        [JsonProperty("scores_csv")]
        public string ScoresCsv { get; }
    }

    public class MatchResponse : ChallongeResponse
    {
        [JsonProperty("match")]
        public Match Match { get; }
    }

    public enum MatchState
    {
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "complete")]
        Complete
    }
}
