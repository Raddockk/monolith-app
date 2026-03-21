using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Global;
public class AppDbContext : IdentityDbContext<IdentityUser>
{

public DbSet<Account> AccountList { get; set; }

	
public DbSet<Bank> BankList { get; set; }

	
public DbSet<Budget> BudgetList { get; set; }

	
public DbSet<Category> CategoryList { get; set; }

	
public DbSet<Family> FamilyList { get; set; }

	
public DbSet<FamilyMember> FamilyMemberList { get; set; }

	
public DbSet<Goal> GoalList { get; set; }

	
public DbSet<Role> RoleList { get; set; }

	
public DbSet<Transaction> TransactionList { get; set; }

public DbSet<Debt> DebtList { get; set; }

	
public DbSet<User> UserList { get; set; }

    public AppDbContext(){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Читаем переменные окружения
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var database = Environment.GetEnvironmentVariable("DB_NAME");
        var username = Environment.GetEnvironmentVariable("DB_USER");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        // Формируем строку подключения
        var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
        
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AccountMap())
    .ApplyConfiguration(new BankMap())
    .ApplyConfiguration(new BudgetMap())
    .ApplyConfiguration(new CategoryMap())
    .ApplyConfiguration(new FamilyMap())
    .ApplyConfiguration(new FamilyMemberMap())
    .ApplyConfiguration(new GoalMap())
    .ApplyConfiguration(new RoleMap())
    .ApplyConfiguration(new TransactionMap())
    .ApplyConfiguration(new DebtMap())
	.ApplyConfiguration(new UserMap());
        Role r = new Role { 
            
            Id = 1,
            Name = "admin" 
        };
            builder.Entity<Role>().HasData(r);
            builder.Entity<User>().HasData(
                new User {
                    Firstname = "adm",
			        Lastname = "adm",
                    Login = "admin",
                    PasswordHash = Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes("admin"))).ToLower(),
                    Id = 1,
                    RoleId = 1
                });
        base.OnModelCreating(builder);
    }
}