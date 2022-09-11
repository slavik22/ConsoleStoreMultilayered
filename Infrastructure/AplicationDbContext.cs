using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure
{
    /// <summary>
    /// AplicationDbContext
    /// </summary>
    public static class AplicationDbContext
    {
        /// <summary>
        /// DbSet of users
        /// </summary>
        static public List<User> Users { get; set; } = new List<User>()
        {
            new User {Email = "1@1", Password = "1", IsAdmin = true },
            new User {Email = "email0@test.com", Password = "pass0" },
            new User {Email = "email1@test.com", Password = "pass1" },
            new User {Email = "email2@test.com", Password = "pass2" }
        };
        /// <summary>
        /// DbSet of products
        /// </summary>
        static public List<Product> Products { get; set; } = new List<Product>()
        {
            new Product {Name = "prod0", Price = 0.99M },
            new Product {Name = "prod1", Price = 1.99M },
            new Product {Name = "prod2", Price = 2.99M }
        };
        /// <summary>
        /// DbSet of orders
        /// </summary>
        static public List<Order> Orders { get; set; } = new List<Order>()
        {
             new Order {UserId = 0,ProductId = 0,Quantity = 1},
             new Order {UserId = 0,ProductId =1,Quantity = 2},
             new Order {UserId = 1,ProductId =2,Quantity = 1}
        };
    }
}
