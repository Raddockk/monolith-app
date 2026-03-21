
namespace Global;
public class GoalRepositoryDto
{
    public short Id { get; set; }
    public string? Name { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public long FamilyId { get; set; }
    public string? FamilyName { get; set; }     
    public long CreatedById { get; set; }
    public string? CreatedByLogin { get; set; }  
}