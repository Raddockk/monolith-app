using System.ComponentModel.DataAnnotations;
public class FamilyMember{
    [Required]
	public long Id { get;set; }
	public long UserId { get;set; }
	public User User {get;set;}
	public long FamilyId { get;set; }
	public Family Family {get;set;}
	public DateTime JoinDate { get;set; }
}