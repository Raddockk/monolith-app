
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateDebtServiceDto
{
    [Required]
	public short Id { get; set; }
	[StringLength(50)]
	public string Name { get; set; }	
	public decimal? Amount { get; set; }
	[StringLength(3)]
	public string Currency { get;set; }
	public DateTime? DueDate { get;set; }
	public short? AccountId { get;set; }
	public short? CategoryId { get;set; }
	public long? UserId { get;set; }
}