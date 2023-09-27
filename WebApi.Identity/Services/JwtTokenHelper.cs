using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Shared.Utilities.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Identity.Models;

namespace WebApi.Identity.Services;

public static class JwtTokenHelper
{
    public static string CreateToken(User user)
    {
        IConfiguration _configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
        var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();

        JwtSecurityTokenHandler tokenHandler = new();


        var key = Encoding.ASCII.GetBytes(tokenOptions.SecurityKey);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = tokenOptions.Issuer,
            Audience = tokenOptions.Audience,
            Subject = GetClaimsIdentity(user),
            Expires = DateTime.Now.AddDays(tokenOptions.AccessTokenExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });


        return tokenHandler.WriteToken(token);
    }


    private static ClaimsIdentity GetClaimsIdentity(User user)
    {

        var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Name, user.FirstName),
                new(JwtRegisteredClaimNames.GivenName, user.LastName),
                new(JwtRegisteredClaimNames.Email, user.Email),
            };

        return new ClaimsIdentity(claims);
    }

}
