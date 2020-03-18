using PlattSampleApp.Models.SwApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PlattSampleApp.Common;
using PlattSampleApp.Models;

namespace PlattSampleApp.ApiServices.StarWars
{
    public class SwStarshipService : ISwStarshipService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SwStarshipService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Starship> GetStarshipByEndpoint(string endpoint)
        {
            var client = _httpClientFactory.CreateClient("swApiClient");
            // the length of the base Uri is 16
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint.Substring(17));

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return JsonStrategy.ReadJsonFromStream<Starship>(responseStream);
                }
            }
            else
            {
                throw new RESTException(response.ReasonPhrase, response.StatusCode, null);
            }
        }
    }
}
