using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            return await createrespons(user);
        }

        private async Task<Responstoken?> createrespons(User? requst)
        {
            return new Responstoken
            {
                Token= createaccesstoken(requst!),
                RefreshToken =await createrefreshtoken(requst!)
            };
        }
        private string Ginratrefreshtoken()
        {
            var rendem = new byte[32];
            using var rag =  RandomNumberGenerator.Create();
            rag.GetBytes(rendem);
            return Convert.ToBase64String(rendem);
        }
        private async Task<string> createrefreshtoken(User user)
        {
            var refreshtoken = Ginratrefreshtoken();
            user.RefreshToken=refreshtoken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await Context.SaveChangesAsync();
            return refreshtoken;
        }

        private string createaccesstoken(User user)
        {
            var cliams = new List<Claim>
            {
              new Claim(ClaimTypes.Name,user.Username?? string.Empty),  
              new Claim(ClaimTypes.Role,user.role?? string.Empty),
              new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())  
            };
            var key= new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:Token")!)
            );
            var cerd = new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var accesstoken = new JwtSecurityToken(
                issuer:Configuration.GetValue<string>("AppSettings:Issuer"),
                audience:Configuration.GetValue<string>("AppSettings:Audience"),
                claims:cliams,
                expires:DateTime.UtcNow.AddDays(1),
                signingCredentials:cerd
            );
            return new JwtSecurityTokenHandler().WriteToken(accesstoken);
        }
        private async Task<User?> Validate(int id, string refresh)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == id && u.RefreshToken == refresh);
            if (user is null)
            {
                return null;
            }

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        public async Task<Responstoken?> refreshToken(RefreshTokenDot requst)
        {
            var user = await Validate(requst.Id, requst.RefreshToken);
            if (user is null)
            {
                return null;
            }
            return await createrespons(user);
        }
    }
}