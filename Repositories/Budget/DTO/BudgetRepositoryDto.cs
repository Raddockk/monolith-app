
namespace Global;
public class BudgetRepositoryDto
{
    public short Id { get; set; }
    public decimal BudgetAmount { get; set; }
    public string? Period { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long FamilyId { get; set; }
    public string? FamilyName { get; set; }     
    public short? CategoryId { get; set; }
    public string? CategoryName { get; set; }   
    public long CreatedById { get; set; }
    public string? CreatedByLogin { get; set; } 
}