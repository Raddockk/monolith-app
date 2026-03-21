using System.ComponentModel.DataAnnotations;
public class Category
{
	[Required]
	public short Id { get; set; }
	[StringLength(50)]
	public string Name { get; set; }
	public ICollection<Transaction> Transactions { get; set; }
	public ICollection<Budget> Budgets { get; set; }
	public ICollection<Debt> Debts { get;set; }
}