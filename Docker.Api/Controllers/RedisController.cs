using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        [HttpPost]
        public void Post()
        {
            RedisCommon.GetRedis().StringSet("docker", "hello", TimeSpan.FromMinutes(1));
        }
        [HttpGet]
        public string Get()
        {
            var docker = RedisCommon.GetRedis().GetStringKey("docker");
            if (docker.HasValue) return docker.ToString();
            return "empty";
        }
    }
}