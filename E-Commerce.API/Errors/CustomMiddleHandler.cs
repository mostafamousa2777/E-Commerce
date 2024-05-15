using System.Net;
using System.Text.Json;

namespace E_Commerce.API.Errors
{
    public class CustomExeptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExeptionHandler> _logger;
        private readonly IHostEnvironment environment;

        public CustomExeptionHandler(RequestDelegate next, ILogger<CustomExeptionHandler> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            this.environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try { 
            
            await _next.Invoke(context);
            
            }
            catch(Exception e) {

                _logger.LogError(e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //context.Response.ContentType = "application/json";
                var response =environment.IsDevelopment()? new ApiExeptionResponse((int)HttpStatusCode.InternalServerError,e.Message,e.StackTrace)
                    : new ApiExeptionResponse((int)HttpStatusCode.InternalServerError);
              //  var Json=JsonSerializer.Serialize(response,
               //     new JsonSerializerOptions {PropertyNamingPolicy=JsonNamingPolicy.CamelCase });

                await context.Response.WriteAsJsonAsync(response);

            
            
            }
                
        }
    }
}
