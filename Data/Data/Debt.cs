using System.ComponentModel.DataAnnotations;
public class Debt{
    [Required]
	public long Id { get;set; }
    public string Name { get; set; }
	public decimal Amount { get; set; }
	[StringLength(3)]
	public string Currency { get;set; }
	public DateTime DueDate { get;set; }
	public short AccountId { get;set; }
	public Account Account {get;set;}
	public short? CategoryId { get;set; }
	public Category Category {get;set;}
	public long UserId { get;set; }
	public User User {get;set;}
}