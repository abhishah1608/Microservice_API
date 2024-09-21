using api.Application.interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Application.Services
{
    public class StartupTask : IHostedService
    {
        private readonly IDataCacheService _dataCacheService;
        private readonly IApiService _apiService;

        public StartupTask(IDataCacheService dataCacheService, IApiService apiService)
        {
            _dataCacheService = dataCacheService;
            _apiService = apiService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Load data from the API when the application starts
            await _dataCacheService.LoadDataAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
