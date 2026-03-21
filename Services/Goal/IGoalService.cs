
namespace Global;
public interface IGoalService
{
    public Task<GoalListServiceDto> GetAllAsync(GoalQueryServiceDto queryDto);

    public Task<GoalServiceDto> GetByIdAsync(short id);

    public Task<GoalServiceDto> AddAsync(AddGoalServiceDto addDto);

    public Task DeleteAsync(short id);

    public Task UpdateAsync(UpdateGoalServiceDto updateDto);
}