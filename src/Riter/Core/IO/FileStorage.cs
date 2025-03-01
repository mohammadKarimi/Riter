using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Media.Imaging;

namespace Riter.Core.IO;

public class FileStorage
{
    private const string FilePath = "appsettings.json";

    public static async Task SaveConfig(AppSettings settings)
    {
        string content = JsonSerializer.Serialize(new { AppSettings = settings });
        await File.WriteAllTextAsync(FilePath, content);
    }

    public static void SaveScreenshot(RenderTargetBitmap renderBitmap)
    {
        try
        {
            string path = $"{Directory.GetCurrentDirectory()}/Screenshots/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, $"Screenshot_{DateTime.Now:yyyyMMddHHmmss}.png");
            using FileStream fileStream = new(filePath, FileMode.Create);
            PngBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            encoder.Save(fileStream);
        }
        catch (Exception ex)
        {
            if (!EventLog.SourceExists("RiterApp"))
            {
                EventLog.CreateEventSource("RiterApp", "Application");
            }

            EventLog.WriteEntry("RiterApp", ex.ToString(), EventLogEntryType.Error);
        }
    }
}
