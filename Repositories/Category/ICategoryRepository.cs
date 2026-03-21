
namespace Global;
public interface ICategoryRepository
{
    public Task<CategoryListRepositoryDto> GetAllAsync(CategoryQueryRepositoryDto queryDto);

    public Task<CategoryRepositoryDto> GetByIdAsync(short id);

    public Task<CategoryRepositoryDto> AddAsync(AddCategoryRepositoryDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateCategoryRepositoryDto updateDto);
}