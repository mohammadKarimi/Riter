using System.IO;
using System.Text.Json;

namespace Riter.Core.IO;
public class FileStorage
{
    private const string FilePath = "appsettings.json";

    public static async Task SaveConfig(AppSettings settings)
    {
        var content = JsonSerializer.Serialize(new { AppSettings = settings });
        await File.WriteAllTextAsync(FilePath, content);
    }
}
