
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class GoalService(IGoalRepository repository,
IFamilyRepository familyRepository,
IUserRepository userRepository,
ILogger<GoalService> logger) : IGoalService
{
    public async Task<GoalServiceDto> AddAsync(AddGoalServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddGoalServiceDto, AddGoalRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddGoalServiceDto, AddGoalRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        familyRepository.GetByIdAsync(addRepositoryDto.FamilyId),
		userRepository.GetByIdAsync(addRepositoryDto.CreatedById));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<GoalRepositoryDto, GoalServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<GoalRepositoryDto, GoalServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<GoalListServiceDto> GetAllAsync(GoalQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<GoalQueryServiceDto,GoalQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<GoalQueryServiceDto,GoalQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<GoalRepositoryDto,GoalServiceDto>());
        var mapper2 = new Mapper(config2);

        var result = await repository.GetAllAsync(dto);
        // return new GoalListServiceDto(){
        //     Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<GoalServiceDto>(x))
        // };

            return new GoalListServiceDto(){
                    Items = result.Items.Select(x=>mapper2.Map<GoalServiceDto>(x)),
                    TotalCount = result.TotalCount
            }; 
    }

    public async Task<GoalServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<GoalRepositoryDto, GoalServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<GoalRepositoryDto, GoalServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateGoalServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateGoalServiceDto, UpdateGoalRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateGoalServiceDto, UpdateGoalRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.FamilyId.HasValue ? familyRepository.GetByIdAsync(updateDto.FamilyId.Value) : Task.CompletedTask,
		updateDto.CreatedById.HasValue ? userRepository.GetByIdAsync(updateDto.CreatedById.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}