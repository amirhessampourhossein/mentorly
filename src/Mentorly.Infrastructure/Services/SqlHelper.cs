namespace Mentorly.Infrastructure.Services;

public static class SqlHelper
{
    public static IEnumerable<string> ToWrapped(
        this IEnumerable<string> strings,
        char wrapper = '\'')
        => strings.Select(s => wrapper + s + wrapper);
}
