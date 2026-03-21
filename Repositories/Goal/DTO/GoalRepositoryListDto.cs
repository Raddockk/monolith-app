
namespace Global;
public class GoalListRepositoryDto
{
    public IEnumerable<GoalRepositoryDto> Items { get; set; }

    public int TotalCount { get; set; }
}