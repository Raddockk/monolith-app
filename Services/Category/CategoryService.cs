
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class CategoryService(ICategoryRepository repository,

ILogger<CategoryService> logger) : ICategoryService
{
    public async Task<CategoryServiceDto> AddAsync(AddCategoryServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddCategoryServiceDto, AddCategoryRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddCategoryServiceDto, AddCategoryRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<CategoryRepositoryDto, CategoryServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<CategoryRepositoryDto, CategoryServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<CategoryListServiceDto> GetAllAsync(CategoryQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryQueryServiceDto,CategoryQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<CategoryQueryServiceDto,CategoryQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<CategoryRepositoryDto,CategoryServiceDto>());
        var mapper2 = new Mapper(config2);
        return new CategoryListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<CategoryServiceDto>(x))
        };
    }

    public async Task<CategoryServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryRepositoryDto, CategoryServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<CategoryRepositoryDto, CategoryServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateCategoryServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateCategoryServiceDto, UpdateCategoryRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateCategoryServiceDto, UpdateCategoryRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}