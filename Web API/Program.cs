
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Global;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin() // Укажи нужные URL фронтенда
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

builder.Services
.AddDbContext<AppDbContext>(ServiceLifetime.Transient)
.AddScoped<IAccountService,AccountService>()
.AddTransient<IAccountRepository, AccountRepository>()
.AddScoped<IBankService,BankService>()
.AddTransient<IBankRepository, BankRepository>()
.AddScoped<IBudgetService,BudgetService>()
.AddTransient<IBudgetRepository, BudgetRepository>()
.AddScoped<IDebtService,DebtService>()
.AddTransient<IDebtRepository, DebtRepository>()
.AddScoped<ICategoryService,CategoryService>()
.AddTransient<ICategoryRepository, CategoryRepository>()
.AddScoped<IFamilyService,FamilyService>()
.AddTransient<IFamilyRepository, FamilyRepository>()
.AddScoped<IFamilyMemberService,FamilyMemberService>()
.AddTransient<IFamilyMemberRepository, FamilyMemberRepository>()
.AddScoped<IGoalService,GoalService>()
.AddTransient<IGoalRepository, GoalRepository>()
.AddScoped<IRoleService,RoleService>()
.AddTransient<IRoleRepository, RoleRepository>()
.AddScoped<ITransactionService,TransactionService>()
.AddTransient<ITransactionRepository, TransactionRepository>()
.AddScoped<IUserService,UserService>()
.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = AppConfig.ISSUER,
            ValidAudience = AppConfig.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.KEY)),
            ValidateIssuerSigningKey = true
        };
});
builder.Services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace);
});
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.ConfigureHttpJsonOptions(opts =>{
    opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();