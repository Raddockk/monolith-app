
namespace Global;
public interface IDebtService
{
    public Task<DebtListServiceDto> GetAllAsync(DebtQueryServiceDto queryDto);

    public Task<DebtServiceDto> GetByIdAsync(short id);

    public Task<DebtServiceDto> AddAsync(AddDebtServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateDebtServiceDto updateDto);
}