using Banking.Api.Domain;

namespace Banking.Api.Infrastructure;

public class InMemoryDb
{
    public List<User> Users { get; set; } = new();
    public List<Account> Accounts { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
    public List<Beneficiary> Beneficiaries { get; set; } = new();

    public InMemoryDb()
    {
        // Add Users
        Users.Add(new User { Id = 1, Username = "mahesh", Password = "999@Abcd" });

        // Add Accounts
        Accounts.Add(new Account { Id = 1, UserId = 1, AccountNumber = "ACC1001", Balance = 5000 });
        Accounts.Add(new Account { Id = 2, UserId = 1, AccountNumber = "ACC1002", Balance = 12000 });

        // Beneficiary accounts (external)
        Accounts.Add(new Account { Id = 3, UserId = 0, AccountNumber = "ACC2001", Balance = 15000 });
        Accounts.Add(new Account { Id = 4, UserId = 0, AccountNumber = "ACC3001", Balance = 8000 });


        // Add Transactions for Account 1 (5 transactions)
        Transactions.Add(new Transaction
        {
            Id = 1,
            AccountId = 1,
            Amount = 200,
            Type = "DEBIT",
            Date = DateTime.Now.AddDays(-5),
            ReferenceNumber = Guid.NewGuid().ToString(),
            Description = "Grocery Shopping"
        });

        Transactions.Add(new Transaction
        {
            Id = 2,
            AccountId = 1,
            Amount = 500,
            Type = "CREDIT",
            Date = DateTime.Now.AddDays(-4),
            ReferenceNumber = Guid.NewGuid().ToString(),
            Description = "Salary Credit"
        });

        Transactions.Add(new Transaction
        {
            Id = 3,
            AccountId = 1,
            Amount = 150,
            Type = "DEBIT",
            Date = DateTime.Now.AddDays(-3),
            ReferenceNumber = Guid.NewGuid().ToString(),
            Description = "Electricity Bill"
        });

        Transactions.Add(new Transaction
        {
            Id = 4,
            AccountId = 1,
            Amount = 300,
            Type = "DEBIT",
            Date = DateTime.Now.AddDays(-2),
            ReferenceNumber = Guid.NewGuid().ToString(),
            Description = "Mobile Recharge"
        });

        Transactions.Add(new Transaction
        {
            Id = 5,
            AccountId = 1,
            Amount = 1000,
            Type = "CREDIT",
            Date = DateTime.Now.AddDays(-1),
            ReferenceNumber = Guid.NewGuid().ToString(),
            Description = "Refund Received"
        });

        // Add Transactions for Account 2 (2 transactions)
        Transactions.Add(new Transaction
        {
            Id = 6,
            AccountId = 2,
            Amount = 800,
            Type = "DEBIT",
            Date = DateTime.Now.AddDays(-3),
            ReferenceNumber = Guid.NewGuid().ToString(),
            Description = "Online Shopping"
        });

        Transactions.Add(new Transaction
        {
            Id = 7,
            AccountId = 2,
            Amount = 2000,
            Type = "CREDIT",
            Date = DateTime.Now.AddDays(-1),
            ReferenceNumber = Guid.NewGuid().ToString(),
            Description = "Bonus Credit"
        });


       //Existing Beneficiaries
        Beneficiaries.Add(new Beneficiary
        {
            Id = 1,
            UserId = 1,
            Name = "Mohhamed Ali",
            AccountNumber = "ACC2001",
            Nickname = "Friend Ali"
        });

        Beneficiaries.Add(new Beneficiary
        {
            Id = 2,
            UserId = 1,
            Name = "Sara Patel",
            AccountNumber = "ACC3001",
            Nickname = "Sister Sara"
        });
    }
}
