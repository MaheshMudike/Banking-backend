using Banking.Api.Domain;

namespace Banking.Api.Services;

public interface IBeneficiaryService
{
    IEnumerable<Beneficiary> GetBeneficiaries(int userId);
    Beneficiary AddBeneficiary(int userId, string name, string accountNumber, string nickname);
    void DeleteBeneficiary(int userId, int beneficiaryId);
}
