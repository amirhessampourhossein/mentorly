using Dapper;
using Mentorly.Application.Services;
using Mentorly.Domain.Users;
using Mentorly.Persistence.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;

namespace Mentorly.Infrastructure.Services;

public class UserService(
    ApplicationDbContext _dbContext,
    IDapperContext _dapperContext)
    : IUserService
{
    public async Task<User> AddAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        var addedUser = await _dbContext.AddAsync(user, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return addedUser.Entity;
    }

    public async Task<string> GetOrCreateInvitationAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(userId, cancellationToken);

        if (!string.IsNullOrEmpty(user!.InvitationCode))
            return user.InvitationCode;

        var userIdHash = SHA256.HashData(Encoding.UTF8.GetBytes(userId.ToString()));

        var invitationCode = BitConverter
            .ToString(userIdHash)
            .ToLower()
            .Replace("-", string.Empty)[..8];

        user.InvitationCode = invitationCode;

        await UpdateAsync(user, cancellationToken);

        return invitationCode;
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        var user = await connection.QuerySingleOrDefaultAsync<User>(
            $"SELECT * FROM Users WHERE IsDeleted = 0 AND Email = @Email",
            new { Email = email });

        return user;
    }

    public async Task<User?> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        var user = await connection
            .QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id",
            new { Id = userId });

        return user;
    }

    public async Task<Guid> GetIdFromInvitationAsync(
        string invitationCode,
        CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        var userId = await connection.QuerySingleOrDefaultAsync<Guid>(
            "SELECT Id FROM Users WHERE InvitationCode = @InvitationCode",
            new { InvitationCode = invitationCode });

        return userId;
    }

    public async Task<bool> IsEmailRegisteredAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        var userId = await connection.ExecuteScalarAsync<Guid>(
            $"SELECT Id FROM Users WHERE IsDeleted = 0 AND Email = @Email",
            new { Email = email });

        return userId != Guid.Empty;
    }

    public async Task<bool> IsMobileRegisteredAsync(
        string mobile,
        CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        var userId = await connection.ExecuteScalarAsync<Guid>(
            $"SELECT Id FROM Users WHERE IsDeleted = 0 AND Mobile = @Mobile",
            new { Mobile = mobile });

        return userId != Guid.Empty;
    }

    public async Task RevokeInvitationAsync(
        Guid userCode,
        CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(userCode, cancellationToken);

        user!.InvitationCode = null;

        await UpdateAsync(user, cancellationToken);
    }

    public async Task<User> UpdateAsync(
        User newUser,
        CancellationToken cancellationToken = default)
    {
        var entry = _dbContext.Users.Update(newUser);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<IReadOnlyList<Expertise>> GetExpertiseAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        return (await connection.QueryAsync<Expertise>(@"SELECT * FROM Expertise"))
            .ToImmutableList();
    }

    public async Task<IReadOnlyList<Discipline>> GetDisciplinesAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        return (await connection.QueryAsync<Discipline, Expertise, Discipline>(
            @"SELECT * FROM Disciplines d
            INNER JOIN Expertise e ON d.ExpertiseCode = e.Id", (discipline, expertise) =>
            {
                discipline.Expertise = expertise;
                return discipline;
            })).ToImmutableList();
    }

    public async Task<IReadOnlyList<Skill>> GetSkillsAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        return (await connection.QueryAsync<Skill>("SELECT * FROM Skills"))
            .ToImmutableList();
    }

    public async Task<IReadOnlyList<Tool>> GetToolsAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);

        return (await connection.QueryAsync<Tool>("SELECT * FROM Tools"))
            .ToImmutableList();
    }

    public async Task UpdateUserExpertiseAsync(
        Guid userId,
        IEnumerable<string>? expertiseNames,
        CancellationToken cancellationToken = default)
    {
        if (expertiseNames is null)
            return;

        await _dbContext.UserExpertise
            .Where(x => x.UserCode == userId)
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (!expertiseNames.Any())
            return;

        var existingExpertise = await GetExpertiseAsync(cancellationToken);
        var addingExpertise = expertiseNames
            .Where(name => !existingExpertise.Any(x => x.Title == name))
            .Select(name => new Expertise { Title = name });
        await _dbContext.Expertise.AddRangeAsync(addingExpertise, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);
        var newUserExpertise = (await connection
            .QueryAsync<Guid>($"SELECT Id FROM Expertise WHERE Title IN ({string.Join(",", expertiseNames.ToWrapped())})"))
            .Select(expertiseId => new UserExpertise
            {
                UserCode = userId,
                ExpertiseCode = expertiseId
            });
        await _dbContext.UserExpertise.AddRangeAsync(newUserExpertise, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserDisciplinesAsync(
        Guid userId,
        IEnumerable<string>? disciplineNames,
        CancellationToken cancellationToken = default)
    {
        if (disciplineNames is null)
            return;

        await _dbContext.UserDisciplines
            .Where(x => x.UserCode == userId)
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (!disciplineNames.Any())
            return;

        var existingDisciplines = await GetDisciplinesAsync(cancellationToken);
        var addingDisciplines = disciplineNames
            .Where(name => !existingDisciplines.Any(x => x.Title == name))
            .Select(name => new Discipline { Title = name });
        await _dbContext.Disciplines.AddRangeAsync(addingDisciplines, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);
        var newUserDisciplines = (await connection
            .QueryAsync<Guid>($"SELECT Id FROM Disciplines WHERE Title IN ({string.Join(",", disciplineNames.ToWrapped())})"))
            .Select(disciplineId => new UserDiscipline
            {
                UserCode = userId,
                DisciplineCode = disciplineId
            });
        await _dbContext.UserDisciplines.AddRangeAsync(newUserDisciplines, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserSkillsAsync(
        Guid userId,
        IEnumerable<string>? skillNames,
        CancellationToken cancellationToken = default)
    {
        if (skillNames is null)
            return;

        await _dbContext.UserSkills
            .Where(x => x.UserCode == userId)
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (!skillNames.Any())
            return;

        var existingSkills = await GetSkillsAsync(cancellationToken);
        var addingSkills = skillNames
            .Where(name => !existingSkills.Any(x => x.Title == name))
            .Select(name => new Skill { Title = name });
        await _dbContext.Skills.AddRangeAsync(addingSkills, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);
        var newUserSkills = (await connection
            .QueryAsync<Guid>($"SELECT Id FROM Skills WHERE Title IN ({string.Join(",", skillNames.ToWrapped())})"))
            .Select(skillId => new UserSkill
            {
                UserCode = userId,
                SkillCode = skillId
            });
        await _dbContext.UserSkills.AddRangeAsync(newUserSkills, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserToolsAsync(
        Guid userId,
        IEnumerable<string>? toolNames,
        CancellationToken cancellationToken = default)
    {
        if (toolNames is null)
            return;

        await _dbContext.UserTools
            .Where(x => x.UserCode == userId)
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (!toolNames.Any())
            return;

        var existingTools = await GetToolsAsync(cancellationToken);
        var addingTools = toolNames
            .Where(name => !existingTools.Any(x => x.Title == name))
            .Select(name => new Tool { Title = name });
        await _dbContext.Tools.AddRangeAsync(addingTools, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var connection = await _dapperContext.OpenConnectionAsync(cancellationToken);
        var newUserTools = (await connection
            .QueryAsync<Guid>($"SELECT Id FROM Tools WHERE Title IN ({string.Join(",", toolNames.ToWrapped())})"))
            .Select(ToolId => new UserTool
            {
                UserCode = userId,
                ToolCode = ToolId
            });
        await _dbContext.UserTools.AddRangeAsync(newUserTools, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
