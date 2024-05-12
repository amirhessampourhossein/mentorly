using ErrorOr;
using Mentorly.Application.Common;

namespace Mentorly.Application.Services;

public interface IMeetManager
{
    Task<ErrorOr<Created>> AddMeetingAsync(
        MeetingInfo info,
        CancellationToken cancellationToken = default);

    Task<ErrorOr<Deleted>> RemoveMeetingAsync(
        string meetingId,
        CancellationToken cancellation = default);
}
