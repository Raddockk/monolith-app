
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddTransactionServiceDto
{
	public decimal Amount { get; set; }
	[Required]
	[StringLength(7)]
	public string Type { get; set; }
	[Required]
	public DateTime TransactionDate { get; set; }
	[Required]
	public short AccountId { get; set; }
	public short? CategoryId { get; set; }
	[Required]
	public long UserId { get; set; }
}