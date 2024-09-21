using api.Application.interfaces;
using api.Domain.Entities.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Application.Services
{
    public class DataCacheService : IDataCacheService
    {
        private readonly IApiService _apiService;
        public DataCacheService(IApiService service)
        {
            _apiService = service;
        }

        public List<APIErrorDetails> lstAPIErrorDetails { get; private set; }

        public async Task<List<APIErrorDetails>> LoadDataAsync()
        {
            // This method will be called to load data from the API
            // (Implementation will be done in the hosted service)
            lstAPIErrorDetails = await _apiService.GetDataAsync();
            return lstAPIErrorDetails;
        }
    }
}
