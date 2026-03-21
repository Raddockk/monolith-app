
using System.ComponentModel.DataAnnotations;
namespace Global;

public class AddDebtRepositoryDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }	
	public decimal Amount { get; set; }
	[StringLength(3)]
	public string Currency { get;set; }
	[Required]	
	public DateTime DueDate { get; set; }
	[Required]	
	public short AccountId { get; set; }
	[Required]
	public short? CategoryId { get; set; }
	[Required]
	public long UserId { get; set; }
}