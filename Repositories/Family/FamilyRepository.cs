
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class FamilyRepository(AppDbContext db) : IFamilyRepository
{ 
    DbSet<Family> set = db.Set<Family>();
    public async Task<FamilyRepositoryDto> AddAsync(AddFamilyRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddFamilyRepositoryDto, Family>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddFamilyRepositoryDto, Family>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Family,FamilyRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Family,FamilyRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Family>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<FamilyListRepositoryDto> GetAllAsync(FamilyQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Family,FamilyRepositoryDto>());
        var mapper = new Mapper(config);
        return new FamilyListRepositoryDto()
        {
            Items = mapper.Map<List<FamilyRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<FamilyRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Family,FamilyRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Family>(new {id});
        return mapper.Map<Family,FamilyRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateFamilyRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Family>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }



        await db.SaveChangesAsync();
    }
}