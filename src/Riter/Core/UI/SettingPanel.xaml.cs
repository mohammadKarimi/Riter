using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

namespace Riter.Core.UI;

/// <summary>
/// Interaction logic for SettingPanel.xaml.
/// </summary>
public partial class SettingPanel : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingPanel"/> class.
    /// </summary>
    public SettingPanel()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gets open Github Project in Broswer.
    /// </summary>
    private void SourceCode_Click(object sender, MouseButtonEventArgs e)
     => _ = Process.Start(new ProcessStartInfo
     {
         FileName = AppSettings.GitHubProjectUrl,
         UseShellExecute = true,
     });

    /// <summary>
    /// Gets open Tegram in Broswer.
    /// </summary>
    private void Telegram_Click(object sender, MouseButtonEventArgs e)
     => _ = Process.Start(new ProcessStartInfo
     {
         FileName = AppSettings.MyTelegram,
         UseShellExecute = true,
     });

    /// <summary>
    /// Enable Blackboard.
    /// </summary>
    private void EnableBlackboard_Click(object sender, MouseButtonEventArgs e)
     => ((MainWindow)Window.GetWindow(this)).MainInkCanvasControl.MainInkCanvas.Background = Application.Current.Resources["BlackBoard"] as Brush;

    /// <summary>
    /// Enable Whiteboard.
    /// </summary>
    private void EnableWhiteboard_Click(object sender, MouseButtonEventArgs e)
    => ((MainWindow)Window.GetWindow(this)).MainInkCanvasControl.MainInkCanvas.Background = Application.Current.Resources["WhiteBoard"] as Brush;

    /// <summary>
    /// Enable Transparent.
    /// </summary>
    private void EnableTransparent_Click(object sender, MouseButtonEventArgs e)
    => ((MainWindow)Window.GetWindow(this)).MainInkCanvasControl.MainInkCanvas.Background = Application.Current.Resources["Transparent"] as Brush;
}
