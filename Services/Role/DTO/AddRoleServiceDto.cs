
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddRoleServiceDto
{
	[Required]
	[StringLength(10)]
	public string Name { get; set; }
}