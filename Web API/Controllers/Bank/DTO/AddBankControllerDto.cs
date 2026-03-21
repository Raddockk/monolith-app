
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddBankControllerDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
}