using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        [JwtAuthorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok(new { title = "Admin张三" });
        }

        [JwtAuthorize(Roles = "App")]
        [HttpGet("app")]
        public IActionResult App()
        {
            return Ok(new { title = "APP李四" });
        }

        [JwtAuthorize(Roles = "Admin,App")]
        [HttpGet("all")]
        public IActionResult AdminApp()
        {
            return Ok(new { title = "Admin张三-----APP李四" });
        }
    }
}