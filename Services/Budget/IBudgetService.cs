
namespace Global;
public interface IBudgetService
{
    public Task<BudgetListServiceDto> GetAllAsync(BudgetQueryServiceDto queryDto);

    public Task<BudgetServiceDto> GetByIdAsync(short id);

    public Task<BudgetServiceDto> AddAsync(AddBudgetServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateBudgetServiceDto updateDto);
}