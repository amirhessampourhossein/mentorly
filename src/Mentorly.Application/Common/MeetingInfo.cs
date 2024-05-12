namespace Mentorly.Application.Common;

public record MeetingInfo(
    string InterviewerName,
    string InterviewerEmail,
    string IntervieweeName,
    string IntervieweeEmail,
    string Title,
    string Description,
    string Summary,
    DateTime StartTime,
    Guid SessionId);