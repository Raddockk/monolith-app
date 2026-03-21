
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class DebtService(IDebtRepository repository,
IAccountRepository accountRepository,
ICategoryRepository categoryRepository,
IUserRepository userRepository,
ILogger<DebtService> logger) : IDebtService
{
    public async Task<DebtServiceDto> AddAsync(AddDebtServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddDebtServiceDto, AddDebtRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddDebtServiceDto, AddDebtRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        accountRepository.GetByIdAsync(addRepositoryDto.AccountId),
		addRepositoryDto.CategoryId.HasValue ? categoryRepository.GetByIdAsync(addRepositoryDto.CategoryId.Value) : Task.CompletedTask,
		userRepository.GetByIdAsync(addRepositoryDto.UserId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<DebtRepositoryDto, DebtServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<DebtRepositoryDto, DebtServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<DebtListServiceDto> GetAllAsync(DebtQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<DebtQueryServiceDto,DebtQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<DebtQueryServiceDto,DebtQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<DebtRepositoryDto,DebtServiceDto>());
        var mapper2 = new Mapper(config2);
        return new DebtListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<DebtServiceDto>(x))
        };
    }

    public async Task<DebtServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<DebtRepositoryDto, DebtServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<DebtRepositoryDto, DebtServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateDebtServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateDebtServiceDto, UpdateDebtRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateDebtServiceDto, UpdateDebtRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.AccountId.HasValue ? accountRepository.GetByIdAsync(updateDto.AccountId.Value) : Task.CompletedTask,
		updateDto.CategoryId.HasValue ? categoryRepository.GetByIdAsync(updateDto.CategoryId.Value) : Task.CompletedTask,
		updateDto.UserId.HasValue ? userRepository.GetByIdAsync(updateDto.UserId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}