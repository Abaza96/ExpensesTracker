using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Items.Any() && context.Expenses.Any())
            {
                return;
            }
            List<Item> ItemsInitializer = new()
            {
                new Item { Borrower = "Peter Parker", Lender = "Miles Morales", ItemName = "Web Shooter", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Dante Sparda", Lender = "Lady Mary", ItemName = "Cash", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Roushdy Helal", Lender = "Boshkash Mahfouz", ItemName = "Luck Pandant", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Damian Wayne", Lender = "Bruce Wayne", ItemName = "Batgrappler", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Matt Murdock", Lender = "Karen Page", ItemName = "Stick", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Jotaru", Lender = "Dio", ItemName = "Stando", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Jessica Jones", Lender = "Trish Walker", ItemName = "Makeup", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Sam Wilson", Lender = "Steve Rogers", ItemName = "Shield", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Anna Grimstottir", Lender = "Sam Fisher", ItemName = "Gun", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Goku", Lender = "Vegeta", ItemName = "Saiyan Armor", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now },
                new Item { Borrower = "Yugi Motto", Lender = "Seto Kaiba", ItemName = "Blue-Eyed White Dragon", UpdatedAt = DateTime.Now, CreatedAt = DateTime.Now }
            };
            List<Expense> ExpensesInitializer = new()
            {
                new Expense { Name = "PlayStation", Cost = 13666, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Freebuds", Cost = 1500, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Shoes", Cost = 360, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Sweat Shirt", Cost = 1999, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Medicine", Cost = 780, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Laptop", Cost = 27800, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Watch", Cost = 1700, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Books", Cost = 900, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Phone", Cost = 27000, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Home Appliances", Cost = 45780, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Expense { Name = "Sport Equipments", Cost = 3150, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            };

            ItemsInitializer.ForEach(q => context.Items.Add(q));
            context.SaveChanges();
            ExpensesInitializer.ForEach(q => context.Expenses.Add(q));
            context.SaveChanges();
        }
    }
}
