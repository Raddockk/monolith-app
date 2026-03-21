
namespace Global;
public interface IFamilyMemberRepository
{
    public Task<FamilyMemberListRepositoryDto> GetAllAsync(FamilyMemberQueryRepositoryDto queryDto);

    public Task<FamilyMemberRepositoryDto> GetByIdAsync(long id);

    public Task<FamilyMemberRepositoryDto> AddAsync(AddFamilyMemberRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateFamilyMemberRepositoryDto updateDto);
}