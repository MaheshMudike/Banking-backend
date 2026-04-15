namespace Banking.Api.Domain;

public class Transaction
{
    public int Id { get; set; }
    public string ReferenceNumber { get; set; } = "";
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = ""; // Debit or Credit
    public DateTime Date { get; set; }
    public string Description { get; set; } = "";
}
