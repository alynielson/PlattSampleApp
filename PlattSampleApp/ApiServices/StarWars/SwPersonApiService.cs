using PlattSampleApp.Common;
using PlattSampleApp.Models;
using PlattSampleApp.Models.SwApi;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattSampleApp.ApiServices.StarWars
{
    public class SwPersonApiService : ISwPersonApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SwPersonApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Resident> GetResidentByEndpoint(string endpoint)
        {
            var client = _httpClientFactory.CreateClient("swApiClient");
            // the length of the base Uri is 16
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint.Substring(17));

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return JsonStrategy.ReadJsonFromStream<Resident>(responseStream);
                }
            }
            else
            {
                throw new RESTException(response.ReasonPhrase, response.StatusCode, null);
            }
        }
    }
}
