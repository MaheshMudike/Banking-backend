using Banking.Api.Services;
using Banking.Api.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly TokenService _tokenService;

    public AuthController(IUserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userService.ValidateUser(request.Username, request.Password);

        if (user == null)
            return Unauthorized(new { message = "Invalid username or password" });

        var token = _tokenService.GenerateToken(user);

        return Ok(new { token });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
