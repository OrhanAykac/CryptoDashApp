namespace WebApi.Identity.Models;

public class TokenOptions
{
    public string SecurityKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpiration { get; set; }
}
