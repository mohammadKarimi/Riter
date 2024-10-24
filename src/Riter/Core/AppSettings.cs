using Microsoft.Extensions.Configuration;
using Riter.Core.Enum;

namespace Riter.Core;

/// <summary>
/// AppSettings Object for reading the appsettings.json.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// name of section.
    /// </summary>
    public const string Section = nameof(AppSettings);

    /// <summary>
    ///  Gets or sets ink Defaulr Color for Drawing.
    /// </summary>
    public const string InkDefaultColor = "#FFFF5656";

    /// <summary>
    /// Gets or sets brush size of Ink.
    /// </summary>
    public const double BrushSize = 5;

    /// <summary>
    /// Gets address of Project on Github.
    /// </summary>
    public const string GitHubProjectUrl = "https://github.com/mohammadKarimi/Riter";

    /// <summary>
    /// Gets address of My Telegram Address.
    /// </summary>
    public const string MyTelegram = "https://t.me/mhakarimi";

    public IEnumerable<HotKeysConfig> HotKeysConfig { get; set; }
}
