
namespace Global;
public interface IFamilyService
{
    public Task<FamilyListServiceDto> GetAllAsync(FamilyQueryServiceDto queryDto);

    public Task<FamilyServiceDto> GetByIdAsync(long id);

    public Task<FamilyServiceDto> AddAsync(AddFamilyServiceDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateFamilyServiceDto updateDto);
}