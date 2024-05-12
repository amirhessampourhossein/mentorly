using Mentorly.Application.Services;
using Mentorly.Persistence.Queries.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mentorly.Persistence.Queries;

public class DapperContext(DapperContextOptions options) : IDapperContext
{
    public async Task<IDbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(options.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}
