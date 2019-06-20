using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
    [PublicAPI]
    public class MatchAttachment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("match_id")]
        public int MatchId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("original_file_name")]
        public string OriginalFileName { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("asset_file_name")]
        public string AssetFileName { get; set; }

        [JsonProperty("asset_content_type")]
        public string AssetContentType { get; set; }

        [JsonProperty("asset_file_size")]
        public object AssetFileSize { get; set; }

        [JsonProperty("asset_url")]
        public string AssetUrl { get; set; }
    }

    [PublicAPI]
    public class MatchAttachmentResponse : ChallongeResponse
    {
        [JsonProperty("match_attachment")]
        public MatchAttachment MatchAttachment { get; set; }
    }
}
