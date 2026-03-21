
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddAccountControllerDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
	public decimal Balance { get; set; }
	[Required]
	[StringLength(3)]
	public string Currency { get; set; }
	public short? BankId { get; set; }
	[Required]
	public long UserId { get; set; }
}