using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuildMaterials.Data;
using BuildMaterials.Infrastructure.Data.Domain;

namespace BuildMaterials.Infrastructure.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);
            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories (dataCategory);
            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrand(dataBrand);

            return app;
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }

            dataCategory.Categories.AddRange(new[]
            {
            new Category {CategoryName="Cement"},
            new Category {CategoryName="Latex"},
            new Category {CategoryName="Plaster"},
            new Category {CategoryName="Parquet"},
            new Category {CategoryName="Tiles"},
            new Category {CategoryName="Concrete"},
            new Category {CategoryName="Filts"}
            });

            dataCategory.SaveChanges();
        }

        private static void SeedBrand(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }

            dataBrand.Brands.AddRange(new[]
            {
            new Brand {BrandName="Holcim"},
            new Brand {BrandName="Unipro"},
            new Brand {BrandName="Adiplast"},
            new Brand {BrandName="Ceresit"},
            new Brand {BrandName="Adiflex"},
            new Brand {BrandName="Mapei Lampocem"},
            new Brand {BrandName="Anrifriz"},
            new Brand {BrandName="Sika"}
            });

            dataBrand.SaveChanges();
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "Admin";
                user.LastName = "Adminov";
                user.UserName = "ADMIN";
                user.Email = "admin@adminski.com";
                user.Address = "admin address";
                user.PhoneNumber = "0888888888";

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

    }
}
