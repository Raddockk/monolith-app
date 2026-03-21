
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateRoleRepositoryDto
{
    [Required]
	public short Id { get; set; }
	[StringLength(10)]
	public string? Name { get; set; }
}