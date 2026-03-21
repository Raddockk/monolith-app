
namespace Global;
public class UserLoginResultRepositoryDto
{
    public long Id { get; set; }
	public string Login { get; set; }
	public string? Mail { get; set; }
	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public string? Patronymic { get; set; }
	public short? Age { get; set; }

    public string RoleName { get;set; }
}