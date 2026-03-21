
namespace Global;
public class UserListServiceDto
{
    public IEnumerable<UserServiceDto> Items { get; set; }
    public int TotalCount { get; set; }
}