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
        

        public StartupTask(IDataCacheService dataCacheService)
        {
            _dataCacheService = dataCacheService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Load data from the API when the application starts
            await _dataCacheService.LoadDataAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
