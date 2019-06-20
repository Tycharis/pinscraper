using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
    [PublicAPI]
    public class Tournament
    {
        [JsonProperty("accept_attachments")]
        public bool AcceptAttachments { get; set; }

        [JsonProperty("allow_participant_match_reporting")]
        public bool AllowParticipantMatchReporting { get; set; }

        [JsonProperty("anonymous_voting")]
        public bool AnonymousVoting { get; set; }

        // TODO: type unknown
        [JsonProperty("category")]
        public object Category { get; set; }

        [JsonProperty("check_in_duration")]
        public int CheckInDuration { get; set; }

        [JsonProperty("completed_at")]
        public DateTime? CompletedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("created_by_api")]
        public bool CreatedByApi { get; set; }

        [JsonProperty("credit_capped")]
        public bool CreditCapped { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("game_id")]
        public int GameId { get; set; }

        [JsonProperty("group_stages_enabled")]
        public bool GroupStagesEnabled { get; set; }

        [JsonProperty("hide_forum")]
        public bool HideForum { get; set; }

        [JsonProperty("hide_seeds")]
        public bool HideSeeds { get; set; }

        [JsonProperty("hold_third_place_match")]
        public bool HoldThirdPlaceMatch { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("max_predictions_per_user")]
        public int MaxPredictionsPerUser { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notify_users_when_matches_open")]
        public bool NotifyUsersWhenMatchesOpen { get; set; }

        [JsonProperty("notify_users_when_the_tournament_ends")]
        public bool NotifyUsersWhenTheTournamentEnds { get; set; }

        [JsonProperty("open_signup")]
        public bool OpenSignup { get; set; }

        [JsonProperty("participants_count")]
        public int ParticipantsCount { get; set; }

        [JsonProperty("prediction_method")]
        public int PredictionMethod { get; set; }

        [JsonProperty("predictions_opened_at")]
        public DateTime? PredictionsOpenedAt { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("progress_meter")]
        public int ProgressMeter { get; set; }

        [JsonProperty("pts_for_bye")]
        public string PointsForBye { get; set; }

        [JsonProperty("pts_for_game_tie")]
        public string PointsForGameTie { get; set; }

        [JsonProperty("pts_for_game_win")]
        public string PointsForGameWin { get; set; }

        [JsonProperty("pts_for_match_tie")]
        public string PointsForMatchTie { get; set; }

        [JsonProperty("pts_for_match_win")]
        public string PointsForMatchWin { get; set; }

        [JsonProperty("quick_advance")]
        public bool QuickAdvance { get; set; }

        [JsonProperty("ranked_by")]
        public string RankedBy { get; set; }

        [JsonProperty("require_score_agreement")]
        public bool RequireScoreAgreement { get; set; }

        [JsonProperty("rr_pts_for_game_tie")]
        public string RrPointsForGameTie { get; set; }

        [JsonProperty("rr_pts_for_game_win")]
        public string RrPointsForGameWin { get; set; }

        [JsonProperty("rr_pts_for_match_tie")]
        public string RrPointsForMatchTie { get; set; }

        [JsonProperty("rr_pts_for_match_win")]
        public string RrPointsForMatchWin { get; set; }

        [JsonProperty("sequential_pairings")]
        public bool SequentialPairings { get; set; }

        [JsonProperty("show_rounds")]
        public bool ShowRounds { get; set; }

        [JsonProperty("signup_cap")]
        public int SignupCap { get; set; }

        [JsonProperty("start_at")]
        public DateTime? StartAt { get; set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("started_checking_in_at")]
        public DateTime? StartedCheckingInAt { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("swiss_rounds")]
        public int SwissRounds { get; set; }

        [JsonProperty("teams")]
        public bool Teams { get; set; }

        [JsonProperty("tie_breaks")]
        public IEnumerable<string> TieBreaks { get; set; }

        [JsonProperty("tournament_type")]
        public string TournamentType { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("description_source")]
        public string DescriptionSource { get; set; }

        // TODO: type unknown
        [JsonProperty("subdomain")]
        public object Subdomain { get; set; }

        [JsonProperty("full_challonge_url")]
        public string FullChallongeUrl { get; set; }

        [JsonProperty("live_image_url")]
        public string LiveImageUrl { get; set; }

        [JsonProperty("sign_up_url")]
        public string SignUpUrl { get; set; }

        [JsonProperty("review_before_finalizing")]
        public bool ReviewBeforeFinalizing { get; set; }

        [JsonProperty("accepting_predictions")]
        public bool AcceptingPredictions { get; set; }

        [JsonProperty("participants_locked")]
        public bool ParticipantsLocked { get; set; }

        [JsonProperty("game_name")]
        public string GameName { get; set; }

        [JsonProperty("participants_swappable")]
        public bool ParticipantsSwappable { get; set; }

        [JsonProperty("team_convertable")]
        public bool TeamConvertable { get; set; }

        [JsonProperty("group_stages_were_started")]
        public bool GroupStagesWereStarted { get; set; }

        [JsonProperty("participants")]
        public IEnumerable<Participant> Participants { get; set; }

        [JsonProperty("matches")]
        public IEnumerable<Match> Matches { get; set; }
    }

    [PublicAPI]
    public class TournamentResponse : ChallongeResponse
    {
        [JsonProperty("tournament")]
        public Tournament Tournament { get; set; }
    }
}
