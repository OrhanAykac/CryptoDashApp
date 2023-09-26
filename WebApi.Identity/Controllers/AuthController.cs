using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Dto;
using WebApi.Identity.Services;
using Shared.Results;

namespace WebApi.Identity.Controllers;

[Route("auth")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLogin)
    {

        var result = await _authService.LoginAsync(userForLogin);

        if (result.Success == true)
        {
            var token = JwtTokenHelper.CreateToken(result.Data);
            return Ok(new BaseDataResponse<string>(token,true));
        }
        return BadRequest(new BaseResponse(false, result.Message));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegister)
    {
        var result = await _authService.RegisterAsync(userForRegister);

        if (result.Success == true)
        {
            var token = JwtTokenHelper.CreateToken(result.Data);
            return Ok(new BaseDataResponse<string>(token,true));
        }
        return BadRequest(new BaseResponse(false, result.Message));

    }
}
