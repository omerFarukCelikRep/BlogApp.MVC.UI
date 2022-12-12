namespace BlogApp.UI.Extensions;

public static class FormFileExtension
{
    public static async Task<string> FileToStringAsync(this IFormFile file, CancellationToken cancellationToken = default)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);
        var fileAsByteArray = stream.ToArray();
        return Convert.ToBase64String(fileAsByteArray);
    }
}
