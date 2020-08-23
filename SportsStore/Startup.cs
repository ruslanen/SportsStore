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
            services.AddMvc(option => option.EnableEndpointRouting = false);
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
            // �������� �������������� MVC
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
