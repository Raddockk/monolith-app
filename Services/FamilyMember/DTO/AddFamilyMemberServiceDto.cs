
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddFamilyMemberServiceDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	public long UserId { get; set; }
	[Required]
	public long FamilyId { get; set; }
	public DateTime JoinDate { get; set; }
}