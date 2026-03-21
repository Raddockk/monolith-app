
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class TransactionService(ITransactionRepository repository,
IAccountRepository accountRepository,
ICategoryRepository categoryRepository,
IUserRepository userRepository,
ILogger<TransactionService> logger) : ITransactionService
{
    public async Task<TransactionServiceDto> AddAsync(AddTransactionServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddTransactionServiceDto, AddTransactionRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddTransactionServiceDto, AddTransactionRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        accountRepository.GetByIdAsync(addRepositoryDto.AccountId),
		addRepositoryDto.CategoryId.HasValue ? categoryRepository.GetByIdAsync(addRepositoryDto.CategoryId.Value) : Task.CompletedTask,
		userRepository.GetByIdAsync(addRepositoryDto.UserId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<TransactionRepositoryDto, TransactionServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<TransactionRepositoryDto, TransactionServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(int id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<TransactionListServiceDto> GetAllAsync(TransactionQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<TransactionQueryServiceDto,TransactionQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<TransactionQueryServiceDto,TransactionQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<TransactionRepositoryDto,TransactionServiceDto>());
        var mapper2 = new Mapper(config2);
        return new TransactionListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<TransactionServiceDto>(x))
        };
    }

    public async Task<TransactionServiceDto> GetByIdAsync(int id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<TransactionRepositoryDto, TransactionServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<TransactionRepositoryDto, TransactionServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateTransactionServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateTransactionServiceDto, UpdateTransactionRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateTransactionServiceDto, UpdateTransactionRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.AccountId.HasValue ? accountRepository.GetByIdAsync(updateDto.AccountId.Value) : Task.CompletedTask,
		updateDto.CategoryId.HasValue ? categoryRepository.GetByIdAsync(updateDto.CategoryId.Value) : Task.CompletedTask,
		updateDto.UserId.HasValue ? userRepository.GetByIdAsync(updateDto.UserId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}