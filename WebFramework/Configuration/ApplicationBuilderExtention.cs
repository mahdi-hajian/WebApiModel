using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebFramework.Configuration
{
    public static class ApplicationBuilderExtention
    {
        public static void UseCustomHsts(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                // دوباره اکسپشن صادر میکند که بصورت جیسون هم دریافت شود
                //app.UseExceptionHandler();
                app.UseHsts();
            }
        }
    }
}
