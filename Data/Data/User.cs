using System.ComponentModel.DataAnnotations;
public class User
{
	[Required]
	public long Id { get; set; }
	[StringLength(32)]
	public string Login { get; set; }
	[StringLength(50)]
	public string? Mail { get; set; }
	[StringLength(32)]
	public string PasswordHash { get; set; }
	[StringLength(50)]
	public string Firstname { get; set; }
	[StringLength(50)]
	public string Lastname { get; set; }
	[StringLength(50)]
	public string? Patronymic { get; set; }
	public short? Age { get; set; }
	public short RoleId { get; set; }
	public Role Role { get; set; }
	public ICollection<FamilyMember> FamilyMembers { get; set; }
	public ICollection<Goal> Goals { get; set; }
	public ICollection<Account> Accounts { get; set; }
	public ICollection<Transaction> Transactions { get; set; }
	public ICollection<Budget> Budgets { get; set; }
	public ICollection<Debt> Debts { get;set; }
}