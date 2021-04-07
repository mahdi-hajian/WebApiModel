using Entities.PostFolder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using EFCoreSecondLevelCacheInterceptor;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filter;
using Data.Contracts;
using WebApiModel.DTO.PostFolder;
using Mapster;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace WebApiModel.Controllers.v1
{
    [ApiVersion(version: "1")]
    public class TestController : BaseController
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
