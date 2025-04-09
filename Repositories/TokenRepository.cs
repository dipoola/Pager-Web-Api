using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pager.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //create claims
            var newclaims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "user"),
            new Claim(ClaimTypes.Role, "role"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            // var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            //  configuration.GetSection("Jwt:SecretKey").Value));
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: configuration["issuer"],
               audience: configuration["audience"],
               claims: newclaims,
               expires: DateTime.UtcNow.AddDays(2),
              signingCredentials: signinCredentials);
           

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);


        }
    }
}
