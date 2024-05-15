﻿using E_Commerce.Core.Entities.Identity;
using E_Commerce.Repository.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration configuration) {

            services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<IdentityDataContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(opt => {

                    //   opt.IncludeErrorDetails = true;
                    opt.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateIssuerSigningKey= true,
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidateAudience = true,
                        ValidAudience= configuration["Token:Audience"],
                        ValidateLifetime = true,
                       


                    };


                });
            return services;
        
        }
    }
}
