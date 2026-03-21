using System.ComponentModel.DataAnnotations;
public class Transaction{
    [Required]
	public int Id { get;set; }
	public decimal Amount { get;set; }
	[StringLength(7)]
	public string Type { get;set; }
	public DateTime TransactionDate { get;set; }
	public short AccountId { get;set; }
	public Account Account {get;set;}
	public short? CategoryId { get;set; }
	public Category Category {get;set;}
	public long UserId { get;set; }
	public User User {get;set;}
}