using YummyEnhanced.Services.Interfaces;

namespace YummyEnhanced.Services.Implementations;

/// <summary>
/// Provides file management operations for uploading and deleting files within the application's web root uploads
/// directory.
/// </summary>
/// <remarks>This service is intended for use in web applications that require handling of user-uploaded files.
/// All file operations are scoped to the 'uploads' directory under the application's web root to help ensure files are
/// managed in a controlled location. The service does not perform validation on file content or type; callers are
/// responsible for any necessary validation prior to upload. This class is not thread-safe.</remarks>
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;
    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }
    
    /// <summary>
    /// Deletes the specified file from the given folder within the application's web root uploads directory.
    /// </summary>
    /// <remarks>If the specified file does not exist, no action is taken and no exception is thrown. The
    /// method only affects files located in the uploads directory under the application's web root.</remarks>
    /// <param name="fileName">The name of the file to delete. If null or empty, the method does nothing.</param>
    /// <param name="folderName">The name of the folder within the uploads directory that contains the file to delete.</param>
    public void Delete(string fileName, string folderName)
    {
        if (string.IsNullOrEmpty(fileName)) return;

        string filePath = Path.Combine(_env.WebRootPath, "uploads", folderName, fileName);

        if (File.Exists(filePath)) 
            File.Delete(filePath);
    }

    /// <summary>
    /// Asynchronously uploads the specified file to the given folder and returns the generated file name.
    /// </summary>
    /// <remarks>The uploaded file is saved in a subdirectory of the web root's 'uploads' folder, under the
    /// specified folder name. The returned file name is a newly generated unique identifier combined with the original
    /// file's extension.</remarks>
    /// <param name="file">The file to upload. Must not be null or empty.</param>
    /// <param name="folderName">The name of the folder within the uploads directory where the file will be stored. If the folder does not exist,
    /// it will be created.</param>
    /// <returns>A string containing the unique file name assigned to the uploaded file.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="file"/> is null.</exception>
    /// <exception cref="InvalidDataException">Thrown if <paramref name="file"/> is empty.</exception>
    public async Task<string> UploadAsync(IFormFile file, string folderName)
    {
        if (file is null) 
            throw new ArgumentNullException(nameof(file), "File is required");

        if (file.Length == 0)
            throw new InvalidDataException("File is empty");

        string uploadPath = Path.Combine(_env.WebRootPath, "uploads", folderName);

        if(!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string fullPath = Path.Combine(uploadPath, fileName);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }
}
