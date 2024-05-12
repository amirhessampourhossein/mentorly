namespace Mentorly.Persistence.Queries.Configuration;

public class DapperContextOptions
{
    public string? ConnectionString { get; set; }

    public void UseSqlServer(string? connectionString)
        => ConnectionString = connectionString;
}
