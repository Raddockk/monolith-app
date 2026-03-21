
namespace Global;
public interface IGoalRepository
{
    public Task<GoalListRepositoryDto> GetAllAsync(GoalQueryRepositoryDto queryDto);

    public Task<GoalRepositoryDto> GetByIdAsync(short id);

    public Task<GoalRepositoryDto> AddAsync(AddGoalRepositoryDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateGoalRepositoryDto updateDto);
}