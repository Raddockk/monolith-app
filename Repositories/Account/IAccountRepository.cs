
namespace Global;
public interface IAccountRepository
{
    public Task<AccountListRepositoryDto> GetAllAsync(AccountQueryRepositoryDto queryDto);

    public Task<AccountRepositoryDto> GetByIdAsync(short id);

    public Task<AccountRepositoryDto> AddAsync(AddAccountRepositoryDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateAccountRepositoryDto updateDto);
}