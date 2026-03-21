
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddRoleControllerDto
{
	[Required]
	[StringLength(10)]
	public string Name { get; set; }
}