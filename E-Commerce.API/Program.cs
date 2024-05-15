using E_Commerce.API.Errors;
using E_Commerce.API.Extensions;
using E_Commerce.API.Helper;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.DataSeeding;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			#region Data Seeding

			// make new folder in the Repo project , then add in it the data thet will be used to test the api , testing data
			// will be seeded ... 

			#endregion

			#region Specification Design pattern

			// insted of useing lazy loading as we did in MVC project , for loading the navigational properties with the 
			// object it seldf ... , we now will use a better way and also a more advanced way !
			
			// build the query dinamically 

			


			#endregion

			#region Services

			var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.


            // Don't forget to add the project reference in the Api project (references Repository project) 
            builder.Services.AddSwaggerService();

            builder.Services.AddAplicationServices(builder.Configuration);
			builder.Services.AddIdentityService(builder.Configuration);

			#endregion

			var app = builder.Build();
			await DbInitializar.InitializeDBAsync(app);
			#region Pipelines / Middlewares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();
			app.UseMiddleware<CustomExeptionHandler>();
			app.Run();

			#endregion
		}

	}
}
