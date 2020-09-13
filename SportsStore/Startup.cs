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

        // ����������� ��� ��������� ����������� ��������, ������� ����� �������������� ����������� � ���������� ����� DI
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                this.Configuration["Data:SportsStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }

        // ������������ ��� ��������� �������, ������� �������� � ������������ HTTP-������� 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ���������� ������ ����������, ������� ��������� � �������� ������
            app.UseDeveloperExceptionPage();
            // ��������� ������� ��������� � HTTP-������, ������� ����� �� ����� �� ���� (��������, 404 - Not Found)
            app.UseStatusCodePages();
            // �������� ��������� ��� ������������ ������������ ����������� �� ����� wwwroot
            app.UseStaticFiles();
            app.UseSession();
            // �������� �������������� MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { Controller = "Product", Action = "List" }
                );
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { Controller = "Product", Action = "List", ProductPage = 1 }
                );
                routes.MapRoute(
                    name: "null",
                    template: "",
                    defaults: new { Controller = "Product", Action = "List", ProductPage = 1 }
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
