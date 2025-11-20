using _8_prac_JWT.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _8_prac_JWT.UniversalMethod
{
    public class JwtGenerator
    {
        private readonly string _secretKey;
        public JwtGenerator(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"] ?? throw new Exception("NOT JWT!!!!");
        }

        public string Generatetoken(LoginPassword user)
        {
            var claims = new[]
            {
                new Claim("UserId", user.Id_user.ToString()),
                new Claim("RoleId", user.Role_id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
