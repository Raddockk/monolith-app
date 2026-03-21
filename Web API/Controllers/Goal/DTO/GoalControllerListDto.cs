
namespace Global;
public class GoalListControllerDto
{
    public IEnumerable<GoalControllerDto> Items { get; set; }
    public int TotalCount { get; set; }
}