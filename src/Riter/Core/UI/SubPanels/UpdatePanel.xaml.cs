using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Windows.Controls;
using Riter.Services;

namespace Riter.Core.UI.SubPanels;

/// <summary>
/// Interaction logic for UpdatePanel.xaml.
/// </summary>
public partial class UpdatePanel : UserControl
{
    private const int BufferSize = 8192;
    private readonly IProgress<int> _downloadProgress;

    public UpdatePanel()
    {
        InitializeComponent();
        _downloadProgress = new Progress<int>(UpdateDownloadProgress);
    }

    public async Task UpdateAsync()
    {
        try
        {
            (string latestVersion, string downloadUrl) = await AutomaticUpdateService.GetLatestVersionAsync();
            if (!ShouldUpdate(latestVersion, downloadUrl))
                return;

            UpdatePaths paths = GetUpdatePaths(latestVersion);
            await DownloadAndExtractUpdate(downloadUrl, paths);
            CreateUpdateScript(paths);
            ExecuteUpdateAndRestart(paths);
        }
        catch
        {
        }
    }

    private static bool ShouldUpdate(string latestVersion, string downloadUrl) => !string.IsNullOrEmpty(latestVersion)
         && latestVersion != ApplicationVersionMapper.GetVersion()
         && !string.IsNullOrEmpty(downloadUrl);

    private static void ExecuteUpdateAndRestart(UpdatePaths paths)
    {
        Process.Start(new ProcessStartInfo(paths.BatchScript) { UseShellExecute = true });
        Environment.Exit(0);
    }

    private static async Task DownloadFileAsync(string url, string destinationPath, IProgress<int> progress)
    {
        using HttpClient client = new();
        using HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        long totalBytes = response.Content.Headers.ContentLength
            ?? throw new Exception("Unable to determine file size.");

        await using Stream contentStream = await response.Content.ReadAsStreamAsync();
        await using FileStream fileStream = new FileStream(
            destinationPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            BufferSize,
            true);

        byte[] buffer = new byte[BufferSize];
        long totalRead = 0L;
        int bytesRead;

        while ((bytesRead = await contentStream.ReadAsync(buffer)) > 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
            totalRead += bytesRead;

            int percentComplete = (int)((totalRead * 100) / totalBytes);
            progress.Report(percentComplete);
        }
    }

    private static UpdatePaths GetUpdatePaths(string latestVersion)
    {
        string currentDirectory = AppContext.BaseDirectory;
        return new UpdatePaths(
            currentDirectory,
            Path.Combine(currentDirectory, $"{latestVersion}.zip"),
            Path.Combine(currentDirectory, "temp_update"),
            Path.Combine(currentDirectory, "temp_update", "Riter"),
            Path.Combine(currentDirectory, "update.bat"));
    }

    private void UpdateDownloadProgress(int percent)
    {
        ProgressBar.Value = percent;
        StatusText.Text = $"Downloading...     {percent}%";
    }

    private async Task DownloadAndExtractUpdate(string downloadUrl, UpdatePaths paths)
    {
        await DownloadFileAsync(downloadUrl, paths.DestinationPath, _downloadProgress);
        ZipFile.ExtractToDirectory(paths.DestinationPath, paths.TempDirectory);

        if (File.Exists(paths.DestinationPath))
            File.Delete(paths.DestinationPath);
    }

    private void CreateUpdateScript(UpdatePaths paths)
    {
        string[] scriptCommands =
        [
            "@echo off",
            "timeout /t 2 /nobreak",
            $"xcopy /Y /E \"{paths.ExtractedPath}\\*\" \"{paths.CurrentDirectory}\"",
            $"rd /s /q \"{paths.TempDirectory}\"",
            $"del \"{paths.DestinationPath}\"",
            $"start \"\" \"{Path.Combine(paths.CurrentDirectory, "Riter.exe")}\"",
            "del \"%~f0\"",
        ];

        File.WriteAllLines(paths.BatchScript, scriptCommands);
    }

    private async void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            UpdateButton.IsEnabled = false;
            await UpdateAsync(_downloadProgress);
            StatusText.Text = "Download Complete!";
        }
        catch
        {
            StatusText.Text = "Download Failed!";
        }
        finally
        {
            UpdateButton.IsEnabled = true;
        }
    }

    private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(AppSettings.GetReleaseUrl(e.Uri.ToString())) { UseShellExecute = true });
        e.Handled = true;
    }
}

internal record UpdatePaths(
        string CurrentDirectory,
        string DestinationPath,
        string TempDirectory,
        string ExtractedPath,
        string BatchScript
    );
