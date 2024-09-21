using api.Domain.Entities.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Application.interfaces
{
    public interface IApiService
    {
        Task<List<APIErrorDetails>> GetDataAsync();
    }
}
