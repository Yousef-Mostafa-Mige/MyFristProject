using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using MyFristProject.Dots;
using MyFristProject.Entity;
using MyFristProject.services;

namespace MyFristProject.controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class Authcontroller (Iservices server): ControllerBase

    {
        [HttpPost("regster")]
        public async Task<IActionResult> regster(UserDot user)
        {
            // No external Server.Register available; return the provided user as a placeholder result.
            var regsterfk = await server.Register(user);
            if(regsterfk is null)
            {
                return BadRequest("the user arady found");
            }
            return Ok(regsterfk);
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(UserDot user)
        {
            // No external Server.Register available; return the provided user as a placeholder result.
            var regsterfk = await server.login(user);
            if(regsterfk is null)
            {
                return BadRequest("not found user or password error");
            }
            return Ok(regsterfk);
        }
        [HttpPost("refreshroken")]
        public async Task<IActionResult> refreshroken(RefreshTokenDot requst)
        {
            // No external Server.Register available; return the provided user as a placeholder result.
            var regsterfk = await server.refreshToken(requst);
            if(regsterfk is null)
            {
                return BadRequest("the user arady found");
            }
            return Ok(regsterfk);
        }
        [Authorize]
        [HttpGet("test")]
        public IActionResult test()
        {
            var username = User.Identity?.Name;
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok($"you are vrey good {username} id : {id}");
        }
    }
}