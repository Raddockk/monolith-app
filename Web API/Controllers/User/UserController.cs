
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Global;
[Authorize(Roles="admin")]
[ApiController]
[Route("User")]
public class UserController(IUserService service)
{
    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IResult> Add(AddUserControllerDto addDto)
    {
        try
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUserControllerDto, AddUserServiceDto>());
            var mapper = new Mapper(config);
            var addServiceDto = mapper.Map<AddUserControllerDto, AddUserServiceDto>(addDto);
            var result = await service.AddAsync(addServiceDto);
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<UserServiceDto, UserControllerDto>());
            var mapper2 = new Mapper(config2);
            return Results.Json(mapper2.Map<UserServiceDto, UserControllerDto>(result));
        }
        catch (EntityNotFoundException ex)
        {
            return Results.BadRequest(new {error = ex.Message});
        }
        catch (DbUpdateException ex)
        when ((ex.InnerException as Npgsql.PostgresException).SqlState == "23505")
        {
            var innerEx = (ex.InnerException as Npgsql.PostgresException);
            return Results.BadRequest(new { error = $"Нарушение уникальности поля {innerEx.ConstraintName.Split("_").LastOrDefault()}" });
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(new {error = ex.Message});
        }
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IResult> Delete(long id)
    {
        try
        {
            await service.DeleteAsync(id);
            return Results.Ok();
        }
        catch (EntityNotFoundException ex)
        {
            return Results.BadRequest(new {error = ex.Message});
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(new {error = ex.Message});
        }
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IResult> GetAll([FromQuery]UserQueryControllerDto queryDto)
    {
        try
        {
            Console.WriteLine($"Search query: {queryDto.Search ?? "null"}");
            
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserQueryControllerDto,UserQueryServiceDto>());
            var mapper = new Mapper(config);
            var dto = mapper.Map<UserQueryControllerDto,UserQueryServiceDto>(queryDto);

            Console.WriteLine($"Mapped to service: {dto.Search ?? "null"}");
            
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<UserServiceDto,UserControllerDto>());
            var mapper2 = new Mapper(config2);

            var serviceResult = await service.GetAllAsync(dto);

            // return Results.Json(new UserListControllerDto(){
            //     Items = (await service.GetAllAsync(dto)).Items.Select(x=>mapper2.Map<UserServiceDto,UserControllerDto>(x))
            // });

                return Results.Json(new UserListControllerDto(){
                    Items = serviceResult.Items.Select(x => mapper2.Map<UserServiceDto, UserControllerDto>(x)),
                    TotalCount = serviceResult.TotalCount 
                });
        }
        catch (EntityNotFoundException ex)
        {
            return Results.BadRequest(new {error = ex.Message});
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(new {error = ex.Message});
        }
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IResult> GetById(long id)
    {
        try
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserServiceDto, UserControllerDto>());
            var mapper = new Mapper(config);
            return Results.Json(mapper.Map<UserServiceDto, UserControllerDto>(await service.GetByIdAsync(id)));
        }
        catch (EntityNotFoundException ex)
        {
            return Results.BadRequest(new {error = ex.Message});
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(new {error = ex.Message});
        }
    }
    [HttpPatch]
    [Route("update")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IResult> UpdateAsync(UpdateUserControllerDto updateDto)
    {
        try
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateUserControllerDto, UpdateUserServiceDto>());
            var mapper = new Mapper(config);
            var updateServiceDto = mapper.Map<UpdateUserControllerDto, UpdateUserServiceDto>(updateDto);
            await service.UpdateAsync(updateServiceDto);
            return Results.Ok();
        }
        catch (EntityNotFoundException ex)
        {
            return Results.BadRequest(new {error = ex.Message});
        }
        catch (DbUpdateException ex)
        when ((ex.InnerException as Npgsql.PostgresException).SqlState == "23505")
        {
            var innerEx = (ex.InnerException as Npgsql.PostgresException);
            return Results.BadRequest(new { error = $"Нарушение уникальности поля {innerEx.ConstraintName.Split("_").LastOrDefault()}" });
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(new {error = ex.Message});
        }
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [AllowAnonymous]
    public async Task<IResult> Login(UserLoginControllerDto loginDto)
    {
        try
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserLoginControllerDto, UserLoginServiceDto>());
            var mapper = new Mapper(config);
            var userLoginServiceDto = mapper.Map<UserLoginControllerDto, UserLoginServiceDto>(loginDto);
            
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<UserLoginResultServiceDto, UserLoginResultControllerDto>());
            var mapper2 = new Mapper(config2);
            return Results.Json(mapper2.Map<UserLoginResultServiceDto, UserLoginResultControllerDto>(await service.Login(userLoginServiceDto)));
            
        }
        catch (BadLoginException ex)
        {
            return Results.NotFound(new {error = ex.Message});
        }
        catch (DbUpdateException ex)
        when ((ex.InnerException as Npgsql.PostgresException).SqlState == "23505")
        {
            var innerEx = (ex.InnerException as Npgsql.PostgresException);
            return Results.BadRequest(new { error = $"Нарушение уникальности поля {innerEx.ConstraintName.Split("_").LastOrDefault()}" });
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(new {error = ex.Message});
        }
    }
}
