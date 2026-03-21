
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class GoalRepository(AppDbContext db) : IGoalRepository
{ 
    DbSet<Goal> set = db.Set<Goal>();
    public async Task<GoalRepositoryDto> AddAsync(AddGoalRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddGoalRepositoryDto, Goal>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddGoalRepositoryDto, Goal>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Goal,GoalRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Goal,GoalRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<Goal>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<GoalListRepositoryDto> GetAllAsync(GoalQueryRepositoryDto queryDto)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Goal, GoalRepositoryDto>()
           .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family != null ? src.Family.Name : "—"))
           .ForMember(dest => dest.CreatedByLogin, opt => opt.MapFrom(src => src.User != null ? src.User.Login : "—"));
    });
    var mapper = new Mapper(config);

    var query = set.AsQueryable();

    // Загружаем связанные сущности
    query = query.Include(g => g.Family)
                 .Include(g => g.User);

    // Поиск
    if (!string.IsNullOrEmpty(queryDto.Search))
    {
        var search = queryDto.Search.ToLower();
        query = query.Where(g =>
            g.Name.ToLower().Contains(search)
        );
    }

    // Фильтры
    if (queryDto.FamilyId.HasValue)
    {
        query = query.Where(g => g.FamilyId == queryDto.FamilyId.Value);
    }

    if (queryDto.CreatedById.HasValue)
    {
        query = query.Where(g => g.CreatedById == queryDto.CreatedById.Value);
    }

    if (queryDto.MinAmount.HasValue)
    {
        query = query.Where(g => g.TargetAmount >= queryDto.MinAmount.Value);
    }

    if (queryDto.MaxAmount.HasValue)
    {
        query = query.Where(g => g.TargetAmount <= queryDto.MaxAmount.Value);
    }

    // Сортировка
    if (!string.IsNullOrEmpty(queryDto.SortBy))
    {
        switch (queryDto.SortBy.ToLower())
        {
            case "name":
                query = queryDto.IsDescending ? query.OrderByDescending(g => g.Name) : query.OrderBy(g => g.Name);
                break;
            case "targetamount":
                query = queryDto.IsDescending ? query.OrderByDescending(g => g.TargetAmount) : query.OrderBy(g => g.TargetAmount);
                break;
            case "currentamount":
                query = queryDto.IsDescending ? query.OrderByDescending(g => g.CurrentAmount) : query.OrderBy(g => g.CurrentAmount);
                break;
            case "familyid":
                query = queryDto.IsDescending ? query.OrderByDescending(g => g.Family.Name) : query.OrderBy(g => g.Family.Name);
                break;
            case "createdbyid":
                query = queryDto.IsDescending ? query.OrderByDescending(g => g.User.Login) : query.OrderBy(g => g.User.Login);
                break;
        }
    }

    var total = await query.CountAsync();
    var entities = await query
        .Skip(queryDto.Offset)
        .Take(queryDto.Count < 50 ? queryDto.Count : 50)
        .ToListAsync();

    var items = entities.Select(g => mapper.Map<GoalRepositoryDto>(g)).ToList();

    return new GoalListRepositoryDto()
    {
        Items = items,
        TotalCount = total
    };
}

    public async Task<GoalRepositoryDto> GetByIdAsync(short id)
{
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Goal, GoalRepositoryDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family != null ? src.Family.Name : "—"))
            .ForMember(dest => dest.CreatedByLogin, opt => opt.MapFrom(src => src.User != null ? src.User.Login : "—"));
        });
        var mapper = new Mapper(config);

        var entity = await set
            .Include(g => g.Family)
            .Include(g => g.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new EntityNotFoundException<Goal>(new { id });
        return mapper.Map<Goal, GoalRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateGoalRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<Goal>(new {Id = updateDto.Id});
		if(!String.IsNullOrEmpty(updateDto.Name)){
            entity.Name = updateDto.Name;
        }
		if(updateDto.TargetAmount.HasValue){
            entity.TargetAmount = updateDto.TargetAmount.Value;
        }
		if(updateDto.CurrentAmount.HasValue){
            entity.CurrentAmount = updateDto.CurrentAmount.Value;
        }
		if(updateDto.FamilyId.HasValue){
            entity.FamilyId = updateDto.FamilyId.Value;
        }

		if(updateDto.CreatedById.HasValue){
            entity.CreatedById = updateDto.CreatedById.Value;
        }

        await db.SaveChangesAsync();
    }
}