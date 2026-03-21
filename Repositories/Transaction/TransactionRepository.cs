
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class TransactionRepository(AppDbContext db) : ITransactionRepository
{ 
    DbSet<Transaction> set = db.Set<Transaction>();
    public async Task<TransactionRepositoryDto> AddAsync(AddTransactionRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddTransactionRepositoryDto, Transaction>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddTransactionRepositoryDto, Transaction>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Transaction,TransactionRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Transaction,TransactionRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Transaction>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<TransactionListRepositoryDto> GetAllAsync(TransactionQueryRepositoryDto queryDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Transaction,TransactionRepositoryDto>());
        var mapper = new Mapper(config);
        return new TransactionListRepositoryDto()
        {
            Items = mapper.Map<List<TransactionRepositoryDto>>(
            await set
.Skip(queryDto.Offset).Take(queryDto.Count < 50 ? queryDto.Count : 50).ToListAsync()
            )
        };
    }

    public async Task<TransactionRepositoryDto> GetByIdAsync(int id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Transaction,TransactionRepositoryDto>());
        var mapper = new Mapper(config);
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Transaction>(new {id});
        return mapper.Map<Transaction,TransactionRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateTransactionRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Transaction>(new {Id = updateDto.Id});
		if(updateDto.Amount.HasValue){
            entity.Amount = updateDto.Amount.Value;
        }
		if(!String.IsNullOrEmpty(updateDto.Type)){
            entity.Type = updateDto.Type;
        }
		if(updateDto.TransactionDate.HasValue){
            entity.TransactionDate = updateDto.TransactionDate.Value;
        }
		if(updateDto.AccountId.HasValue){
            entity.AccountId = updateDto.AccountId.Value;
        }



		if(updateDto.UserId.HasValue){
            entity.UserId = updateDto.UserId.Value;
        }

        await db.SaveChangesAsync();
    }
}