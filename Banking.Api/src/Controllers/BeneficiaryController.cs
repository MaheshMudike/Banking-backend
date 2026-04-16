using Banking.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/beneficiaries")]
[Authorize]
public class BeneficiaryController : ControllerBase
{
    private readonly IBeneficiaryService _beneficiaryService;

    public BeneficiaryController(IBeneficiaryService beneficiaryService)
    {
        _beneficiaryService = beneficiaryService;
    }

    [HttpGet]
    public IActionResult GetBeneficiaries()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var list = _beneficiaryService.GetBeneficiaries(userId);
        return Ok(list);
    }

    public record AddBeneficiaryRequest(string Name, string AccountNumber, string Nickname);

    [HttpPost]
    public IActionResult AddBeneficiary([FromBody] AddBeneficiaryRequest request)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var created = _beneficiaryService.AddBeneficiary(
            userId,
            request.Name,
            request.AccountNumber,
            request.Nickname
        );

        return CreatedAtAction(nameof(GetBeneficiaries), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBeneficiary(int id)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        _beneficiaryService.DeleteBeneficiary(userId, id);
        return NoContent();
    }
}
