using Banking.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/accounts")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult GetUserAccounts()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var accounts = _accountService.GetUserAccounts(userId);

        return Ok(accounts);
    }


    [HttpGet("debit")]
    public IActionResult GetDebitAccounts()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var accounts = _accountService.GetUserAccounts(userId);
        return Ok(accounts);
    }

    [HttpGet("credit")]
    public IActionResult GetCreditAccounts()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var accounts = _accountService.GetCreditAccounts(userId);
        return Ok(accounts);
    }

}
