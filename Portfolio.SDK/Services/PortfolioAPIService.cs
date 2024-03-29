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
    public class PortfolioAPIService : IPortfolioService
    {
        public HttpClient _client { get; }

        public PortfolioAPIService(HttpClient client, IOptionsMonitor<PortfolioAPIOptions> options)
        {

            client.BaseAddress = new Uri(options.Get("PortfolioAPI").BaseAddress);

            client.DefaultRequestHeaders.Add("Accept", "text/plain");

            _client = client;
        }

        public async Task<PortfolioDTO> Get(Guid id)
        {
            PortfolioDTO returnValue = new PortfolioDTO();

            HttpResponseMessage response = await _client.GetAsync(
                $"/Portfolio/{id}");

            response.EnsureSuccessStatusCode();

            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                if (responseStream.Length > 0)
                {
                    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

                    PortfolioDTO result = await JsonSerializer.DeserializeAsync
                      <PortfolioDTO>(responseStream, options);

                    if (result != null)
                    {
                        returnValue = result;
                    }
                }
            }

            return returnValue;
        }

        public async Task<IEnumerable<PortfolioDTO>> GetAll()
        {
            List<PortfolioDTO> returnValue = new List<PortfolioDTO>();

            HttpResponseMessage response = await _client.GetAsync(
                "/Portfolio/All");

            response.EnsureSuccessStatusCode();

            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                if (responseStream.Length > 0)
                {
                    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

                    IEnumerable<PortfolioDTO> result = await JsonSerializer.DeserializeAsync
                      <IEnumerable<PortfolioDTO>>(responseStream, options);

                    if (result != null)
                    {
                        returnValue.AddRange(result.ToList());
                    }
                }
            }

            return returnValue;
        }

        public async Task<Guid> Save(PortfolioDTO portfolio)
        {
            Guid returnValue = Guid.Empty;

            StringContent portfolioJson = new StringContent(
               JsonSerializer.Serialize(portfolio),
               Encoding.UTF8,
               "application/json");

            HttpResponseMessage response = await _client.PostAsync(
                "/Portfolio/Save", portfolioJson);


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

        public async Task<bool> Delete(Guid id)
        {
            bool returnValue = false;

            HttpResponseMessage response = await _client.GetAsync(
               $"/Portfolio/{id}/Delete");

            response.EnsureSuccessStatusCode();

            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                if (responseStream.Length > 0)
                {
                    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

                    returnValue = await JsonSerializer.DeserializeAsync
                      <bool>(responseStream, options);
                }
            }

            return returnValue;
        }
    }
}