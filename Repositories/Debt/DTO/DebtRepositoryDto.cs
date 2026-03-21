
namespace Global;
public class DebtRepositoryDto
{
    public long Id { get; set; }
	public string Name { get; set; }	
	public decimal Amount { get; set; }	
	public string Currency { get;set; }
	public DateTime DueDate { get;set; }
	public short AccountId { get;set; }
	public short? CategoryId { get;set; }
	public long UserId { get;set; }
}