using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Domain.Entities.Error
{
    /// <summary>
    /// APIErrorDetails.
    /// </summary>
    public class APIErrorDetails
    {
        public int ErrorId { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorCode { get; set; }
    }
}
