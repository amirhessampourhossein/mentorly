namespace Mentorly.Api.Extensions;

public static class FormFileExtensions
{
    public static async Task<byte[]?> ToBytesAsync(this IFormFile? formFile)
    {
        if (formFile is null)
            return null;
        using var stream = new MemoryStream();
        await formFile.CopyToAsync(stream);
        return stream.ToArray();
    }
}
