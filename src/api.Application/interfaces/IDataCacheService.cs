using api.Domain.Entities.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Application.interfaces
{
    /// <summary>
    /// IDataCacheService.
    /// </summary>
    public interface IDataCacheService
    {
        List<APIErrorDetails> lstAPIErrorDetails { get; }
        Task<List<APIErrorDetails>> LoadDataAsync();
    }
}
