using api.Application.interfaces;
using api.Domain.Entities.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationFilter : IActionFilter
    {
        private readonly IDataCacheService _dataCacheService; 

        public ValidationFilter(IDataCacheService cacheService) { 
            _dataCacheService = cacheService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<APIErrorDetails> list = _dataCacheService.lstAPIErrorDetails;
                bool isServerError = false; 
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(error => {
                        var e = list.Where(p => error.ErrorMessage.ToLower().Contains(p.ErrorDescription.ToLower())).FirstOrDefault();
                        APIErrorDetails details = new APIErrorDetails();
                        if(e != null)
                        {
                            details.ErrorDescription = e.ErrorDescription;
                            details.ErrorCode = e.ErrorCode;
                        }
                        else
                        {
                            details.ErrorDescription = "Server Error";
                            isServerError = true;
                        }
                        return details;
                    }).ToList();
                ErrorDetails error = new ErrorDetails();
                error.StatusCode = isServerError ? 500 : 400;
                var uniqueErrors = errors.DistinctBy(e => e.ErrorCode).ToList();

                error.errorDetails = uniqueErrors;
                var errorResponse = error;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            if (!context.ModelState.IsValid)
            {
                List<APIErrorDetails> list = _dataCacheService.lstAPIErrorDetails;
                bool isServerError = false;
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(error => {
                        var e = list.Where(p => error.ErrorMessage.ToLower().Contains(p.ErrorDescription.ToLower())).FirstOrDefault();
                        APIErrorDetails details = new APIErrorDetails();
                        if (e != null)
                        {
                            details.ErrorDescription = e.ErrorDescription;
                            details.ErrorCode = e.ErrorCode;
                        }
                        else
                        {
                            isServerError = true;
                            details.ErrorDescription = "server Error";
                        }
                        return details;
                    })
                    .ToList();
                ErrorDetails error = new ErrorDetails();
                error.StatusCode = isServerError ? 500 : 400;
                var uniqueErrors = errors.DistinctBy(e => e.ErrorCode).ToList();
                error.errorDetails = uniqueErrors;
                var errorResponse = error;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }

}
