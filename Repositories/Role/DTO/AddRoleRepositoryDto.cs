
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddRoleRepositoryDto
{
	[Required]
	[StringLength(10)]
	public string Name { get; set; }
}