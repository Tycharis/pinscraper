using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
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
    }

    /*
    Example JSON response:

     {
       "tournament": {
       "accept_attachments": false,
       "allow_participant_match_reporting": true,
       "anonymous_voting": false,
       "category": null,
       "check_in_duration": null,
       "completed_at": null,
       "created_at": "2015-01-19T16:47:30-05:00",
       "created_by_api": false,
       "credit_capped": false,
       "description": "",
       "game_id": 600,
       "group_stages_enabled": false,
       "hide_forum": false,
       "hide_seeds": false,
       "hold_third_place_match": false,
       "id": 1086875,
       "max_predictions_per_user": 1,
       "name": "Sample Tournament 1",
       "notify_users_when_matches_open": true,
       "notify_users_when_the_tournament_ends": true,
       "open_signup": false,
       "participants_count": 4,
       "prediction_method": 0,
       "predictions_opened_at": null,
       "private": false,
       "progress_meter": 0,
       "pts_for_bye": "1.0",
       "pts_for_game_tie": "0.0",
       "pts_for_game_win": "0.0",
       "pts_for_match_tie": "0.5",
       "pts_for_match_win": "1.0",
       "quick_advance": false,
       "ranked_by": "match wins",
       "require_score_agreement": false,
       "rr_pts_for_game_tie": "0.0",
       "rr_pts_for_game_win": "0.0",
       "rr_pts_for_match_tie": "0.5",
       "rr_pts_for_match_win": "1.0",
       "sequential_pairings": false,
       "show_rounds": true,
       "signup_cap": null,
       "start_at": null,
       "started_at": "2015-01-19T16:57:17-05:00",
       "started_checking_in_at": null,
       "state": "underway",
       "swiss_rounds": 0,
       "teams": false,
       "tie_breaks": [
       "match wins vs tied",
       "game wins",
       "points scored"
       ],
       "tournament_type": "single elimination",
       "updated_at": "2015-01-19T16:57:17-05:00",
       "url": "sample_tournament_1",
       "description_source": "",
       "subdomain": null,
       "full_challonge_url": "http://challonge.com/sample_tournament_1",
       "live_image_url": "http://images.challonge.com/sample_tournament_1.png",
       "sign_up_url": null,
       "review_before_finalizing": true,
       "accepting_predictions": false,
       "participants_locked": true,
       "game_name": "Table Tennis",
       "participants_swappable": false,
       "team_convertable": false,
       "group_stages_were_started": false
       }
       }
     */
    public class TournamentResponse : ChallongeResponse
    {
        [JsonProperty("tournament")]
        public Tournament Tournament { get; set; }
    }
}
