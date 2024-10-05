using api.Application.interfaces;
using api.Domain.Entities.Error;
using api.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Application.Services
{
    public class DataCacheService : IDataCacheService
    {
        private readonly IApiRepository _apiRepository;

        private readonly IConfiguration _configuration;

        public DataCacheService(IApiRepository apiRepository, IConfiguration configuration)
        {
            _apiRepository = apiRepository;
            _configuration = configuration;
        }

        public List<APIErrorDetails> lstAPIErrorDetails { get; private set; }

        public async Task<List<APIErrorDetails>> LoadDataAsync()
        {
            // This method will be called to load data from the API
            // (Implementation will be done in the hosted service)
            string url = _configuration["Externalmicroservices:DistributedService"];
            url = url + "api/ApiError/GetCache?key=APIErrorDetails";
            lstAPIErrorDetails = await _apiRepository.GetDataList<APIErrorDetails>(url, null);
            return lstAPIErrorDetails;
        }
    }
}
