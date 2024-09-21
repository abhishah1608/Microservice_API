using api.Application.interfaces;
using api.Domain.Entities.Error;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<APIErrorDetails>> GetDataAsync()
        {
            string url = _configuration["Externalmicroservices:DistributedService"];
            url = url + "api/ApiError/GetCache?key=APIErrorDetails";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonHelper.JsonToList<APIErrorDetails>(content);
        }


    }
}
