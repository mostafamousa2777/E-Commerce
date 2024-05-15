using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public static class IdentityDataContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager) {
            if (!userManager.Users.Any()) {
                var user = new ApplicationUser {
                
                UserName="mostafamousa",
                Email= "mostafamousa287@gmail.com",
                DisplayName= "mostafa mousa 99",
                Address=new Address {
                City="sirs",
                State="ayhaga",
                Country="Egypt",
                Street="5",
                PostalCode="10",

                
                
                }

                };
                await userManager.CreateAsync(user,"P@ssw0rd12345");

            }
        
        }
    }
}
