
namespace Global;
public class TransactionServiceDto
{
    public int Id { get; set; }
	public decimal Amount { get; set; }
	public string Type { get; set; }
	public DateTime TransactionDate { get; set; }
	public short AccountId { get; set; }
	public short? CategoryId { get; set; }
	public long UserId { get; set; }
}