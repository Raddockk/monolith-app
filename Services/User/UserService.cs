
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.Extensions.Logging;
namespace Global;
public class UserService(IUserRepository repository,
IRoleRepository roleRepository, ILogger<UserService> logger) : IUserService
{
    public async Task<UserServiceDto> AddAsync(AddUserServiceDto addServiceDto)
    {
        logger.Log(LogLevel.Debug,"Add()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUserServiceDto, AddUserRepositoryDto>());
        var mapper = new Mapper(config);
        var addRepositoryDto = mapper.Map<AddUserServiceDto, AddUserRepositoryDto>(addServiceDto);
        await Task.WhenAll(
        roleRepository.GetByIdAsync(addRepositoryDto.RoleId));
        var passwordHash = Convert.ToHexString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(addServiceDto.Password))).ToLower();
        addRepositoryDto.PasswordHash = passwordHash;
        var entityRepositoryDto = await repository.AddAsync(addRepositoryDto);
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<UserRepositoryDto, UserServiceDto>());
        var mapper2 = new Mapper(config2);
        return mapper2.Map<UserRepositoryDto, UserServiceDto>(entityRepositoryDto);
    }

    public async Task DeleteAsync(long id)
    {
        logger.Log(LogLevel.Debug,"Delete()");
        await repository.DeleteAsync(id);
    }

    public async Task<UserListServiceDto> GetAllAsync(UserQueryServiceDto queryDto)
    {
        logger.Log(LogLevel.Debug,"GetAll()");

        Console.WriteLine($"Service received Search: {queryDto.Search ?? "null"}");

        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserQueryServiceDto,UserQueryRepositoryDto>());
        var mapper = new Mapper(config);
        var dto = mapper.Map<UserQueryServiceDto,UserQueryRepositoryDto>(queryDto);   

        Console.WriteLine($"Mapped to repository: {dto.Search ?? "null"}");
         
        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<UserRepositoryDto,UserServiceDto>());
        var mapper2 = new Mapper(config2);

        var result = await repository.GetAllAsync(dto);
        // return new UserListServiceDto(){
        //     Items = (await repository.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<UserServiceDto>(x)),
        //     TotalCount = result.TotalCount
        // };     
            return new UserListServiceDto(){
                Items = result.Items.Select(x=>mapper2.Map<UserServiceDto>(x)),
                TotalCount = result.TotalCount
            };      
    }

    public async Task<UserServiceDto> GetByIdAsync(long id)
    {
        logger.Log(LogLevel.Debug,"GetById()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserRepositoryDto, UserServiceDto>());
        var mapper = new Mapper(config);
        return mapper.Map<UserRepositoryDto, UserServiceDto>(await repository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(UpdateUserServiceDto updateDto)
    {
        logger.Log(LogLevel.Debug,"Update()");
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateUserServiceDto, UpdateUserRepositoryDto>());
        var mapper = new Mapper(config);
        var updateRepositoryDto = mapper.Map<UpdateUserServiceDto, UpdateUserRepositoryDto>(updateDto);
        await Task.WhenAll(
        updateDto.RoleId.HasValue ? roleRepository.GetByIdAsync(updateDto.RoleId.Value) : Task.CompletedTask);
        await repository.UpdateAsync(updateRepositoryDto);
    }
    
    public async Task<UserLoginResultServiceDto> Login(UserLoginServiceDto loginDto)
    {
        logger.Log(LogLevel.Debug,"Login()");
        var passwordHash = Convert.ToHexString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password))).ToLower();
        var credentials = await repository.Login(new UserLoginRepositoryDto(){
            Login = loginDto.Login,
            PasswordHash = passwordHash
        });
        var claims = new List<Claim> {
                new Claim("id", credentials.Id.ToString()),
                new Claim(ClaimTypes.Name, credentials.Login),
                new Claim(ClaimTypes.Role, credentials.RoleName)
                };
        var jwt = new JwtSecurityToken(
                issuer: AppConfig.ISSUER,
                audience: AppConfig.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(90)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.KEY)), SecurityAlgorithms.HmacSha256));

        return new UserLoginResultServiceDto(){
            Token = new JwtSecurityTokenHandler().WriteToken(jwt)
        };
    }
}