using Microsoft.AspNetCore.Mvc;
using Business.Abstract;

namespace WebApi.Identity.Controllers;

[Route("auth")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public IActionResult Login()
    {
        return Ok();
    }

    [HttpPost("register")]
    public IActionResult Register()
    {
        return Ok();
    }
}
