
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class AccountRepository(AppDbContext db) : IAccountRepository
{ 
    DbSet<Account> set = db.Set<Account>();
    public async Task<AccountRepositoryDto> AddAsync(AddAccountRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddAccountRepositoryDto, Account>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddAccountRepositoryDto, Account>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Account,AccountRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Account,AccountRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Account>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<AccountListRepositoryDto> GetAllAsync(AccountQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Account,AccountRepositoryDto>());
        var mapper = new Mapper(config);
        return new AccountListRepositoryDto()
        {
            Items = mapper.Map<List<AccountRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<AccountRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Account,AccountRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Account>(new {id});
        return mapper.Map<Account,AccountRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateAccountRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Account>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }
		if(updateDto.Balance.HasValue){
            entity.Balance = updateDto.Balance.Value;
        }
		if(!String.IsNullOrEmpty(updateDto.Currency)){
            entity.Currency = updateDto.Currency;
        }


		if(updateDto.UserId.HasValue){
            entity.UserId = updateDto.UserId.Value;
        }


        await db.SaveChangesAsync();
    }
}