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
using PortfolioAPI.SDK.DTO;

namespace PortfolioAPI.SDK.Services
{
    public class VantageAPIService
    {
        public HttpClient _client { get; }

        public VantageAPIService(HttpClient client, IOptionsMonitor<PortfolioAPIOptions> options)
        {
            try
            {
                client.BaseAddress = new Uri(options.Get("PortfolioAPI").BaseAddress);

                client.DefaultRequestHeaders.Add("Accept", "text/plain");
            }
            catch (OptionsValidationException)
            {

            }

            _client = client;
        }

        public async Task<VantageDailyDTO> GetDaily(string ticker)
        {
            VantageDailyDTO returnValue = new VantageDailyDTO();

            HttpResponseMessage response = await _client.GetAsync(
                $"/Vantage/{ticker}/Daily");

            response.EnsureSuccessStatusCode();

            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                if (responseStream.Length > 0)
                {
                    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

                    VantageDailyDTO result = await JsonSerializer.DeserializeAsync
                      <VantageDailyDTO>(responseStream, options);

                    if (result != null)
                    {
                        returnValue = result;
                    }
                }
            }

            return returnValue;
        }
    }
}