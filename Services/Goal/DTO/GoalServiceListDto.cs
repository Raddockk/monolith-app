
namespace Global;
public class GoalListServiceDto
{
    public IEnumerable<GoalServiceDto> Items { get; set; }
    public int TotalCount { get; set; }
}