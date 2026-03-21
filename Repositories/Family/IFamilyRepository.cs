
namespace Global;
public interface IFamilyRepository
{
    public Task<FamilyListRepositoryDto> GetAllAsync(FamilyQueryRepositoryDto queryDto);

    public Task<FamilyRepositoryDto> GetByIdAsync(long id);

    public Task<FamilyRepositoryDto> AddAsync(AddFamilyRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateFamilyRepositoryDto updateDto);
}