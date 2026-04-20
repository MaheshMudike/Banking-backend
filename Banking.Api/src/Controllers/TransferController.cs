using Banking.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/transfer")]
[Authorize]
public class TransferController : ControllerBase
{
    private readonly IFundTransferService _transferService;

    public TransferController(IFundTransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost]
    public IActionResult Transfer([FromBody] TransferRequest request)
    {
        var success = _transferService.Transfer(
            request.FromAccountId,
            request.ToAccountId,
            request.ToAccountNumber,
            request.Amount,
            out string message,
            out string? reference
        );

        if (!success)
            return BadRequest(new { message });

        return Ok(new { message, reference });
    }

}

public class TransferRequest
{
    public int FromAccountId { get; set; }
    public int? ToAccountId { get; set; }         
    public string? ToAccountNumber { get; set; } 
    public decimal Amount { get; set; }
}
