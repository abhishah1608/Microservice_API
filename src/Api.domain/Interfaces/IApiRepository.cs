using api.Domain.Entities.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Domain.Interfaces
{
    public interface IApiRepository
    {
        public Task<List<APIErrorDetails>> GetAPIErrorListAsync();
    }
}
