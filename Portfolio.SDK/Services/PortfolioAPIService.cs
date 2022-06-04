using CommonLib.Interfaces;
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

namespace PortfolioAPI.SDK.Services
{
    public class PortfolioAPIService : IPortfolioService
    {
        public HttpClient _client { get; }

        public PortfolioAPIService(HttpClient client, IOptions<PortfolioAPIOptions> options)
        {
            try
            {
                client.BaseAddress = new Uri(options.Value.BaseAddress);

                client.DefaultRequestHeaders.Add("Accept", "text/plain");
            }
            catch (OptionsValidationException)
            {

            }

            _client = client;
        }

        public Task<PortfolioDTO> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PortfolioDTO>> GetAll()
        {
            List<PortfolioDTO> returnValue = new List<PortfolioDTO>();

            HttpResponseMessage response = await _client.GetAsync(
                "/Produce/GetAll");

            response.EnsureSuccessStatusCode();

            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                await JsonSerializer.DeserializeAsync
                  <IEnumerable<PortfolioDTO>>(responseStream);
            }

            return returnValue;
        }

        public Task<Guid> Save(PortfolioDTO portfolio)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<ProduceDTO>> GetAll()
        //{

        //}

        //public async Task<ProduceDTO> Get(int Id)
        //{
        //    HttpResponseMessage response = await _client.GetAsync(
        //        $"/Produce/{Id}");

        //    response.EnsureSuccessStatusCode();

        //    using Stream responseStream = await response.Content.ReadAsStreamAsync();

        //    return await JsonSerializer.DeserializeAsync
        //        <ProduceDTO>(responseStream);
        //}

        //public async Task<ProduceDTO> Save(ProduceDTO produce)
        //{
        //    StringContent produceJson = new StringContent(
        //       JsonSerializer.Serialize(produce),
        //       Encoding.UTF8,
        //       "application/json");

        //    HttpResponseMessage response =
        //        await _client.PostAsync($"/Produce", produceJson);

        //    response.EnsureSuccessStatusCode();

        //    using Stream responseStream = await response.Content.ReadAsStreamAsync();

        //    return await JsonSerializer.DeserializeAsync
        //        <ProduceDTO>(responseStream);
        //}

        //public async Task<int> Delete(int id)
        //{
        //    HttpResponseMessage response =
        //        await _client.DeleteAsync($"/Produce/{id}");

        //    response.EnsureSuccessStatusCode();

        //    using Stream responseStream = await response.Content.ReadAsStreamAsync();

        //    return await JsonSerializer.DeserializeAsync
        //        <int>(responseStream);
        //}
    }
}