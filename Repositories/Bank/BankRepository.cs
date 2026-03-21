
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class BankRepository(AppDbContext db) : IBankRepository
{ 
    DbSet<Bank> set = db.Set<Bank>();
    public async Task<BankRepositoryDto> AddAsync(AddBankRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddBankRepositoryDto, Bank>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddBankRepositoryDto, Bank>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Bank,BankRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Bank,BankRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Bank>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<BankListRepositoryDto> GetAllAsync(BankQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Bank,BankRepositoryDto>());
        var mapper = new Mapper(config);
        return new BankListRepositoryDto()
        {
            Items = mapper.Map<List<BankRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<BankRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Bank,BankRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Bank>(new {id});
        return mapper.Map<Bank,BankRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateBankRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Bank>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }

        await db.SaveChangesAsync();
    }
}