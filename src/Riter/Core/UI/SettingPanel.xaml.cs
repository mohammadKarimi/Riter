﻿using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Riter.Core.UI;

/// <summary>
/// Interaction logic for SettingPanel.xaml.
/// </summary>
public partial class SettingPanel : UserControl
{
    private readonly AppSettings _appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingPanel"/> class.
    /// </summary>
    public SettingPanel()
    {
        InitializeComponent();
        _appSettings = App.ServiceProvider.GetService<AppSettings>();
        TransparentLable.Content = _appSettings.HotKeysConfig.FirstOrDefault(x => x.Key == HotKey.TransparentBackground.ToString()).Value;
        WhiteBoatdLable.Content = _appSettings.HotKeysConfig.FirstOrDefault(x => x.Key == HotKey.WhiteboardBackground.ToString()).Value;
        BlackBoatdLable.Content = _appSettings.HotKeysConfig.FirstOrDefault(x => x.Key == HotKey.BlackboardBackground.ToString()).Value;
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
}
