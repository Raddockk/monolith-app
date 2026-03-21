
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UpdateUserControllerDto
{

    [Required]
    public long Id { get; set; }

	[StringLength(32)]
	public string? Login { get; set; }
	[StringLength(50)]
	public string? Mail { get; set; }
	[StringLength(50)]
	public string? Firstname { get; set; }
	[StringLength(50)]
	public string? Lastname { get; set; }
	[StringLength(50)]
	public string? Patronymic { get; set; }
	public short? Age { get; set; }
	public short? RoleId { get; set; }
    public string? Password { get; set; }
}