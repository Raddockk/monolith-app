
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateFamilyRepositoryDto
{
    [Required]
	public long Id { get; set; }
	[StringLength(30)]
	public string? Name { get; set; }
}