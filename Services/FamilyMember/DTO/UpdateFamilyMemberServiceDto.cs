
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateFamilyMemberServiceDto
{
    [Required]
	public long Id { get; set; }
	public long? UserId { get; set; }
	public long? FamilyId { get; set; }
	public DateTime? JoinDate { get; set; }
}