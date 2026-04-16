using Banking.Api.Domain;

namespace Banking.Api.Repositories;

public interface IBeneficiaryRepository
{
    IEnumerable<Beneficiary> GetBeneficiaries(int userId);
    Beneficiary AddBeneficiary(Beneficiary beneficiary);
    void DeleteBeneficiary(int userId, int beneficiaryId);
}
