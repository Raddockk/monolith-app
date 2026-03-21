
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddGoalServiceDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
	public decimal TargetAmount { get; set; }
	public decimal CurrentAmount { get; set; }
	[Required]
	public long FamilyId { get; set; }
	[Required]
	public long CreatedById { get; set; }
}