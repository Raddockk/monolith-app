
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateRoleServiceDto
{
    [Required]
	public short Id { get; set; }
	[StringLength(10)]
	public string? Name { get; set; }
}