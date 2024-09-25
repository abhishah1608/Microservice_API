using api.Domain.Entities.Error;
using api.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace api.Infrastructure.Repository
{
    public class ApiRepository : IApiRepository
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public ApiRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<APIErrorDetails>> GetAPIErrorListAsync()
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
