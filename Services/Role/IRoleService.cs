
namespace Global;
public interface IRoleService
{
    public Task<RoleListServiceDto> GetAllAsync(RoleQueryServiceDto queryDto);

    public Task<RoleServiceDto> GetByIdAsync(short id);

    public Task<RoleServiceDto> AddAsync(AddRoleServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateRoleServiceDto updateDto);
}