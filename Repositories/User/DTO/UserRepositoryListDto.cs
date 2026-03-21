
namespace Global;
public class UserListRepositoryDto
{
    public IEnumerable<UserRepositoryDto> Items { get; set; }

    public int TotalCount { get; set; }
}