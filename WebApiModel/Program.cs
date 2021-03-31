using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //    //Set deafult proxy
            //    //WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };

            //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            //    try
            //    {
            //        CreateWebHostBuilder(args).Build().Run();
            //    }
            //    catch (Exception ex)
            //    {
            //        //NLog: catch setup errors
            //        logger.Error(ex, "Stopped program because of exception");
            //        throw;
            //    }
            //    finally
            //    {
            //        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            //        NLog.LogManager.Shutdown();
            //    }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            // اتوفک را اضافه میکند
            // AutofacServiceProviderFactory همچنین تمام تزریق های قبلی را اضافه میکند
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
