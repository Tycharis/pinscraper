using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ChallongeApi
{
    public class ErrorResponse : ChallongeResponse
    {
        [JsonProperty("errors")]
        public IEnumerable<string> Errors { get; set; }

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
