using System;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
    public class Participant
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("checked_in_at")]
        public DateTime? CheckedInAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        // TODO: type unknown
        [JsonProperty("final_rank")]
        public object FinalRank { get; set; }

        // TODO: type unknown
        [JsonProperty("group_id")]
        public object GroupId { get; set; }

        // TODO: type unknown
        [JsonProperty("icon")]
        public object Icon { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("invitation_id")]
        public int? InvitationId { get; set; }

        [JsonProperty("invite_email")]
        public string InviteEmail { get; set; }

        [JsonProperty("misc")]
        public string Miscellaneous { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("on_waiting_list")]
        public bool OnWaitingList { get; set; }

        [JsonProperty("seed")]
        public int Seed { get; set; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("challonge_username")]
        public string ChallongeUsername { get; set; }

        // TODO: type unknown
        [JsonProperty("challonge_email_address_verified")]
        public object ChallongeEmailAddressVerified { get; set; }

        [JsonProperty("removable")]
        public bool Removable { get; set; }

        [JsonProperty("participatable_or_invitation_attached")]
        public bool ParticipatableOrInvitationAttached { get; set; }

        [JsonProperty("confirm_remove")]
        public bool ConfirmRemove { get; set; }

        [JsonProperty("invitation_pending")]
        public bool InvitationPending { get; set; }

        [JsonProperty("display_name_with_invitation_email_address")]
        public string DisplayNameWithInvitationEmailAddress { get; set; }

        [JsonProperty("email_hash")]
        public string EmailHash { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("attached_participatable_portrait_url")]
        public string AttachedParticipatablePortraitUrl { get; set; }

        [JsonProperty("can_check_in")]
        public bool CanCheckIn { get; set; }

        [JsonProperty("checked_in")]
        public bool CheckedIn { get; set; }

        [JsonProperty("reactivatable")]
        public bool Reactivatable { get; set; }
    }

    public class ParticipantResponse : ChallongeResponse
    {
        [JsonProperty("participant")]
        public Participant Participant { get; set; }
    }
}
