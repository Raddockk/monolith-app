
namespace Global;
public interface ICategoryService
{
    public Task<CategoryListServiceDto> GetAllAsync(CategoryQueryServiceDto queryDto);

    public Task<CategoryServiceDto> GetByIdAsync(short id);

    public Task<CategoryServiceDto> AddAsync(AddCategoryServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateCategoryServiceDto updateDto);
}