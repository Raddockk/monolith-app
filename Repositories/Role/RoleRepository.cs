
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class RoleRepository(AppDbContext db) : IRoleRepository
{ 
    DbSet<Role> set = db.Set<Role>();
    public async Task<RoleRepositoryDto> AddAsync(AddRoleRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddRoleRepositoryDto, Role>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddRoleRepositoryDto, Role>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Role,RoleRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Role,RoleRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Role>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<RoleListRepositoryDto> GetAllAsync(RoleQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Role,RoleRepositoryDto>());
        var mapper = new Mapper(config);
        return new RoleListRepositoryDto()
        {
            Items = mapper.Map<List<RoleRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<RoleRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Role,RoleRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Role>(new {id});
        return mapper.Map<Role,RoleRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateRoleRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Role>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }

        await db.SaveChangesAsync();
    }
}