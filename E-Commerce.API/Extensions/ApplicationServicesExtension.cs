using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services,IConfiguration configuration)
        {
                Services
                .AddDbContext<DataContext>(o => o.UseSqlServer(configuration.GetConnectionString("SQLConnection")));
            Services
                .AddDbContext<IdentityDataContext>(o => o.UseSqlServer(configuration.GetConnectionString("IdentitySQLConnection")));
            Services.AddControllers();        // Allowa the dependancy injection for the controllers
                                                      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepositoriy, BasketRepositoriy>();


            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<ICashService, CashService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<ITokenService, TokenService>();




            // builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
            Services.AddScoped<PictureUrlResolver>();
            Services.AddSingleton<IConnectionMultiplexer>(opt =>

            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
                return ConnectionMultiplexer.Connect(config);

            });
            Services.Configure<ApiBehaviorOptions>(option =>
            option.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Where(m => m.Value.Errors.Any())
                .SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage).ToList();
                return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors });
            }

            );

            return Services;
        }
    }
}
