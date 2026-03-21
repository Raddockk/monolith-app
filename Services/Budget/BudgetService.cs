
using AutoMapper;
namespace Global;
using Microsoft.Extensions.Logging;
public class BudgetService(IBudgetRepository repository,
IFamilyRepository familyRepository,
ICategoryRepository categoryRepository,
IUserRepository userRepository,
ILogger<BudgetService> logger) : IBudgetService
{
    public async Task<BudgetServiceDto> AddAsync(AddBudgetServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddBudgetServiceDto, AddBudgetRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddBudgetServiceDto, AddBudgetRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        familyRepository.GetByIdAsync(addRepositoryDto.FamilyId),
		addRepositoryDto.CategoryId.HasValue ? categoryRepository.GetByIdAsync(addRepositoryDto.CategoryId.Value) : Task.CompletedTask,
		userRepository.GetByIdAsync(addRepositoryDto.CreatedById));
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BudgetRepositoryDto, BudgetServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<BudgetRepositoryDto, BudgetServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(short id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<BudgetListServiceDto> GetAllAsync(BudgetQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<BudgetQueryServiceDto,BudgetQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<BudgetQueryServiceDto,BudgetQueryRepositoryDto>(queryDto);    
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BudgetRepositoryDto,BudgetServiceDto>());
        var mapper2 = new Mapper(config2);

        var result = await repository.GetAllAsync(dto);
        // return new BudgetListServiceDto(){
        //     Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<BudgetServiceDto>(x))
        // };

            return new BudgetListServiceDto(){
                    Items = result.Items.Select(x=>mapper2.Map<BudgetServiceDto>(x)),
                    TotalCount = result.TotalCount
            };
    }

    public async Task<BudgetServiceDto> GetByIdAsync(short id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<BudgetRepositoryDto, BudgetServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<BudgetRepositoryDto, BudgetServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateBudgetServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateBudgetServiceDto, UpdateBudgetRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateBudgetServiceDto, UpdateBudgetRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.FamilyId.HasValue ? familyRepository.GetByIdAsync(updateDto.FamilyId.Value) : Task.CompletedTask,
		updateDto.CategoryId.HasValue ? categoryRepository.GetByIdAsync(updateDto.CategoryId.Value) : Task.CompletedTask,
		updateDto.CreatedById.HasValue ? userRepository.GetByIdAsync(updateDto.CreatedById.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
}