
namespace Global;
public interface IAccountService
{
    public Task<AccountListServiceDto> GetAllAsync(AccountQueryServiceDto queryDto);

    public Task<AccountServiceDto> GetByIdAsync(short id);

    public Task<AccountServiceDto> AddAsync(AddAccountServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateAccountServiceDto updateDto);
}