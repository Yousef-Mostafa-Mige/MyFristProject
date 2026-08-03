using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFristProject.Dots;
using MyFristProject.services;

namespace myFristProject.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrders _orders) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(int userid,int prodectid)
        {
            var usrid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var orderDto = new OrderDto
            {
                prodectid = prodectid
            };
            
            var result = await _orders.AddOrder(userid, orderDto);
            return Ok(result);
        }
    }
}