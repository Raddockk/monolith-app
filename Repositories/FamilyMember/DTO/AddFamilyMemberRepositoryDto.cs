
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddFamilyMemberRepositoryDto
{
	[Required]
	public long Id { get; set; }
	[Required]
	public long UserId { get; set; }
	[Required]
	public long FamilyId { get; set; }
	public DateTime JoinDate { get; set; }
}