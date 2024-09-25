using api.Application.interfaces;
using api.Domain.Entities.Error;
using api.Domain.Interfaces;
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
        public DataCacheService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public List<APIErrorDetails> lstAPIErrorDetails { get; private set; }

        public async Task<List<APIErrorDetails>> LoadDataAsync()
        {
            // This method will be called to load data from the API
            // (Implementation will be done in the hosted service)
            lstAPIErrorDetails = await _apiRepository.GetAPIErrorListAsync();
            return lstAPIErrorDetails;
        }
    }
}
