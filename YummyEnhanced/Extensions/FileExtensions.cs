namespace YummyEnhanced.Extensions;

/// <summary>
/// Provides extension methods for IFormFile to assist with file type and size validation based on file extensions and
/// size constraints.
/// </summary>
/// <remarks>These methods enable common file validation scenarios in web applications, such as checking whether
/// an uploaded file is an image, video, or document, or whether it meets size requirements. All extension methods are
/// static and intended for use with IFormFile instances, typically in ASP.NET Core applications handling file
/// uploads.</remarks>
public static class FileExtensions
{
    /// <summary>
    /// Determines whether the specified file's size is less than or equal to the allowed maximum size in megabytes.
    /// </summary>
    /// <param name="file">The file to check. Cannot be null.</param>
    /// <param name="maxMb">The maximum allowed file size, in megabytes. Must be greater than or equal to 0.</param>
    /// <returns>true if the file's size is less than or equal to the specified maximum size; otherwise, false.</returns>
    public static bool IsAllowedSize(this IFormFile file, int maxMb)
    {
        return file.Length <= maxMb * 1024 * 1024;
    }

    /// <summary>
    /// Determines whether the specified file has an image file extension.
    /// </summary>
    /// <remarks>This method checks the file's extension against a set of common image formats, including
    /// .jpg, .jpeg, .png, .webp, .gif, .bmp, and .svg. The check is based solely on the file extension and does not
    /// inspect the file's content.</remarks>
    /// <param name="file">The file to check for a valid image extension. Cannot be null.</param>
    /// <returns>true if the file has a recognized image file extension; otherwise, false.</returns>
    public static bool IsImage(this IFormFile file)
    {
        string[] allowed = { ".jpg", ".jpeg", ".png", ".webp", ".gif", ".bmp", ".svg" };

        bool isImage = file.HasValidExtension(allowed);

        return isImage;
    }
       
    /// <summary>
    /// Determines whether the specified file has a video file extension commonly used for web and media applications.
    /// </summary>
    /// <remarks>This method checks the file's extension against a predefined list of common video formats,
    /// including .mp4, .mov, .webm, .avi, .mkv, .flv, and .wmv. The check is based solely on the file extension and
    /// does not inspect the file's content.</remarks>
    /// <param name="file">The file to check for a supported video file extension. Cannot be null.</param>
    /// <returns>true if the file has a recognized video file extension; otherwise, false.</returns>
    public static bool IsVideo(this IFormFile file)
    {
        string[] allowed = { ".mp4", ".mov", ".webm", ".avi", ".mkv", ".flv", ".wmv" };

        bool isVideo = file.HasValidExtension(allowed);

        return isVideo;
    }

    /// <summary>
    /// Determines whether the specified file is recognized as a common document type based on its file extension.
    /// </summary>
    /// <remarks>This method checks the file's extension against a predefined set of common document formats.
    /// The check is case-insensitive and does not validate the file's actual content.</remarks>
    /// <param name="file">The file to evaluate. Must not be null.</param>
    /// <returns>true if the file has a supported document extension such as .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .txt,
    /// or .csv; otherwise, false.</returns>
    public static bool IsDocument(this IFormFile file)
    {
        string[] allowed = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv" };

        bool isDocument = file.HasValidExtension(allowed);

        return isDocument;
    }

    /// <summary>
    /// Determines whether the file has an extension that matches any of the specified allowed extensions.
    /// </summary>
    /// <remarks>The comparison is case-insensitive. If file is null, the method returns false.</remarks>
    /// <param name="file">The file to check for a valid extension. Can be null.</param>
    /// <param name="allowedExtensions">An array of allowed file extensions to compare against. Each extension should include the leading period (for
    /// example, ".jpg").</param>
    /// <returns>true if the file's extension matches one of the allowed extensions; otherwise, false.</returns>
    public static bool HasValidExtension(this IFormFile file, params string[] allowedExtensions)
    {
        if (file is null) 
            return false;

        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        bool hasAllowedExtension = allowedExtensions.Contains(extension);

        return hasAllowedExtension;
    }
}
