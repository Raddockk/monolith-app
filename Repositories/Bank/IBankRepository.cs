
namespace Global;
public interface IBankRepository
{
    public Task<BankListRepositoryDto> GetAllAsync(BankQueryRepositoryDto queryDto);

    public Task<BankRepositoryDto> GetByIdAsync(short id);

    public Task<BankRepositoryDto> AddAsync(AddBankRepositoryDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateBankRepositoryDto updateDto);
}