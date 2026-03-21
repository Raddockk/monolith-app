
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddCategoryControllerDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}