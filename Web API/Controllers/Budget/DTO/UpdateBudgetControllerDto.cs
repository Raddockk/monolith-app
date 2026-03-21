
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateBudgetControllerDto
{
    [Required]
	public short Id { get; set; }
	public decimal? BudgetAmount { get; set; }
	[StringLength(10)]
	public string? Period { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public long? FamilyId { get; set; }
	public short? CategoryId { get; set; }
	public long? CreatedById { get; set; }
}