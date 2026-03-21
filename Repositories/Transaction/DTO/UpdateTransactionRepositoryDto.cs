
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateTransactionRepositoryDto
{
    [Required]
	public int Id { get; set; }
	public decimal? Amount { get; set; }
	[StringLength(7)]
	public string? Type { get; set; }
	public DateTime? TransactionDate { get; set; }
	public short? AccountId { get; set; }
	public short? CategoryId { get; set; }
	public long? UserId { get; set; }
}