using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ConfigurationHub.Core.Auth
{
    public interface IJwtTokenUtils
    {
        string CreateToken(string secret, string id);
    }

    public class JwtTokenUtils : IJwtTokenUtils
    {
        private readonly Encoding _encoding;
        private readonly DateTime _expiry;

        public JwtTokenUtils()
        {
            _encoding = Encoding.ASCII;
            _expiry = DateTime.UtcNow.AddDays(7);
        }

        public JwtTokenUtils(Encoding encoding, DateTime expiry)
        {
            _encoding = encoding;
            _expiry = expiry;
        }

        public string CreateToken(string secret, string id)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = _expiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_encoding.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
