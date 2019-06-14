using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ChallongeApi
{
    public class ErrorResponse : ChallongeResponse
    {
        public static ErrorResponse GetEmptyError(HttpStatusCode statusCode)
        {
            return new ErrorResponse
            {
                StatusCode = statusCode,
                Errors = Array.Empty<string>()
            };
        }

        [JsonProperty("errors")]
        public IEnumerable<string> Errors { get; set; }
    }
}
