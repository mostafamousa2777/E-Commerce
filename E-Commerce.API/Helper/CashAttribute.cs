using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;
using System.Text;

namespace E_Commerce.API.Helper
{
    public class CashAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _time;

        public CashAttribute(int time)
        {
            _time = time;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashkey = GenertateKeyFromRequest(context.HttpContext.Request);
            var _cashservice=context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            var cashresponse=await _cashservice.GetCashResponseAsync(cashkey);
            if (cashresponse != null) {
                var result = new ContentResult
                {
                    ContentType = "application/json",
                    StatusCode = 200,
                    Content = cashresponse

                };
                context.Result= result;
                 return;
            
            }
           var excutedcontext= await next();
            if (excutedcontext.Result is OkObjectResult response) {
                await _cashservice.SetCashResponseAsync(cashkey, response.Value, TimeSpan.FromSeconds(_time));
            
            }

        }
        private string GenertateKeyFromRequest(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append($"{request.Path}");
            foreach (var item in request.Query.OrderBy(o => o.Key))
                key.Append(item);
            return key.ToString();
        }
    }
}
