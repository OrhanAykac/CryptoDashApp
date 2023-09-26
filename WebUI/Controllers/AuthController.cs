using Entities.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebUI.Services;

namespace WebUI.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthApiService _authApiService;

    public AuthController(IHttpContextAccessor httpContextAccessor, IAuthApiService authApiService)
    {
        _authApiService = authApiService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View(new UserForLoginDto());
    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await LogoutUser();
        return Redirect("/auth/login");
    }


    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new UserForRegisterDto());
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] UserForLoginDto model)
    {
        if (ModelState.IsValid == false)
            return View(model);

        var result = await _authApiService.LoginAsync(model);

        if (result.Success == true)
        {
            await SignInUser(result.Data);
            return Redirect("/");
        }

        ModelState.AddModelError("", result.Message);
        return View(model);
    }
    [HttpPost("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] UserForRegisterDto model)
    {
        if (ModelState.IsValid == false)
            return View(model);

        var result = await _authApiService.RegisterAsync(model);

        if (result.Success == true)
        {
            await SignInUser(result.Data);
            return Redirect("/");
        }

        ModelState.AddModelError("", result.Message);
        return View(model);
    }


    private async Task LogoutUser()
    {
        _httpContextAccessor.HttpContext
            ?.Response.Cookies.Delete("tokenCookie");
        await _httpContextAccessor.HttpContext.SignOutAsync();
    }


    private async Task SignInUser(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("tokenCookie", token);

        var principal = new ClaimsPrincipal(identity);
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }


}
