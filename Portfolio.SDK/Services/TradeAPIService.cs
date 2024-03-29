﻿using System;
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
using PortfolioAPI.SDK.Interfaces;

namespace PortfolioAPI.SDK.Services
{
    public class TradeAPIService : ITradeService
    {
        public HttpClient _client { get; }

        public TradeAPIService(HttpClient client, IOptionsMonitor<PortfolioAPIOptions> options)
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

        public async Task<IEnumerable<TradeDTO>> GetAll(Guid portfolioId)
        {
            List<TradeDTO> returnValue = new List<TradeDTO>();

            HttpResponseMessage response = await _client.GetAsync(
                $"/Trade/{portfolioId}/All");

            response.EnsureSuccessStatusCode();

            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                if (responseStream.Length > 0)
                {
                    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

                    IEnumerable<TradeDTO> result = await JsonSerializer.DeserializeAsync
                      <IEnumerable<TradeDTO>>(responseStream, options);

                    if (result != null)
                    {
                        returnValue.AddRange(result.ToList());
                    }
                }
            }

            return returnValue;
        }

        public async Task<Guid> Save(TradeDTO trade)
        {
            Guid returnValue = Guid.Empty;

            StringContent portfolioJson = new StringContent(
               JsonSerializer.Serialize(trade),
               Encoding.UTF8,
               "application/json");

            HttpResponseMessage response = await _client.PostAsync(
                "/Trade/Save", portfolioJson);

            if (response.IsSuccessStatusCode)
            {
                using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                {
                    returnValue = await JsonSerializer.DeserializeAsync
                      <Guid>(responseStream);
                }
            }

            return returnValue;
        }

        public async Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();

            //Guid returnValue = new List<TradeDTO>();

            //HttpResponseMessage response = await _client.GetAsync(
            //    $"/Trade/{portfolioId}/All");

            //response.EnsureSuccessStatusCode();
        }

    }
}