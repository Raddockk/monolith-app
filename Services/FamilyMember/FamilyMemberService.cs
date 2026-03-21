
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class FamilyMemberService(IFamilyMemberRepository repository,
IUserRepository userRepository,
IFamilyRepository familyRepository,
ILogger<FamilyMemberService> logger) : IFamilyMemberService
{
    public async Task<FamilyMemberServiceDto> AddAsync(AddFamilyMemberServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddFamilyMemberServiceDto, AddFamilyMemberRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddFamilyMemberServiceDto, AddFamilyMemberRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        userRepository.GetByIdAsync(addRepositoryDto.UserId),
		familyRepository.GetByIdAsync(addRepositoryDto.FamilyId));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FamilyMemberRepositoryDto, FamilyMemberServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<FamilyMemberRepositoryDto, FamilyMemberServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(long id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<FamilyMemberListServiceDto> GetAllAsync(FamilyMemberQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FamilyMemberQueryServiceDto,FamilyMemberQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<FamilyMemberQueryServiceDto,FamilyMemberQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<FamilyMemberRepositoryDto,FamilyMemberServiceDto>());
        var mapper2 = new Mapper(config2);
        return new FamilyMemberListServiceDto(){
            Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<FamilyMemberServiceDto>(x))
        };
    }

    public async Task<FamilyMemberServiceDto> GetByIdAsync(long id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FamilyMemberRepositoryDto, FamilyMemberServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<FamilyMemberRepositoryDto, FamilyMemberServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateFamilyMemberServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateFamilyMemberServiceDto, UpdateFamilyMemberRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateFamilyMemberServiceDto, UpdateFamilyMemberRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.UserId.HasValue ? userRepository.GetByIdAsync(updateDto.UserId.Value) : Task.CompletedTask,
		updateDto.FamilyId.HasValue ? familyRepository.GetByIdAsync(updateDto.FamilyId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}