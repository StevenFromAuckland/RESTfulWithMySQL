using HillLabTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HillLabTest.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            //services.AddDbContext<ProductContext>(o => o.UseInMemoryDatabase("productdb"));
            var connectionString = config["MySQLConnection:ConnectionString"];
            services.AddDbContext<ProductContext>(o => o.UseMySql(connectionString));
        }

        public static void ConfigureDBContextWrapper(this IServiceCollection services)
        {
            services.AddScoped<IProductContextWrapper, ProductContextWrapper>();
        }

    }
}
