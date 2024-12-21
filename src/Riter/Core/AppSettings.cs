using System.Text.Json.Serialization;
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
    [JsonIgnore]
    public const string Section = nameof(AppSettings);

    /// <summary>
    /// Gets address of Project on Github.
    /// </summary>
    [JsonIgnore]
    public const string GitHubProjectUrl = "https://github.com/mohammadKarimi/Riter";

    /// <summary>
    /// Gets address of My Telegram Address.
    /// </summary>
    [JsonIgnore]
    public const string MyTelegram = "https://t.me/mhakarimi";

    /// <summary>
    ///  Gets or sets ink Defaulr Color for Drawing.
    /// </summary>
    public string InkDefaultColor { get; set; }

    /// <summary>
    /// <summary>
    /// Gets or sets brush size of Ink.
    /// </summary>
    public double BrushSize { get; set; }

    public StartupLocation StartupLocation { get; set; }

    public List<HotKeysConfig> HotKeysConfig { get; set; }
}
