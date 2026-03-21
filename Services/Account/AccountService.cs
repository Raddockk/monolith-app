
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class AccountService(IAccountRepository repository,
IBankRepository bankRepository,
IUserRepository userRepository,
ILogger<AccountService> logger) : IAccountService
{
    public async Task<AccountServiceDto> AddAsync(AddAccountServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddAccountServiceDto, AddAccountRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddAccountServiceDto, AddAccountRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        addRepositoryDto.BankId.HasValue ? bankRepository.GetByIdAsync(addRepositoryDto.BankId.Value) : Task.CompletedTask,
		userRepository.GetByIdAsync(addRepositoryDto.UserId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<AccountRepositoryDto, AccountServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<AccountRepositoryDto, AccountServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<AccountListServiceDto> GetAllAsync(AccountQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AccountQueryServiceDto,AccountQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<AccountQueryServiceDto,AccountQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<AccountRepositoryDto,AccountServiceDto>());
        var mapper2 = new Mapper(config2);
        return new AccountListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<AccountServiceDto>(x))
        };
    }

    public async Task<AccountServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AccountRepositoryDto, AccountServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<AccountRepositoryDto, AccountServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateAccountServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateAccountServiceDto, UpdateAccountRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateAccountServiceDto, UpdateAccountRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.BankId.HasValue ? bankRepository.GetByIdAsync(updateDto.BankId.Value) : Task.CompletedTask,
		updateDto.UserId.HasValue ? userRepository.GetByIdAsync(updateDto.UserId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}