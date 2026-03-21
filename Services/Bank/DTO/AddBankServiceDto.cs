
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddBankServiceDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
}