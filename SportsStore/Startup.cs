using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Применяется для настройки разделяемых объектов, которые могут использоваться повсеместно в приложении через DI
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                this.Configuration["Data:SportsStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // Используется для настройки средств, которые получают и обрабатывают HTTP-запросы 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Отображает детали исключения, которые произошли в процессе работы
            app.UseDeveloperExceptionPage();
            // Добавляет простые сообщения в HTTP-ответы, которые иначе не имели бы тела (например, 404 - Not Found)
            app.UseStatusCodePages();
            // Включает поддержку для обслуживания статического содержимого из папки wwwroot
            app.UseStaticFiles();
            // Включает инфраструктуру MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "pagination",
                    template: "Products/Page{productPage}",
                    defaults: new { Controller = "Product", action = "List" }
                );
                routes.MapRoute(
                    "default",
                    "{controller=Product}/{action=List}/{id?}"
                );
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
