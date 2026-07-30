using MyFristProject.Dots;
using MyFristProject.Entity;

namespace MyFristProject.services
{
    public interface Iservices
    {
    Task<User> Register(UserDot requst);
    Task<Responstoken?> login(UserDot requst);
    Task<Responstoken?> refreshToken(RefreshTokenDot requst);
    }
}