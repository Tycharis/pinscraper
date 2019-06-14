using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChallongeApi
{
    public interface IChallongeResponse
    {
        HttpStatusCode StatusCode { get; set; }
    }

    public abstract class ChallongeResponse : IChallongeResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public static async Task<T> FromHttpResponseMessageAsync<T>(HttpResponseMessage responseMessage)
            where T : ChallongeResponse
        {
            string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var challongeResponse = JsonConvert.DeserializeObject<T>(
                jsonResponse,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            challongeResponse.StatusCode = responseMessage.StatusCode;

            return challongeResponse;
        }
    }
}
