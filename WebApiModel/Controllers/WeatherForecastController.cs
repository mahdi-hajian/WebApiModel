using Entities.PostFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WebFramework.Api;
using Entities.IdntityUser;
using WebFramework.Filter;

namespace WebApiModel.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [ApiVersion(version: "1")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v1/[controller]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Category _category1;

        public WeatherForecastController(Category category1)
        {
            _category1 = category1;
        }

        [HttpGet]
        ////[Route("salam")]
        public ApiResult<Category> WeatherForecast()
        {
            _category1.Name = "asdfa";
            return _category1;
        }
    }
}
