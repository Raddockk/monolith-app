using System.ComponentModel.DataAnnotations;
public class Account
{
	[Required]
	public short Id { get; set; }
	[StringLength(50)]
	public string Name { get; set; }
	public decimal Balance { get; set; }
	[StringLength(3)]
	public string Currency { get; set; }
	public short? BankId { get; set; }
	public Bank Bank { get; set; }
	public long UserId { get; set; }
	public User User { get; set; }
	public ICollection<Transaction> Transactions { get; set; }
	public ICollection<Debt> Debts { get;set; }
}