
namespace Global;
public class BudgetListControllerDto
{
    public IEnumerable<BudgetControllerDto> Items { get; set; }

    public int TotalCount { get; set; }
}