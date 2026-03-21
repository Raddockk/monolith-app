
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class FamilyService(IFamilyRepository repository,

ILogger<FamilyService> logger) : IFamilyService
{
    public async Task<FamilyServiceDto> AddAsync(AddFamilyServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddFamilyServiceDto, AddFamilyRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddFamilyServiceDto, AddFamilyRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FamilyRepositoryDto, FamilyServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<FamilyRepositoryDto, FamilyServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(long id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<FamilyListServiceDto> GetAllAsync(FamilyQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FamilyQueryServiceDto,FamilyQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<FamilyQueryServiceDto,FamilyQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FamilyRepositoryDto,FamilyServiceDto>());
        var mapper2 = new Mapper(config2);
        return new FamilyListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<FamilyServiceDto>(x))
        };
    }

    public async Task<FamilyServiceDto> GetByIdAsync(long id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FamilyRepositoryDto, FamilyServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<FamilyRepositoryDto, FamilyServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateFamilyServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateFamilyServiceDto, UpdateFamilyRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateFamilyServiceDto, UpdateFamilyRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}