using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace E_Commerce.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            //Services.AddSwaggerGen(opt =>
            //{

            //    var Scheme=new OpenApiSecurityScheme { 

            //    Description="Standerd Authorization Header",
            //    In=ParameterLocation.Header,
            //    Name= "Authorization",
            //    Type=SecuritySchemeType.ApiKey,


            //    };
            //    opt.AddSecurityDefinition("bearer", Scheme);
            //    opt.OperationFilter<SecurityRequirementsOperationFilter>();
            //});
            Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            return Services;

        }
    }
}
