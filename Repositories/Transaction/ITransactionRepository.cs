
namespace Global;
public interface ITransactionRepository
{
    public Task<TransactionListRepositoryDto> GetAllAsync(TransactionQueryRepositoryDto queryDto);

    public Task<TransactionRepositoryDto> GetByIdAsync(int id);

    public Task<TransactionRepositoryDto> AddAsync(AddTransactionRepositoryDto addDto);

    public Task DeleteAsync(int id);

    public Task UpdateAsync(UpdateTransactionRepositoryDto updateDto);
}