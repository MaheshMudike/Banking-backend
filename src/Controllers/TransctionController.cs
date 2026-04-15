using Banking.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/transactions")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{accountId}")]
    public IActionResult GetRecentTransactions(int accountId)
    {
        var transactions = _transactionService.GetRecentTransactions(accountId);
        return Ok(transactions);
    }
}
