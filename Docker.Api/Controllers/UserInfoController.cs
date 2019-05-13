using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Docker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private DbUserInfoContext _DbUserInfoContext;
        public UserInfoController(DbUserInfoContext context)
        {
            _DbUserInfoContext = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(await _DbUserInfoContext.userInfos.FirstOrDefaultAsync());
        }
    }
}