using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PortfolioAPI.SDK.Options;
using Common.Shared.DTO;
using PortfolioAPI.SDK.Enumerations;
using System.Text.Json.Serialization;

namespace PortfolioAPI.External.Services
{
    public class AlphaVantageAPIService
    {
        public HttpClient _client { get; }
        private string _serviceApiKey { get; }
        private static Dictionary<string, string> AlphaVantageSupportedFunctions => new Dictionary<string, string>()
        {
            { "daily", "TIME_SERIES_DAILY" },
            { "quote", "GLOBAL_QUOTE" },
            { "overview", "OVERVIEW" },
            { "statement", "INCOME_STATEMENT" },
        };

        public AlphaVantageAPIService(HttpClient client, IOptionsMonitor<VantageAPIOptions> options)
        {
            try
            {
                _serviceApiKey = options.Get("VantageAPI").ServiceApiKey;

                client.BaseAddress = new Uri("https://www.alphavantage.co/");
            }
            catch (OptionsValidationException)
            {

            }

            _client = client;
        }

        private string ToAlphaVantageQuery(string verb, StockFilterDTO filter)
        {
            return $"query?function={verb}&symbol={filter.Symbol}&apikey={_serviceApiKey}";
        }

        private async Task<Dictionary<string, JsonElement>> GetFromAlphaVantage(string query)
        {
            HttpResponseMessage response = await _client.GetAsync(query);

            response.EnsureSuccessStatusCode();

            Dictionary<string, JsonElement> result = new Dictionary<string, JsonElement>();
            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                result = await JsonSerializer
                    .DeserializeAsync<Dictionary<string, JsonElement>>(responseStream);
            }

            return result;
        }

        public async Task<Dictionary<string, JsonElement>> QueryAlphaVantage(VantageVerbs verbEnum, StockFilterDTO filter)
        {
            if (AlphaVantageSupportedFunctions.TryGetValue(verbEnum.ToString(), out string functionVerb))
            {
                string query = ToAlphaVantageQuery(functionVerb, filter);

                return await GetFromAlphaVantage(query);
            }

            return new Dictionary<string, JsonElement>();
        }
    }

    public class DailyData
    {
        
        public string @class { get; set; }

        [JsonPropertyName("1. open")]
        public decimal Open { get; set; }

    }
}