
using System.ComponentModel.DataAnnotations;
namespace Global;
public class AddUserServiceDto
{
    [Required]
	public long Id { get; set; }
	[Required]
	[StringLength(32)]
	public string Login { get; set; }
		[StringLength(50)]
	public string? Mail { get; set; }
	[Required]
	[StringLength(50)]
	public string Firstname { get; set; }
	[Required]
	[StringLength(50)]
	public string Lastname { get; set; }
		[StringLength(50)]
	public string? Patronymic { get; set; }
		public short? Age { get; set; }
	[Required]
	public short RoleId { get; set; }
    [Required]
    public string Password { get; set; }
}