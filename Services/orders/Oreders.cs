using MyFristProject.Data;
using MyFristProject.Dots;
using MyFristProject.Entity;
using MyFristProject.services;
using Microsoft.EntityFrameworkCore;

namespace MyFristProject.Services
{
    public class OrderServeces (ProdectServices prodectmetods,Servicesuser user, AppDbContext context): IOrders
    {
        public async Task<ResponsorderDto> AddOrder(int userid ,OrderDto orders)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userid);
            if (user is null)
            {
                return null!;
            }
            var prodect = await prodectmetods.GetByIdProdects(orders.prodectid);
            if (prodect is null)
            {
                return null!;
            }
            var order = new Orders
            {
                UserId = user.Id,
                ProdectId = prodect.Id,
            };
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return new ResponsorderDto
            {
                UserId = user.Id,
                User = new UserDot
                {
                    Username = user.Username,
                    Password = string.Empty
                },
                Prodect = new ProdectDto
                {
                    Name = prodect.Name,
                    Price = prodect.Price
                },
                time = order.time
            };
        }

        public Task<string?> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponsorderDto> GetByIdOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResponsorderDto>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<ResponsorderDto?> PutOrder(OrderDto? orders, int? id)
        {
            throw new NotImplementedException();
        }
    }
}