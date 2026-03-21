
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddCategoryServiceDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}