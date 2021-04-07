using Autofac;
using Common;
using Data;
using Entities.PostFolder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Configuration;
using WebFramework.Configuration.Caching_configuraion_Extention;
using WebFramework.CustomMapping;
using WebFramework.Middleware;
using WebFramework.Swagger;

namespace WebApiModel
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
            services.AddCorsExtention();
            services.AddCachingServiceExtention();
            // با این متد میشود این تنظیمات را داخل کانسترکتور ها دریافت کرد
            // مثال در کانسترکتور JWTSwrvice.cs
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.InitializeAutoMapper();
            services.AddDbContext(Configuration);
            services.AddCustomApiVersioning();
            // ترتیب بین این دو مورد پایین مهم است
            services.AddCustomIdentity(_siteSetting.IdentitySettings);
            services.AddJwtAuthentication(_siteSetting.JwtSettings);
            services.AddControllers();
            services.AddSwagger();

            // ConfigureContainer را میخاند
            services.AddOptions();
        }

        public void ConfigureContainer (ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacConfiguraion());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();

            app.UseCustomHsts(env);

            app.UseCors("SiteCorsPolicy");

            app.UseHttpsRedirection();

            app.UseSwaggerAndUI();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
