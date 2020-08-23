using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder арр)
        {
            ApplicationDbContext context = арр.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Гетры футбольные",
                        Description = "Черные",
                        Category = "Футбол",
                        Price = 700
                    },
                    new Product
                    {
                        Name = "Мяч футбольный",
                        Description = "Из натуральной кожи",
                        Category = "Футбол",
                        Price = 3000
                    },
                    new Product
                    {
                        Name = "Мяч водный",
                        Description = "Для игры на воде",
                        Category = "Водные виды спорта",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Очки плавательные 1",
                        Description = "Для любителей",
                        Category = "Водные виды спорта",
                        Price = 400
                    },
                    new Product
                    {
                        Name = "Очки плавательные 2",
                        Description = "Для профессионалов",
                        Category = "Водные виды спорта",
                        Price = 1500
                    },
                    new Product
                    {
                        Name = "Звонок велосипедный",
                        Description = "Умеренной громкости",
                        Category = "Велоспорт",
                        Price = 350
                    },
                    new Product
                    {
                        Name = "Сиденье для велосипеда",
                        Description = "Для долгих поездок",
                        Category = "Велоспорт",
                        Price = 1100
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
