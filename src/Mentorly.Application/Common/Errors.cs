namespace Mentorly.Application.Common;

public record CustomError(
    string ErrorCode,
    string ErrorMessage)
{
    public static implicit operator string(CustomError customError)
        => $"{customError.ErrorCode} - {customError.ErrorMessage}";
}

public static class CustomErrors
{
    public static readonly CustomError MobileAlreadyRegistered = new(
        "User.Conflict",
        "This mobile is already registered!");

    public static readonly CustomError EmailAlreadyRegistered = new(
        "User.Conflict",
        "This email is already registered!");

    public static readonly CustomError FailedToCreateMeeting = new(
        "Meeting.Failure",
        "Failed to create the meeting!");

    public static readonly CustomError FailedToRemoveMeeting = new(
        "Meeting.Failure",
        "Failed to remove the meeting!");

    public static readonly CustomError AuthenticatedNotFound = new(
        "User.Conflict",
        "User is authenticated but not found with the given email!");

    public static readonly CustomError UserNotFound = new(
        "User.NotFound",
        "User is not registered!");
}
