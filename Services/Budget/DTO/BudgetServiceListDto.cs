
namespace Global;
public class BudgetListServiceDto
{
    public IEnumerable<BudgetServiceDto> Items { get; set; }

    public int TotalCount { get; set; }
}