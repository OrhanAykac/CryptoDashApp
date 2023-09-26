using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RiseX.Business.Abstract;
using RiseX.Entities.Dto;
using System.Security.Claims;

namespace RiseX.WebUI.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthService _authService;
    public AuthController(IHttpContextAccessor httpContextAccessor, IAuthService authService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View(new UserForLoginDto());
    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync();
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

        var result = await _authService.LoginAsync(model);

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

        var result = await _authService.RegisterAsync(model);

        if (result.Success == true)
        {
            await SignInUser(result.Data);
            return Redirect("/");
        }

        ModelState.AddModelError("", result.Message);
        return View(model);
    }


    private async Task SignInUser(Entities.Concrete.User user)
    {

        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.GivenName, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

}
