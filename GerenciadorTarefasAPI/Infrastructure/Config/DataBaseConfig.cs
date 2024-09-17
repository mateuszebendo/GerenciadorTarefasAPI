namespace Infrastructure.Config;

public static class DataBaseConfig
{
    public static string ConnectionString { get; private set; }

    public static void Initialize(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("SqlConnection");
    }
}