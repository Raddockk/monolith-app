
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class RoleService(IRoleRepository repository,

ILogger<RoleService> logger) : IRoleService
{
    public async Task<RoleServiceDto> AddAsync(AddRoleServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddRoleServiceDto, AddRoleRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddRoleServiceDto, AddRoleRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        );
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<RoleRepositoryDto, RoleServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<RoleRepositoryDto, RoleServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<RoleListServiceDto> GetAllAsync(RoleQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<RoleQueryServiceDto,RoleQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<RoleQueryServiceDto,RoleQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<RoleRepositoryDto,RoleServiceDto>());
        var mapper2 = new Mapper(config2);
        return new RoleListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<RoleServiceDto>(x))
        };
    }

    public async Task<RoleServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<RoleRepositoryDto, RoleServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<RoleRepositoryDto, RoleServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateRoleServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateRoleServiceDto, UpdateRoleRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateRoleServiceDto, UpdateRoleRepositoryDto>(updateDto);
        await Task.WhenAll(
        );
        await repository.UpdateAsync(updateRepositoryDto);
    }
}