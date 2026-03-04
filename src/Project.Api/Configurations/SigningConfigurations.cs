using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Project.Api.Configurations
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(IConfiguration configuration)
        {
            var secret = configuration["JwtSettings:Secret"];
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
