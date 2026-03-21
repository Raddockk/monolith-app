
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateBankRepositoryDto
{
    [Required]
	public short Id { get; set; }
	[StringLength(30)]
	public string? Name { get; set; }
}