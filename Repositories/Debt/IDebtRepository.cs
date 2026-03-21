
namespace Global;
public interface IDebtRepository
{
    public Task<DebtListRepositoryDto> GetAllAsync(DebtQueryRepositoryDto queryDto);

    public Task<DebtRepositoryDto> GetByIdAsync(short id);

    public Task<DebtRepositoryDto> AddAsync(AddDebtRepositoryDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateDebtRepositoryDto updateDto);
}