
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateFamilyServiceDto
{
    [Required]
	public long Id { get; set; }
	[StringLength(30)]
	public string? Name { get; set; }
}