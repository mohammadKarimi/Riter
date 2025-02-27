using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;

namespace Riter.Services;

public class AutomaticUpdateService
{
    private static readonly string RepoOwner = "MohammadKarimi";
    private static readonly string RepoName = "Riter";
    private static readonly string Url = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases/latest";

    public static async Task UpdateAsync()
    {
        (string latestVersion, string downloadUrl) = await GetLatestVersionAsync();
        if (!string.IsNullOrEmpty(latestVersion) && latestVersion != ApplicationVersionMapper.GetVersion() && !string.IsNullOrEmpty(downloadUrl))
        {
            await DownloadAndUpdateAsync(latestVersion, downloadUrl);
        }
    }

    public static async Task<bool> HasNewVersionAsync()
    {
        (string latestVersion, string downloadUrl) = await GetLatestVersionAsync();
        return !string.IsNullOrEmpty(latestVersion) && latestVersion != ApplicationVersionMapper.GetVersion() && !string.IsNullOrEmpty(downloadUrl);
    }

    private static async Task<(string latestVersion, string downloadUrl)> GetLatestVersionAsync()
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Add("User-Agent", "request");

        try
        {
            string response = await client.GetStringAsync(Url);
            using JsonDocument json = JsonDocument.Parse(response);

            string latestVersion = json.RootElement.GetProperty("tag_name").GetString() ?? string.Empty;
            string downloadUrl = json.RootElement.GetProperty("assets").EnumerateArray()
                .FirstOrDefault(asset => asset.GetProperty("name").GetString()?.EndsWith(".zip") == true)
                .GetProperty("browser_download_url").GetString() ?? string.Empty;

            return (latestVersion, downloadUrl);
        }
        catch (Exception)
        {
            return (string.Empty, string.Empty);
        }
    }

    private static async Task DownloadAndUpdateAsync(string latestVersion, string url)
    {
        try
        {
            string currentDirectory = AppContext.BaseDirectory;
            string downloadPath = Path.Combine(currentDirectory, $"{latestVersion}.zip");

            using HttpClient client = new();
            byte[] data = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(downloadPath, data);

            ZipFile.ExtractToDirectory(downloadPath, currentDirectory, true);

            if (File.Exists(downloadPath))
                File.Delete(downloadPath);

            RestartApplication(currentDirectory);
        }
        catch (Exception)
        {
            // Consider logging the error
        }
    }

    private static void RestartApplication(string currentDirectory)
    {
        string setupPath = Path.Combine(currentDirectory, "Riter.exe");
        Process.Start(new ProcessStartInfo(setupPath) { UseShellExecute = true });
        Environment.Exit(0);
    }
}
