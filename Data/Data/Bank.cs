using System.ComponentModel.DataAnnotations;
public class Bank{
    [Required]
	public short Id { get;set; }
	[StringLength(30)]
	public string Name { get;set; }
	public ICollection<Account> Accounts { get;set; }
}