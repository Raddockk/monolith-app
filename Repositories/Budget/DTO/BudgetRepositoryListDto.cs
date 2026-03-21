
namespace Global;
public class BudgetListRepositoryDto
{
    public IEnumerable<BudgetRepositoryDto> Items { get; set; }

    public int TotalCount { get; set; }
}