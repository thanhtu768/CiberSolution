using Ciber.Data.Enititys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciber.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                    new Customer() { ID = 1, Name = "Thanh Tu", Address = "HaNoi", Password = "123" },
                    new Customer() { ID = 2, Name = "Tuan Tu", Address = "HaNoi", Password = "123" }
                );
            modelBuilder.Entity<Category>().HasData(
                    new Category() { ID = 1, Name = "Paper", Description = "A1,A2,A3,A4" },
                    new Category() { ID = 2, Name = "Pencil", Description = "pencil" }
                );
            modelBuilder.Entity<Product>().HasData(
                    new Product() { ID = 1, Name = "A4 Canon", CategoryID = 1, Description = "A4 Canon Description", Price = 10000, Quantity = 100 },
                    new Product() { ID = 2, Name = "A4 Casio", CategoryID = 1, Description = "A4 Casio Description", Price = 14000, Quantity = 10 },
                    new Product() { ID = 3, Name = "Pencil 1", CategoryID = 2, Description = "Pencil Description", Price = 15000, Quantity = 100 }
                );
            modelBuilder.Entity<Order>().HasData(
                    new Order() {ID = 1, CustomerID = 1, ProductID = 1, Amount = 2, OrderDate = DateTime.Now },
                    new Order() { ID = 2, CustomerID = 1, ProductID = 2, Amount = 32, OrderDate = DateTime.Now },
                    new Order() { ID = 3, CustomerID = 2, ProductID = 3, Amount = 11, OrderDate = DateTime.Now }
                );
        }
    }
}
