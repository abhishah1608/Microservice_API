
using api.Domain.Entities.Error;

namespace API.Extensions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public List<APIErrorDetails> errorDetails { get; set; }

        public override string ToString()
        {
            return JsonHelper.ObjectToJson(this);
        }
    }
}
