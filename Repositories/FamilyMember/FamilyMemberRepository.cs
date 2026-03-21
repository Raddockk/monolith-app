
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class FamilyMemberRepository(AppDbContext db) : IFamilyMemberRepository
{ 
    DbSet<FamilyMember> set = db.Set<FamilyMember>();
    public async Task<FamilyMemberRepositoryDto> AddAsync(AddFamilyMemberRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddFamilyMemberRepositoryDto, FamilyMember>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddFamilyMemberRepositoryDto, FamilyMember>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FamilyMember,FamilyMemberRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<FamilyMember,FamilyMemberRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<FamilyMember>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<FamilyMemberListRepositoryDto> GetAllAsync(FamilyMemberQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FamilyMember,FamilyMemberRepositoryDto>());
        var mapper = new Mapper(config);
        return new FamilyMemberListRepositoryDto()
        {
            Items = mapper.Map<List<FamilyMemberRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<FamilyMemberRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FamilyMember,FamilyMemberRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<FamilyMember>(new {id});
        return mapper.Map<FamilyMember,FamilyMemberRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateFamilyMemberRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<FamilyMember>(new {Id = updateDto.Id});
		if(updateDto.UserId.HasValue){
            entity.UserId = updateDto.UserId.Value;
        }

		if(updateDto.FamilyId.HasValue){
            entity.FamilyId = updateDto.FamilyId.Value;
        }

		if(updateDto.JoinDate.HasValue){
            entity.JoinDate = updateDto.JoinDate.Value;
        }
        await db.SaveChangesAsync();
    }
}