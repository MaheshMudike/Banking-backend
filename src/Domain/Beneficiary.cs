namespace Banking.Api.Domain;

public class Beneficiary
{
    public int Id { get; set; }
    public int UserId { get; set; }         
    public string Name { get; set; } = ""; 
    public string AccountNumber { get; set; } = "";
    public string Nickname { get; set; } = "";
}
