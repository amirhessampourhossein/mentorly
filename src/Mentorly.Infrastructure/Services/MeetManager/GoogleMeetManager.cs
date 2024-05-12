using ErrorOr;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Mentorly.Application.Common;
using Mentorly.Application.Services;
using Microsoft.Extensions.Options;

namespace Mentorly.Infrastructure.Services.MeetManager;

public class GoogleMeetManager(IOptions<GoogleOptions> options) : IMeetManager
{
    private const string MeetingOrganizer = "Mentorly";
    private const string MeetingType = "hangoutsMeet";
    private const string DefaultCalendarId = "primary";

    public async Task<ErrorOr<Created>> AddMeetingAsync(
        MeetingInfo info,
        CancellationToken cancellationToken = default)
    {
        var calendarService = await GetCalendarServiceAsync(cancellationToken);

        var calendarEvent = CreateCalendarEvent(info);

        var eventRequest = calendarService.Events.Insert(calendarEvent, DefaultCalendarId);

        if (eventRequest is null)
            return ErrorOr.Error.Failure(
            CustomErrors.FailedToCreateMeeting.ErrorCode,
            CustomErrors.FailedToCreateMeeting.ErrorMessage);

        eventRequest.ConferenceDataVersion = 1;
        var createdEvent = await eventRequest.ExecuteAsync(cancellationToken);

        if (createdEvent is null)
            return ErrorOr.Error.Failure(
            CustomErrors.FailedToCreateMeeting.ErrorCode,
            CustomErrors.FailedToCreateMeeting.ErrorMessage);

        return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> RemoveMeetingAsync(
        string meetingId,
        CancellationToken cancellationToken = default)
    {
        var calendarService = await GetCalendarServiceAsync(cancellationToken);

        var deleteRequest = calendarService.Events.Delete(meetingId, DefaultCalendarId);

        if (deleteRequest is null)
            return ErrorOr.Error.Failure(
            CustomErrors.FailedToRemoveMeeting.ErrorCode,
            CustomErrors.FailedToRemoveMeeting.ErrorMessage);

        var result = await deleteRequest.ExecuteAsync(cancellationToken);

        if (result is null)
            return ErrorOr.Error.Failure(
            CustomErrors.FailedToRemoveMeeting.ErrorCode,
            CustomErrors.FailedToRemoveMeeting.ErrorMessage);

        return Result.Deleted;
    }

    private async Task<CalendarService> GetCalendarServiceAsync(CancellationToken cancellationToken = default)
    {
        string[] scopes = [CalendarService.Scope.Calendar];

        UserCredential credential;

        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        new ClientSecrets()
        {
            ClientId = options.Value.ClientId,
            ClientSecret = options.Value.ClientSecret
        },
        scopes,
        "user",
        cancellationToken);

        var service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = MeetingOrganizer
        });

        return service;
    }

    private static Event CreateCalendarEvent(MeetingInfo info)
    {
        var calendarEvent = new Event
        {
            Creator = new()
            {
                DisplayName = MeetingOrganizer
            },
            Organizer = new()
            {
                DisplayName = MeetingOrganizer
            },
            Summary = info.Summary,
            Description = info.Description,
            Attendees =
            [
                new()
                {
                    DisplayName = info.InterviewerName,
                    Email = info.InterviewerEmail
                },
                new()
                {
                    DisplayName = info.IntervieweeName,
                    Email = info.IntervieweeEmail
                }
            ],
            Start = new()
            {
                DateTimeDateTimeOffset = info.StartTime
            },
            End = new()
            {
                DateTimeDateTimeOffset = info.StartTime.AddHours(1)
            },
            ConferenceData = new()
            {
                CreateRequest = new()
                {
                    RequestId = info.SessionId.ToString(),
                    ConferenceSolutionKey = new()
                    {
                        Type = MeetingType
                    }
                }
            },
            GuestsCanInviteOthers = false,
            GuestsCanModify = false
        };

        return calendarEvent;
    }
}
