using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;

public class BudgetRepository(AppDbContext db) : IBudgetRepository
{
    DbSet<Budget> set = db.Set<Budget>();

    public async Task<BudgetRepositoryDto> AddAsync(AddBudgetRepositoryDto addDto)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddBudgetRepositoryDto, Budget>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddBudgetRepositoryDto, Budget>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();

        // Возвращаем DTO с именами
        var config2 = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Budget, BudgetRepositoryDto>()
               .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family != null ? src.Family.Name : "—"))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "—"))
               .ForMember(dest => dest.CreatedByLogin, opt => opt.MapFrom(src => src.User != null ? src.User.Login : "—"));
        });
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<Budget, BudgetRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(short id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) throw new EntityNotFoundException<Budget>(new { id });
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<BudgetListRepositoryDto> GetAllAsync(BudgetQueryRepositoryDto queryDto)
{
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Budget, BudgetRepositoryDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family != null ? src.Family.Name : "—"))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "—"))
            .ForMember(dest => dest.CreatedByLogin, opt => opt.MapFrom(src => src.User != null ? src.User.Login : "—"));
        });
        var mapper = new Mapper(config);

        var query = set.AsQueryable();

        // Загружаем связанные сущности
        query = query.Include(b => b.Family)
                    .Include(b => b.Category)
                    .Include(b => b.User);

        // Поиск
        if (!string.IsNullOrEmpty(queryDto.Search))
        {
            var search = queryDto.Search.ToLower();
            query = query.Where(b =>
                b.Period.ToLower().Contains(search)
            );
        }

        // Фильтры
        if (queryDto.FamilyId.HasValue)
        {
            query = query.Where(b => b.FamilyId == queryDto.FamilyId.Value);
        }

        if (queryDto.CreatedById.HasValue)
        {
            query = query.Where(b => b.CreatedById == queryDto.CreatedById.Value);
        }

        if (queryDto.CategoryId.HasValue)
        {
            query = query.Where(b => b.CategoryId == queryDto.CategoryId.Value);
        }

        if (queryDto.MinAmount.HasValue)
        {
            query = query.Where(b => b.BudgetAmount >= queryDto.MinAmount.Value);
        }

        if (queryDto.MaxAmount.HasValue)
        {
            query = query.Where(b => b.BudgetAmount <= queryDto.MaxAmount.Value);
        }

        // Сортировка
        if (!string.IsNullOrEmpty(queryDto.SortBy))
        {
            switch (queryDto.SortBy.ToLower())
            {
                case "budgetamount":
                    query = queryDto.IsDescending ? query.OrderByDescending(b => b.BudgetAmount) : query.OrderBy(b => b.BudgetAmount);
                    break;
                case "period":
                    query = queryDto.IsDescending ? query.OrderByDescending(b => b.Period) : query.OrderBy(b => b.Period);
                    break;
                case "startdate":
                    query = queryDto.IsDescending ? query.OrderByDescending(b => b.StartDate) : query.OrderBy(b => b.StartDate);
                    break;
                case "enddate":
                    query = queryDto.IsDescending ? query.OrderByDescending(b => b.EndDate) : query.OrderBy(b => b.EndDate);
                    break;
                case "familyid":
                    query = queryDto.IsDescending ? query.OrderByDescending(b => b.Family.Name) : query.OrderBy(b => b.Family.Name);
                    break;
                case "categoryid":
                    query = queryDto.IsDescending ? query.OrderByDescending(b => b.Category.Name) : query.OrderBy(b => b.Category.Name);
                    break;
                case "createdbyid":
                    query = queryDto.IsDescending ? query.OrderByDescending(b => b.User.Login) : query.OrderBy(b => b.User.Login);
                    break;
            }
        }

        var total = await query.CountAsync();
        var entities = await query
            .Skip(queryDto.Offset)
            .Take(queryDto.Count < 50 ? queryDto.Count : 50)
            .ToListAsync();

        foreach (var b in entities)
        {
            Console.WriteLine($"Budget ID: {b.Id}, Family: {b.Family?.Name}, Category: {b.Category?.Name}, User: {b.User?.Login}");
        }

        // Маппим в DTO
        var items = entities.Select(b => mapper.Map<BudgetRepositoryDto>(b)).ToList();

        return new BudgetListRepositoryDto()
        {
            Items = items,
            TotalCount = total
        };
}

    public async Task<BudgetRepositoryDto> GetByIdAsync(short id)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Budget, BudgetRepositoryDto>()
               .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family != null ? src.Family.Name : "—"))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "—"))
               .ForMember(dest => dest.CreatedByLogin, opt => opt.MapFrom(src => src.User != null ? src.User.Login : "—"));
        });
        var mapper = new Mapper(config);

        var entity = await set
            .Include(b => b.Family)
            .Include(b => b.Category)
            .Include(b => b.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new EntityNotFoundException<Budget>(new { id });
        return mapper.Map<Budget, BudgetRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateBudgetRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if (entity == null) throw new EntityNotFoundException<Budget>(new { Id = updateDto.Id });

        if (updateDto.BudgetAmount.HasValue) entity.BudgetAmount = updateDto.BudgetAmount.Value;
        if (!String.IsNullOrEmpty(updateDto.Period)) entity.Period = updateDto.Period;
        if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
        if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
        if (updateDto.FamilyId.HasValue) entity.FamilyId = updateDto.FamilyId.Value;
        if (updateDto.CategoryId.HasValue) entity.CategoryId = updateDto.CategoryId.Value;
        if (updateDto.CreatedById.HasValue) entity.CreatedById = updateDto.CreatedById.Value;

        await db.SaveChangesAsync();
    }
}