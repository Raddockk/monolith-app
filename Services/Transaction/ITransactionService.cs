
namespace Global;
public interface ITransactionService
{
    public Task<TransactionListServiceDto> GetAllAsync(TransactionQueryServiceDto queryDto);

    public Task<TransactionServiceDto> GetByIdAsync(int id);

    public Task<TransactionServiceDto> AddAsync(AddTransactionServiceDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateTransactionServiceDto updateDto);
}