using PlattSampleApp.Common;
using PlattSampleApp.Models;
using PlattSampleApp.Models.SwApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattSampleApp.ApiServices.StarWars
{
    public class SwVehicleApiService : ISwVehicleApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SwVehicleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<PagedResult<Vehicle>> GetVehiclesPage(HttpClient httpClient, string nextPageEndpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, nextPageEndpoint);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return JsonStrategy.ReadJsonFromStream<PagedResult<Vehicle>>(responseStream);
                }
            }
            else
            {
                throw new RESTException(response.ReasonPhrase, response.StatusCode, null);
            }
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            var client = _httpClientFactory.CreateClient("swApiClient");
            var endpoint = "/api/vehicles";
            var allVehicles = new List<Vehicle>();
            PagedResult<Vehicle> pagedResult = null;
            do
            {
                pagedResult = await GetVehiclesPage(client, endpoint);
                if (pagedResult?.Results is null || pagedResult.Results.Count == 0)
                {
                    break;
                }
                allVehicles.AddRange(pagedResult.Results);
                endpoint = "/api/vehicles" + pagedResult.Next?.Substring(pagedResult.Next.LastIndexOf('/'));
            } while (pagedResult.Next != null);

            return allVehicles;
        }
    }
}
