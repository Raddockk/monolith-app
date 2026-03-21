
namespace Global;
public interface IBudgetRepository
{
    public Task<BudgetListRepositoryDto> GetAllAsync(BudgetQueryRepositoryDto queryDto);

    public Task<BudgetRepositoryDto> GetByIdAsync(short id);

    public Task<BudgetRepositoryDto> AddAsync(AddBudgetRepositoryDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateBudgetRepositoryDto updateDto);
}