
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UserLoginRepositoryDto
{
    [Required]
    [StringLength(32)]
    public string Login {get; set;}
    [Required]
    public string PasswordHash { get; set; }
}