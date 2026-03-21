
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Global;
public class UserRepository(AppDbContext db) : IUserRepository
{ 
    DbSet<User> set = db.Set<User>();
    public async Task<UserRepositoryDto> AddAsync(AddUserRepositoryDto addDto)
    {  
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUserRepositoryDto, User>());
        var mapper = new Mapper(config);
        var entity = mapper.Map<AddUserRepositoryDto, User>(addDto);
        await set.AddAsync(entity);
        await db.SaveChangesAsync();
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<User,UserRepositoryDto>());
        var mapper2 = new Mapper(config2);
        var dto = mapper2.Map<User,UserRepositoryDto>(entity);
        return dto;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new EntityNotFoundException<User>(new {id});
        set.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<UserListRepositoryDto> GetAllAsync(UserQueryRepositoryDto queryDto)
{
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserRepositoryDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : "—"));
        });
        var mapper = new Mapper(config);

        var query = set.AsQueryable();

        // Загружаем связанную сущность
        query = query.Include(u => u.Role);

        // Поиск
        if (!string.IsNullOrEmpty(queryDto.Search))
        {
            var search = queryDto.Search.ToLower();
            query = query.Where(u =>
                u.Login.ToLower().Contains(search) ||
                u.Mail.ToLower().Contains(search) ||
                u.Firstname.ToLower().Contains(search) ||
                u.Lastname.ToLower().Contains(search) ||
                u.Patronymic.ToLower().Contains(search)
            );
        }

        // Фильтры
        if (queryDto.RoleId.HasValue)
        {
            query = query.Where(u => u.RoleId == queryDto.RoleId.Value);
        }

        if (queryDto.MinAge.HasValue)
        {
            query = query.Where(u => u.Age >= queryDto.MinAge.Value);
        }

        if (queryDto.MaxAge.HasValue)
        {
            query = query.Where(u => u.Age <= queryDto.MaxAge.Value);
        }

        // Сортировка
        if (!string.IsNullOrEmpty(queryDto.SortBy))
        {
            switch (queryDto.SortBy.ToLower())
            {
                case "login":
                    query = queryDto.IsDescending ? query.OrderByDescending(u => u.Login) : query.OrderBy(u => u.Login);
                    break;
                case "mail":
                    query = queryDto.IsDescending ? query.OrderByDescending(u => u.Mail) : query.OrderBy(u => u.Mail);
                    break;
                case "firstname":
                    query = queryDto.IsDescending ? query.OrderByDescending(u => u.Firstname) : query.OrderBy(u => u.Firstname);
                    break;
                case "lastname":
                    query = queryDto.IsDescending ? query.OrderByDescending(u => u.Lastname) : query.OrderBy(u => u.Lastname);
                    break;
                case "patronymic":
                    query = queryDto.IsDescending ? query.OrderByDescending(u => u.Patronymic) : query.OrderBy(u => u.Patronymic);
                    break;
                case "age":
                    query = queryDto.IsDescending ? query.OrderByDescending(u => u.Age) : query.OrderBy(u => u.Age);
                    break;
                case "roleid":
                    query = queryDto.IsDescending ? query.OrderByDescending(u => u.Role.Name) : query.OrderBy(u => u.Role.Name);
                    break;
            }
        }

        var total = await query.CountAsync();
        var entities = await query
            .Skip(queryDto.Offset)
            .Take(queryDto.Count < 50 ? queryDto.Count : 50)
            .ToListAsync();

        var items = entities.Select(u => mapper.Map<UserRepositoryDto>(u)).ToList();

        return new UserListRepositoryDto()
        {
            Items = items,
            TotalCount = total
        };
    }

    public async Task<UserRepositoryDto> GetByIdAsync(long id)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserRepositoryDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : "—"));
        });
        var mapper = new Mapper(config);

        var entity = await set
            .Include(u => u.Role)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new EntityNotFoundException<User>(new { id });
        return mapper.Map<User, UserRepositoryDto>(entity);
    }

    public async Task UpdateAsync(UpdateUserRepositoryDto updateDto)
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
        if(entity == null) throw new EntityNotFoundException<User>(new {Id = updateDto.Id});
        		if(!String.IsNullOrEmpty(updateDto.Login)){
            entity.Login = updateDto.Login;
        }
		if(!String.IsNullOrEmpty(updateDto.Mail)){
            entity.Mail = updateDto.Mail;
        }
		if(!String.IsNullOrEmpty(updateDto.PasswordHash)){
            entity.PasswordHash = updateDto.PasswordHash;
        }
		if(!String.IsNullOrEmpty(updateDto.Firstname)){
            entity.Firstname = updateDto.Firstname;
        }
		if(!String.IsNullOrEmpty(updateDto.Lastname)){
            entity.Lastname = updateDto.Lastname;
        }
		if(!String.IsNullOrEmpty(updateDto.Patronymic)){
            entity.Patronymic = updateDto.Patronymic;
        }

		if(updateDto.RoleId.HasValue){
            entity.RoleId = updateDto.RoleId.Value;
        }






        await db.SaveChangesAsync();
    }

    public async Task<UserLoginResultRepositoryDto> Login(UserLoginRepositoryDto loginDto)
    {
        var entity = await set.Include(x=>x.Role).FirstOrDefaultAsync(x=>x.Login == loginDto.Login && x.PasswordHash == loginDto.PasswordHash);
        if(entity == null) throw new BadLoginException();
        var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserLoginResultRepositoryDto>());
        var mapper = new Mapper(config);
        var resultDto = mapper.Map<User, UserLoginResultRepositoryDto>(entity);
        resultDto.RoleName = entity.Role.Name;
        return resultDto;
    }
}