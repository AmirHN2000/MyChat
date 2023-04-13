namespace MyChat.Server.BL.Helpers.FileManager;

public class FileManagerService
{
    private readonly string RootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); 

    public async Task SaveFile(Stream fileStream, string path)
    {
        var list = path.Split('/').ToList();
        list.Insert(0, RootPath);
        var savaPath = Path.Combine(list.ToArray());
        
        var parent = Directory.GetParent(savaPath);
        if (!parent.Exists)
        {
            parent.Create();
        }
        
        await using var file = File.Create(savaPath);
        await fileStream.CopyToAsync(file);
        
        file.Close();
    }
}