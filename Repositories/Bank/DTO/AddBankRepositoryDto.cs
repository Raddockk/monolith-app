
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddBankRepositoryDto
{
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
}