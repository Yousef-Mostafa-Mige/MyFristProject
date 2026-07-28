using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFristProject.Data;
using MyFristProject.Dots;
using MyFristProject.Entity;
namespace MyFristProject.services
{
    public class Services(AppDbContext Context, IConfiguration Configuration) : Iservices
    {

        public async Task<User> Register(UserDot requst)
        {
            if (requst is null)
            {
                return null!;
            }

            if (await Context.Users.AnyAsync(u => u.Username == requst.Username))
            {
                return null!;                
            }
            var user = new User
            {
                Username = requst.Username
            };
            var hash = new PasswordHasher<User>().HashPassword(user, requst.Password);
            user.PasswordHash = hash;
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
            return user!;
        }

        public async Task<Responstoken?> login(UserDot requst)
        {
            if (requst is null)
            {
                return null!;
            }

            var user = await Context.Users.FirstOrDefaultAsync(u => u.Username == requst.Username);
            if (user is null)
            {
                return null!;
            }

            var verification = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, requst.Password);
            if (verification != PasswordVerificationResult.Success)
            {
                return null!;
            }

            return await createrespons(requst);
        }

        private async Task<Responstoken?> createrespons(UserDot? requst)
        {
            return new Responstoken
            {
                Token=createaccesstoken(),
                RefreshToken = createrefreshtoken()
            };
        }

        private string createrefreshtoken()
        {
            throw new NotImplementedException();
        }

        private string createaccesstoken()
        {
            throw new NotImplementedException();
        }

        public Task<Responstoken?> refreshToken(RefreshTokenDot requst)
        {
            throw new NotImplementedException();
        }
    }
}