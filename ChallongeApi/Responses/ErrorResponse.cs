using System;
using System.Collections.Generic;
using System.Net;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ChallongeApi.Responses
{
    [PublicAPI]
    public class ErrorResponse : ChallongeResponse
    {
        [PublicAPI]
        [JsonProperty("errors")]
        public IEnumerable<string> Errors { get; set; }

        [PublicAPI]
        public static ErrorResponse GetEmptyError(HttpStatusCode statusCode)
        {
            return new ErrorResponse
            {
                StatusCode = statusCode,
                Errors = Array.Empty<string>()
            };
        }
    }
}
