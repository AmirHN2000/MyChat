using MyChat.Server.BL.Helpers.FileManager;

namespace Server.Web.Api.Helper;

public static class FileUtility
{
    public static string[] ImageTypes = { ".jpg", ".jpeg", ".png", ".svg"};
    
    public static string[] FileTypes = { ".jpg", ".jpeg", ".png", ".svg", ".txt", ".mp3", ".mp4"};

    public static string GetFileName => Guid.NewGuid().ToString().Replace("-", "");
    
    public static async Task<string> SaveImage(this IFormFile formFile, string path)
    {
        var fileType = Path.GetExtension(formFile.FileName);
        if (ImageTypes.Contains(fileType))
        {
            path += GetFileName + fileType;
            var fileManagerService = new FileManagerService();
            await fileManagerService.SaveFile(formFile.OpenReadStream(), path);

            return path;
        }

        return string.Empty;
    }
}