using Banking.Api.Domain;
using Banking.Api.Infrastructure;

namespace Banking.Api.Repositories.Implementations;

public class BeneficiaryRepository : IBeneficiaryRepository
{
    private readonly InMemoryDb _db;

    public BeneficiaryRepository(InMemoryDb db)
    {
        _db = db;
    }

    public IEnumerable<Beneficiary> GetBeneficiaries(int userId)
    {
        return _db.Beneficiaries
            .Where(b => b.UserId == userId)
            .OrderBy(b => b.Name);
    }

    public Beneficiary AddBeneficiary(Beneficiary beneficiary)
    {
        beneficiary.Id = _db.Beneficiaries.Any()
            ? _db.Beneficiaries.Max(b => b.Id) + 1
            : 1;

        _db.Beneficiaries.Add(beneficiary);
        return beneficiary;
    }

    public void DeleteBeneficiary(int userId, int beneficiaryId)
    {
        var existing = _db.Beneficiaries
            .FirstOrDefault(b => b.Id == beneficiaryId && b.UserId == userId);

        if (existing != null)
        {
            _db.Beneficiaries.Remove(existing);
        }
    }
}
