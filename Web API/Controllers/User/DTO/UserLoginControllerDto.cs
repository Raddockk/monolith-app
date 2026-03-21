
using System.ComponentModel.DataAnnotations;
namespace Global;
public class UserLoginControllerDto
{
    [Required]
    [StringLength(32)]
    public string Login {get; set;}
    [Required]
    public string Password { get; set; }
}