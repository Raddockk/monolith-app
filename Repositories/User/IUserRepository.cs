
        namespace Global;
        public interface IUserRepository
{
    public Task<UserListRepositoryDto> GetAllAsync(UserQueryRepositoryDto queryDto);

    public Task<UserRepositoryDto> GetByIdAsync(long id);

    public Task<UserRepositoryDto> AddAsync(AddUserRepositoryDto addDto);

    public Task DeleteAsync(long id);

    public Task UpdateAsync(UpdateUserRepositoryDto updateDto);

    public Task<UserLoginResultRepositoryDto> Login(UserLoginRepositoryDto loginDto);
}