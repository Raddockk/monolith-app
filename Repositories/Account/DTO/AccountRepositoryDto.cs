
namespace Global;
public class AccountRepositoryDto
{
    public short Id { get; set; }
	public string Name { get; set; }
	public decimal Balance { get; set; }
	public string Currency { get; set; }
	public short? BankId { get; set; }
	public long UserId { get; set; }
}