
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class DebtRepository(AppDbContext db) : IDebtRepository
{ 
    DbSet<Debt> set = db.Set<Debt>();
    public async Task<DebtRepositoryDto> AddAsync(AddDebtRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddDebtRepositoryDto, Debt>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddDebtRepositoryDto, Debt>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Debt,DebtRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Debt,DebtRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Debt>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<DebtListRepositoryDto> GetAllAsync(DebtQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Debt,DebtRepositoryDto>());
        var mapper = new Mapper(config);
        return new DebtListRepositoryDto()
        {
            Items = mapper.Map<List<DebtRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<DebtRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Debt,DebtRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Debt>(new {id});
        return mapper.Map<Debt,DebtRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateDebtRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Debt>(new {Id = updateDto.Id});
        if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }

		if (updateDto.Amount.HasValue)
        {
            entity.Amount = updateDto.Amount.Value;
        }
        
		if (!String.IsNullOrEmpty(updateDto.Currency))
        {
            entity.Currency = updateDto.Currency;
        }
        
		if (updateDto.DueDate.HasValue)
        {
            entity.DueDate = updateDto.DueDate.Value;
        }

		if(updateDto.AccountId.HasValue){
            entity.AccountId = updateDto.AccountId.Value;
        }

		if(updateDto.CategoryId.HasValue){
            entity.CategoryId = updateDto.CategoryId.Value;
        }

        if(updateDto.UserId.HasValue){
            entity.UserId = updateDto.UserId.Value;
        }

        await db.SaveChangesAsync();
    }
}