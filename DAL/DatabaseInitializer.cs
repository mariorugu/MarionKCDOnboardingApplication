﻿using DAL.Core;
using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            await SeedDefaultUsersAsync();
            await SeedDemoDataAsync();
        }

        private async Task SeedDefaultUsersAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "administrator";
                const string userRoleName = "user";

                await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                await CreateUserAsync("admin", "tempP@ss123", "Inbuilt Administrator", "admin@ebenmonney.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await CreateUserAsync("user", "tempP@ss123", "Inbuilt Standard User", "user@ebenmonney.com", "+1 (123) 000-0001", new string[] { userRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                _logger.LogInformation($"Generating default role: {roleName}");

                var applicationRole = new ApplicationRole(roleName, description);

                var result = await _accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Succeeded)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");
            }
        }

        private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            _logger.LogInformation($"Generating default user: {userName}");

            var applicationUser = new ApplicationUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Succeeded)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");

            return applicationUser;
        }

        private async Task SeedDemoDataAsync()
        {
            if (!await _context.Customers.AnyAsync() && !await _context.ProductCategories.AnyAsync())
            {
                _logger.LogInformation("Seeding demo data");

                var cust_1 = new Customer
                {
                    Name = "Ebenezer Monney",
                    Email = "contact@ebenmonney.com",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var cust_2 = new Customer
                {
                    Name = "Itachi Uchiha",
                    Email = "uchiha@narutoverse.com",
                    PhoneNumber = "+81123456789",
                    Address = "Some fictional Address, Street 123, Konoha",
                    City = "Konoha",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var cust_3 = new Customer
                {
                    Name = "John Doe",
                    Email = "johndoe@anonymous.com",
                    PhoneNumber = "+18585858",
                    Address = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                    Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                    City = "Lorem Ipsum",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var cust_4 = new Customer
                {
                    Name = "Jane Doe",
                    Email = "Janedoe@anonymous.com",
                    PhoneNumber = "+18585858",
                    Address = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                    Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                    City = "Lorem Ipsum",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var prodCat_1 = new ProductCategory
                {
                    Name = "None",
                    Description = "Default category. Products that have not been assigned a category",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var prod_1 = new Product
                {
                    Name = "BMW M6",
                    Description = "Yet another masterpiece from the world's best car manufacturer",
                    BuyingPrice = 109775,
                    SellingPrice = 114234,
                    UnitsInStock = 12,
                    IsActive = true,
                    ProductCategory = prodCat_1,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var prod_2 = new Product
                {
                    Name = "Nissan Patrol",
                    Description = "A true man's choice",
                    BuyingPrice = 78990,
                    SellingPrice = 86990,
                    UnitsInStock = 4,
                    IsActive = true,
                    ProductCategory = prodCat_1,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var ordr_1 = new Order
                {
                    Discount = 500,
                    Cashier = await _context.Users.OrderBy(u => u.UserName).FirstAsync(),
                    Customer = cust_1,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail {UnitPrice = prod_1.SellingPrice, Quantity=1, Product = prod_1 },
                        new OrderDetail {UnitPrice = prod_2.SellingPrice, Quantity=1, Product = prod_2 },
                    }
                };

                var ordr_2 = new Order
                {
                    Cashier = await _context.Users.OrderBy(u => u.UserName).FirstAsync(),
                    Customer = cust_2,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail {UnitPrice = prod_2.SellingPrice, Quantity=1, Product = prod_2 },
                    }
                };
                
                var kcdUser1 = new KCDUser
                {
                    Id = "41F481AF-21A4-44C7-B4AC-1CF43E7760E3",
                    FirstName = "Matthew",
                    MiddleName = "",
                    LastName = "Llyod",
                    Email = "mariorugu@lalandi.com",
                    PhoneNumber = "06051465869",
                    Password = "cGFzc3dvcmQxMjM0",
                    Country = "Netherlands",
                    Company = "Lalandi",
                    IsActive = false
                };
                
                var kcdUser2 = new KCDUser
                {
                    Id = "7DE9FBAA-52D5-4DDC-87A1-08AA8D9DF0EE",
                    FirstName = "Marjorie",
                    MiddleName = "",
                    LastName = "Yannick",
                    Email = "mariorugu@yahoo.com",
                    PhoneNumber = "06051495869",
                    Password = "cGFzc3dvcmQxMjM0",
                    Country = "South Africa",
                    Company = "Lalandi",
                    IsActive = false
                };

                var employee1 = new Employee
                {
                    Id = "41F481AF-21A4-44C7-B4AC-1CF43E7760E3",
                    FirstName = "Marion",
                    MiddleName = "Mugabirwe",
                    LastName = "Rugunda",
                    Email = "mariorugu@lalandi.com",
                    PhoneNumber = "06051465869",
                    Password = "cGFzc3dvcmQxMjM0",
                    Country = "Uganda",
                    Company = "Lalandi",
                    IsActive = true,
                    IsAdministrator = true
                };
                
                var employee2 = new Employee
                {
                    Id = "7DE9FBAA-52D5-4DDC-87A1-08AA8D9DF0EE",
                    FirstName = "Marion",
                    MiddleName = "",
                    LastName = "Rugunda",
                    Email = "mariorugu@yahoo.com",
                    PhoneNumber = "06051465869",
                    Password = "cGFzc3dvcmQxMjM0",
                    Country = "Uganda",
                    Company = "Lalandi",
                    IsActive = true,
                    IsAdministrator = true
                };

                _context.Employees.Add(employee1);
                _context.Employees.Add(employee2);
                _context.KcdUsers.Add(kcdUser1);
                _context.KcdUsers.Add(kcdUser2);
                
                
                _context.Customers.Add(cust_1);
                _context.Customers.Add(cust_2);
                _context.Customers.Add(cust_3);
                _context.Customers.Add(cust_4);

                _context.Products.Add(prod_1);
                _context.Products.Add(prod_2);

                _context.Orders.Add(ordr_1);
                _context.Orders.Add(ordr_2);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeding demo data completed");
            }
        }
    }
}
