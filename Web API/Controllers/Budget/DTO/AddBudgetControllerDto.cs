
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddBudgetControllerDto
{
	public decimal BudgetAmount { get; set; }
	[Required]
	[StringLength(10)]
	public string Period { get; set; }
	[Required]
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	[Required]
	public long FamilyId { get; set; }
	public short? CategoryId { get; set; }
	[Required]
	public long CreatedById { get; set; }
}