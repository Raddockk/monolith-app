using System.ComponentModel.DataAnnotations;
public class Goal{
    [Required]
	public short Id { get;set; }
	[StringLength(50)]
	public string Name { get;set; }
	public decimal TargetAmount { get;set; }
	public decimal CurrentAmount { get;set; }
	public long FamilyId { get;set; }
	public Family Family {get;set;}
	public long CreatedById { get;set; }
	public User User {get;set;}
}