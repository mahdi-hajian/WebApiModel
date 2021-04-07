using Data.Contracts;
using Entities;
using Entities.IdntityUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Autorizes;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiModel.DTO.PostFolder;
using WebFramework.Api;

namespace WebApiModel.Controllers.v2
{
    [ApiVersion("2")]
    public class UsersController : v1.UsersController
    {
        public UsersController(IUserRepository userRepository,
            ILogger<v1.UsersController> logger,
            IJWTService jwtService,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
            : base(userRepository, logger, jwtService, userManager, roleManager, signInManager)
        {
        }

        [HttpPost("SetProfile")]
        public IFormFile Set_Profile(IFormFile file1)
        {
            //IFormFileCollection file = Request.Form.Files;
            return file1;
        }
 
        public override Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            return base.Create(userDto, cancellationToken);
        }

        public override Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }

        public override Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }

        public override Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        public override Task<ActionResult> Token([FromForm] TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            return base.Token(tokenRequest, cancellationToken);
        }

        public override Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            return base.Update(id, user, cancellationToken);
        }
    }
}
