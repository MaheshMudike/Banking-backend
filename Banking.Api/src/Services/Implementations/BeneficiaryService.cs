using Banking.Api.Domain;
using Banking.Api.Repositories;

namespace Banking.Api.Services.Implementations;

public class BeneficiaryService : IBeneficiaryService
{
    private readonly IBeneficiaryRepository _beneficiaryRepo;

    private readonly IAccountRepository _accountRepo;


    public BeneficiaryService(IBeneficiaryRepository beneficiaryRepo, IAccountRepository accountRepo)
    {
        _beneficiaryRepo = beneficiaryRepo;
        _accountRepo = accountRepo;

    }

    public IEnumerable<Beneficiary> GetBeneficiaries(int userId)
    {
        return _beneficiaryRepo.GetBeneficiaries(userId);
    }

   public Beneficiary AddBeneficiary(int userId, string name, string accountNumber, string nickname)
    {
        var beneficiary = new Beneficiary
        {
            UserId = userId,
            Name = name,
            AccountNumber = accountNumber,
            Nickname = nickname
        };

        var created = _beneficiaryRepo.AddBeneficiary(beneficiary);
        
         _accountRepo.AddExternalAccount(accountNumber);

        return created;
    }

    public void DeleteBeneficiary(int userId, int beneficiaryId)
    {
        _beneficiaryRepo.DeleteBeneficiary(userId, beneficiaryId);
    }
}
