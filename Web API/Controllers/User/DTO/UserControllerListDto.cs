
namespace Global;
public class UserListControllerDto
{
    public IEnumerable<UserControllerDto> Items { get; set; }

    public int TotalCount { get; set; }
}