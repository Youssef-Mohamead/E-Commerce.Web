using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager, StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {

                // Create Database If it dosen't Exists && Apply To Any Pending Migrations
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                // Data Seeding
                if (!_dbContext.Set<ProductBrand>().Any())
                {
                    // Read All data From Brands Json Files
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // Convert Data "String" => C# Objects [Productbrand]
                    //Transform String To C# Objects [List<ProductBrands>]
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);

                    if (ProductBrands is not null && ProductBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }

                if (!_dbContext.Set<ProductType>().Any())
                {
                    // Read All data From Types Json Files
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // Convert Data "String" => C# Objects [ProductType]
                    //Transform String To C# Objects [List<ProductTypess>]
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);

                    if (ProductTypes is not null && ProductTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }

                if (!_dbContext.Set<Product>().Any())
                {
                    // Read All data From Products Json Files
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // Convert Data "String" => C# Objects [Productbrand]
                    //Transform String To C# Objects [List<Products>]
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);

                    if (Products is not null && Products.Any())
                        await _dbContext.Products.AddRangeAsync(Products);
                }

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    // Read All data From Products Json Files
                    var DeliveryMethodData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    // Convert Data "String" => C# Objects [Productbrand]
                    //Transform String To C# Objects [List<Products>]
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);

                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //TODO

            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "YoussefMohamed@gmail.com",
                        DisplayName = "Youssef Mohamed",
                        PhoneNumber = "01128088595",
                        UserName = "YoussefMohamed"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "OmarMohamed@gmail.com",
                        DisplayName = "Omar Mohamed",
                        PhoneNumber = "012345678",
                        UserName = "OmarMohamed"
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    _userManager.AddToRoleAsync(User01, "Admin");
                    _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }

            // await  _identityDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }


        }
    }
}
