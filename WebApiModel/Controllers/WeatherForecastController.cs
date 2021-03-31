using Entities.PostFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreSecondLevelCacheInterceptor;
using System.Threading.Tasks;
using Common;
using WebFramework.Api;
using Entities.IdntityUser;
using WebFramework.Filter;
using Entities.Interfaces.PostFolder;
using Data.Contracts;
using WebApiModel.DTO.PostFolder;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace WebApiModel.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [ApiVersion(version: "1")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v1/[controller]
    public class WeatherForecastController : ControllerBase
    {
        public IRepository<Category> _CategoryRepository  { get; private set; }
        public WeatherForecastController(IRepository<Category> CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        [HttpGet]
        ////[Route("salam")]
        public ApiResult<IList<CategoryDto>> WeatherForecast()
        {
            var cat = _CategoryRepository.TableNoTracking.Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromSeconds(30)).Include(c=>c.ChildCategories).Include(c=>c.ParentCategory).ToList();
            var destObject = cat.Adapt<List<CategoryDto>>();
            return destObject;
        }
    }
}
