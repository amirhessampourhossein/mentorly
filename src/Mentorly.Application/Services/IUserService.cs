namespace Mentorly.Domain.Users;

public interface IUserService
{
    Task<bool> IsEmailRegisteredAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<bool> IsMobileRegisteredAsync(
        string mobile,
        CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<User> AddAsync(
        User user,
        CancellationToken cancellationToken = default);

    Task<User> UpdateAsync(
        User modifiedUser,
        CancellationToken cancellationToken = default);

    Task<string> GetOrCreateInvitationAsync(
        Guid userCode,
        CancellationToken cancellationToken = default);

    Task<Guid> GetIdFromInvitationAsync(
        string invitationCode,
        CancellationToken cancellationToken = default);

    Task RevokeInvitationAsync(
        Guid userCode,
        CancellationToken cancellationToken = default);

    Task UpdateUserExpertiseAsync(
        Guid userId,
        IEnumerable<string>? expertiseNames,
        CancellationToken cancellationToken = default);

    Task UpdateUserDisciplinesAsync(
        Guid userId,
        IEnumerable<string>? disciplineNames,
        CancellationToken cancellationToken = default);

    Task UpdateUserSkillsAsync(
        Guid userId,
        IEnumerable<string>? skillNames,
        CancellationToken cancellationToken = default);

    Task UpdateUserToolsAsync(
        Guid userId,
        IEnumerable<string>? toolNames,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Expertise>> GetExpertiseAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Discipline>> GetDisciplinesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Skill>> GetSkillsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Tool>> GetToolsAsync(CancellationToken cancellationToken = default);
}
