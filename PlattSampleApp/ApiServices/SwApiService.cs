using Newtonsoft.Json;
using PlattSampleApp.Models.SwApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using PlattSampleApp.Common;
using PlattSampleApp.Models;

namespace PlattSampleApp.ApiServices
{
    public class SwApiService : ISwApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SwApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Planet>> GetAllPlanets()
        {
            var client = _httpClientFactory.CreateClient("swApiClient");
            var endpoint = "/api/planets";
            var allPlanets = new List<Planet>();
            PagedResult<Planet> pagedResult = null;
            do
            {
                pagedResult = await GetPlanetsPage(client, endpoint);
                if (pagedResult?.Results is null || pagedResult.Results.Count == 0)
                {
                    break;
                }
                allPlanets.AddRange(pagedResult.Results);
                endpoint = "/api/planets" + pagedResult.Next?.Substring(pagedResult.Next.LastIndexOf('/'));
            } while (pagedResult.Next != null);

            return allPlanets;
        }

        public async Task<PagedResult<Planet>> SearchPlanetsByName(string name)
        {
            var client = _httpClientFactory.CreateClient("swApiClient");

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/planets/?search={name}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return JsonStrategy.ReadJsonFromStream<PagedResult<Planet>>(responseStream);
                }
            }
            else
            {
                throw new RESTException(response.ReasonPhrase, response.StatusCode, null);
            }
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

        public async Task<Planet> GetPlanet(int planetId)
        {
            var client = _httpClientFactory.CreateClient("swApiClient");

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/planets/{planetId}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return JsonStrategy.ReadJsonFromStream<Planet>(responseStream);
                }
            }
            else
            {
                throw new RESTException(response.ReasonPhrase, response.StatusCode, null);
            }
        }

        private async Task<PagedResult<Planet>> GetPlanetsPage(HttpClient httpClient, string nextPageEndpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, nextPageEndpoint);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return JsonStrategy.ReadJsonFromStream<PagedResult<Planet>>(responseStream);
                }
            }
            else
            {
                throw new RESTException(response.ReasonPhrase, response.StatusCode, null);
            }
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
