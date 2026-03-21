
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UserLoginServiceDto
{
    [Required]
    [StringLength(32)]
    public string Login {get; set;}
    [Required]
    public string Password { get; set; }
}