using System.Data;

namespace Mentorly.Application.Services;

public interface IDapperContext
{
    Task<IDbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
}
