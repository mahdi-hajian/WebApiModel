using CacheManager.Core;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Configuration.Caching_configuraion_Extention
{
    public static class CacheService
    {
        public static void AddCachingServiceExtention(this IServiceCollection services)
        {
            const string providerName1 = "InMemory1";
            services.AddEFSecondLevelCache(options =>
                    options.UseEasyCachingCoreProvider(providerName1, isHybridCache: false).DisableLogging(true)
            );

            services.AddEasyCaching(options =>
            {
                // use memory cache with your own configuration
                options.asdasdcasdc(config =>
                {
                    config.DBConfig = new InMemoryCachingOptions
                    {
                        // scan time, default value is 60s
                        ExpirationScanFrequency = 60,
                        // total count of cache items, default value is 10000
                        SizeLimit = 100,

                        // enable deep clone when reading object from cache or not, default value is true.
                        EnableReadDeepClone = false,
                        // enable deep clone when writing object to cache or not, default value is false.
                        EnableWriteDeepClone = false,
                    };
                    // the max random second will be added to cache's expiration, default value is 120
                    config.MaxRdSecond = 120;
                    // whether enable logging, default is false
                    config.EnableLogging = false;
                    // mutex key's alive time(ms), default is 5000
                    config.LockMs = 5000;
                    // when mutex key alive, it will sleep some time, default is 300
                    config.SleepMs = 300;
                }, providerName1);
            });
        }
    }
}
