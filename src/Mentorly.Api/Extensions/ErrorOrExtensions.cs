using ErrorOr;

namespace Mentorly.Api.Extensions;

public static class ErrorOrExtensions
{
    public static IResult ToResult<TValue>(this ErrorOr<TValue> errorOr) => errorOr.MatchFirst(
        value => Results.Ok(value),
        error => Results.Problem(
            title: error.Code,
            detail: error.Description,
            statusCode: error.Type.ToStatusCode())
        );

    private static int ToStatusCode(this ErrorType errorType) => errorType switch
    {
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };
}
