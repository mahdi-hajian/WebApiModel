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
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace WebApiModel.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [ApiVersion(version: "1")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v1/[controller]
    public class TestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _CategoryRepository;
        public TestController(IRepository<Category> CategoryRepository, IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        ////[Route("salam")]
        public async Task<ApiResult<IList<CategoryDto>>> WeatherForecastAsync()
        {
            var cat = await _CategoryRepository.TableNoTracking.Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromSeconds(30)).Include(c=>c.ChildCategories).Include(c=>c.ParentCategory).ToListAsync();
            var destObject = cat.Adapt<List<CategoryDto>>();

            var categories = await _CategoryRepository.TableNoTracking.Cacheable().ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();

            return destObject;
        }
    }
}
