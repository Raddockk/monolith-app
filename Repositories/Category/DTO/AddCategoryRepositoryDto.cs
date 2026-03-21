
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddCategoryRepositoryDto
{
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
}