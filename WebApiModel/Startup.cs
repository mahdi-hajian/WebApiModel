using Common;
using Entities.PostFolder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Configuration;
using WebFramework.Configuration.Caching_configuraion_Extention;
using WebFramework.CustomMapping;
using WebFramework.Middleware;

namespace WebApplication2
{
    // common
    // entities
    // data
    // services
    // web framework
    // my api
    public class Startup
    {
        private readonly SiteSettings _siteSetting;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // با این متد میشود این تنظیمات را داخل کانسترکتور ها دریافت کرد
            // مثال در کانسترکتور JWTSwrvice.cs
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<Category>();

            services.AddCorsExtention();

            services.AddCachingServiceExtention();

            // با این متد میشود این تنظیمات را داخل کانسترکتور ها دریافت کرد
            // مثال در کانسترکتور JWTSwrvice.cs
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);

            services.AddCustomApiVersioning();

            // ترتیب بین این دو مورد پایین مهم است
            services.AddCustomIdentity(_siteSetting.IdentitySettings);
            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddControllers();

            // برای استفاده از اتوفک
            // ابتدا خروجی این متد را از ووید به آی سرویس پروایدر تغییر میدهید و عملیات های زیر را انجام میدهیم
            return services.BuilderServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();

            app.UseCustomHsts(env);

            app.UseCors("SiteCorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
