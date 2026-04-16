using Banking.Api.Domain;
using Banking.Api.Repositories;

namespace Banking.Api.Services.Implementations;

public class BeneficiaryService : IBeneficiaryService
{
    private readonly IBeneficiaryRepository _beneficiaryRepo;

    public BeneficiaryService(IBeneficiaryRepository beneficiaryRepo)
    {
        _beneficiaryRepo = beneficiaryRepo;
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

        return _beneficiaryRepo.AddBeneficiary(beneficiary);
    }

    public void DeleteBeneficiary(int userId, int beneficiaryId)
    {
        _beneficiaryRepo.DeleteBeneficiary(userId, beneficiaryId);
    }
}
