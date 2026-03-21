
namespace Global;
public interface IFamilyMemberService
{
    public Task<FamilyMemberListServiceDto> GetAllAsync(FamilyMemberQueryServiceDto queryDto);

    public Task<FamilyMemberServiceDto> GetByIdAsync(long id);

    public Task<FamilyMemberServiceDto> AddAsync(AddFamilyMemberServiceDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateFamilyMemberServiceDto updateDto);
}