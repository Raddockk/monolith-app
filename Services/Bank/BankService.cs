
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class BankService(IBankRepository repository,

ILogger<BankService> logger) : IBankService
{
    public async Task<BankServiceDto> AddAsync(AddBankServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddBankServiceDto, AddBankRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddBankServiceDto, AddBankRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BankRepositoryDto, BankServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<BankRepositoryDto, BankServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<BankListServiceDto> GetAllAsync(BankQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<BankQueryServiceDto,BankQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<BankQueryServiceDto,BankQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BankRepositoryDto,BankServiceDto>());
        var mapper2 = new Mapper(config2);
        return new BankListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<BankServiceDto>(x))
        };
    }

    public async Task<BankServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<BankRepositoryDto, BankServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<BankRepositoryDto, BankServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateBankServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateBankServiceDto, UpdateBankRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateBankServiceDto, UpdateBankRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}