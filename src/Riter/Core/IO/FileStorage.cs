using System.IO;
using System.Text.Json;
using System.Windows.Media.Imaging;

namespace Riter.Core.IO;
public class FileStorage
{
    private const string FilePath = "appsettings.json";

    public static async Task SaveConfig(AppSettings settings)
    {
        var content = JsonSerializer.Serialize(new { AppSettings = settings });
        await File.WriteAllTextAsync(FilePath, content);
    }

    public static void SaveScreenshot(RenderTargetBitmap renderBitmap)
    {
        try
        {
            var path = $"{Directory.GetCurrentDirectory()}/Screenshots/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, $"Screenshot_{DateTime.Now:yyyyMMddHHmmss}.png");
            using FileStream fileStream = new(filePath, FileMode.Create);
            PngBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            encoder.Save(fileStream);
        }
        catch
        {
        }
    }
}
