using E_Commerce.Core.Entities.Identity;
using E_Commerce.Repository.Data.DataSeeding;
using E_Commerce.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extensions
{
    public static class DbInitializar
    {
        public static async Task InitializeDBAsync(WebApplication app)              // takes app (which is a WebApplication)
        {
            // Steps : 
            // Create the DB if not exists
            // Apply Seeding


            // new way for making dependancy injection 
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<DataContext>();
                    var usermanger = service.GetRequiredService<UserManager<ApplicationUser>>();

                    // Create the DB if not exists
                    if ((await context.Database.GetPendingMigrationsAsync()).Any())
                        await context.Database.MigrateAsync();

                    // Apply Seeding
                    await DataContextSeed.SeedDataAsync(context);
                    await IdentityDataContextSeed.SeedUserAsync(usermanger);

                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex);     --- Not allowed here !
                    //  How to log the error ?

                    var logger = LoggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }


        }

    }
}
