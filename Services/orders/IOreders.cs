using MyFristProject.Dots;
using MyFristProject.Entity;
namespace MyFristProject.services
{
    public interface IOrders
    {
        Task<ResponsorderDto> AddOrder(int userid ,OrderDto orders);
        Task<List<ResponsorderDto>> GetOrders();
        Task<ResponsorderDto> GetByIdOrder(int id);
        Task<ResponsorderDto?> PutOrder(OrderDto? orders, int? id);
        Task<string?> DeleteOrder(int id);
    }
}