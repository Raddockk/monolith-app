
namespace Global;
public class UserControllerDto
{
    public long Id { get; set; }
    public string Login { get; set; } = null!;
    public string? Mail { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public short? Age { get; set; }
    public short RoleId { get; set; }
    public string? RoleName { get; set; }
}