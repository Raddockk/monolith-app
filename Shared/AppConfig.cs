namespace Global;
public class AppConfig
{
    public readonly static string KEY = Environment.GetEnvironmentVariable("JWT_KEY");
    public readonly static string ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER");
    public readonly static string AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
}