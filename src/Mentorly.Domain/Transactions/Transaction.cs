using Mentorly.Domain.Common;
using Mentorly.Domain.Sessions;

namespace Mentorly.Domain.Transactions;

public class Transaction : Entity
{
    public Session Session { get; set; } = null!;

    public Guid SessionCode { get; set; }

    public bool Status { get; set; }

    public int ReferenceCode { get; set; }
}