
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddFamilyControllerDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	[StringLength(30)]
	public string Name { get; set; }
}