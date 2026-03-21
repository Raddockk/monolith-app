
namespace Global;
public interface IBankService
{
    public Task<BankListServiceDto> GetAllAsync(BankQueryServiceDto queryDto);

    public Task<BankServiceDto> GetByIdAsync(short id);

    public Task<BankServiceDto> AddAsync(AddBankServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateBankServiceDto updateDto);
}