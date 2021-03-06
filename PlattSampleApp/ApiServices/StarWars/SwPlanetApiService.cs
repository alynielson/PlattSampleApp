﻿using PlattSampleApp.Models.SwApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PlattSampleApp.Common;
using PlattSampleApp.Models;

namespace PlattSampleApp.ApiServices.StarWars
{
    public class SwPlanetApiService : ISwPlanetApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SwPlanetApiService(IHttpClientFactory httpClientFactory)
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
    }
}
