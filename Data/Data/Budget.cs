using System.ComponentModel.DataAnnotations;
public class Budget{
    [Required]
	public short Id { get;set; }
	public decimal BudgetAmount { get;set; }
	[StringLength(10)]
	public string Period { get;set; }
	public DateTime StartDate { get;set; }
	public DateTime? EndDate { get;set; }
	public long FamilyId { get;set; }
	public Family Family {get;set;}
	public short? CategoryId { get;set; }
	public Category Category {get;set;}
	public long CreatedById { get;set; }
	public User User {get;set;}
}