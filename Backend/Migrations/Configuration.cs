namespace Backend.Migrations
{
    using Backend.Models.Car;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Backend.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Backend.Models.ApplicationDbContext";
        }

        protected override void Seed(Backend.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Cars.AddOrUpdate(x => x.Id,
            new CarModels()
            {
                Id = 1,
                Name = "Mitsubishi Delica",
                Description = "Mitsubishi Delica",
                Price = 100000,
                ImagePath = "Content/Images/Car/Delica.jpg",
                IsActive = true,
                IsHotPrice = true
            },
            new CarModels()
            {
                Id = 2,
                Name = "Mitsubishi Eclipse Cross",
                Description = "Mitsubishi Eclipse Cross",
                Price = 200000,
                ImagePath = "Content/Images/Car/Eclipse-Cross.jpg",
                IsActive = true,
                IsHotPrice = true
            },
            new CarModels()
            {
                Id = 3,
                Name = "Mitsubishi Mirage",
                Description = "Mitsubishi Mirage",
                Price = 300000,
                ImagePath = "Content/Images/Car/Mirage.jpg",
                IsActive = true,
                IsHotPrice = true
            }
            );
        }
    }
}
