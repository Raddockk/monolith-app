
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class CategoryRepository(AppDbContext db) : ICategoryRepository
{ 
    DbSet<Category> set = db.Set<Category>();
    public async Task<CategoryRepositoryDto> AddAsync(AddCategoryRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddCategoryRepositoryDto, Category>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddCategoryRepositoryDto, Category>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Category,CategoryRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Category,CategoryRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Category>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<CategoryListRepositoryDto> GetAllAsync(CategoryQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Category,CategoryRepositoryDto>());
        var mapper = new Mapper(config);
        return new CategoryListRepositoryDto()
        {
            Items = mapper.Map<List<CategoryRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<CategoryRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Category,CategoryRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Category>(new {id});
        return mapper.Map<Category,CategoryRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateCategoryRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Category>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }


        await db.SaveChangesAsync();
    }
}