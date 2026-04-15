namespace Banking.Api.Services;

public interface IFundTransferService
{
    bool Transfer(int fromAccountId, int toAccountId, decimal amount, out string message, out string? reference);
}

