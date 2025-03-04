using System.Net.Http;
using System.Text.Json;

namespace Riter.Services;

public class AutomaticUpdateService
{
    private static readonly string RepoOwner = "MohammadKarimi";
    private static readonly string RepoName = "Riter";
    private static readonly string Url = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases/latest";

    public static async Task<(bool, string)> HasNewVersionAsync()
    {
        (string latestVersion, string downloadUrl) = await GetLatestVersionAsync();
        bool newVersion = !string.IsNullOrEmpty(latestVersion) && latestVersion != ApplicationVersionMapper.GetVersion() && !string.IsNullOrEmpty(downloadUrl);
        return (newVersion, latestVersion);
    }

    public static async Task<(string latestVersion, string downloadUrl)> GetLatestVersionAsync()
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
}
