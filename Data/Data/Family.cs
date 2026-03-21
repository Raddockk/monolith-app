using System.ComponentModel.DataAnnotations;
public class Family{
    [Required]
	public long Id { get;set; }
	[StringLength(30)]
	public string Name { get;set; }
	public ICollection<FamilyMember> FamilyMembers { get;set; }
	public ICollection<Goal> Goals { get;set; }
	public ICollection<Budget> Budgets { get;set; }
}