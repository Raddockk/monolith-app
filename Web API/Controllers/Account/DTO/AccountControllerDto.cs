
namespace Global;
public class AccountControllerDto
{
    public short Id { get; set; }
	public string Name { get; set; }
	public decimal Balance { get; set; }
	public string Currency { get; set; }
	public short? BankId { get; set; }
	public long UserId { get; set; }
}