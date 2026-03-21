
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateAccountServiceDto
{
    [Required]
	public short Id { get; set; }
	[StringLength(50)]
	public string? Name { get; set; }
	public decimal? Balance { get; set; }
	[StringLength(3)]
	public string? Currency { get; set; }
	public short? BankId { get; set; }
	public long? UserId { get; set; }
}