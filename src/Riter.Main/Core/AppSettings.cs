using Microsoft.Extensions.Configuration;
using Riter.Main.Core.Enum;

namespace Riter.Main.Core;

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
    /// Default ButtonSelectedName.
    /// </summary>
    public const string ButtonSelectedName = "ReleasedButton";

    /// <summary>
    /// Gets or sets brush size of Ink.
    /// </summary>
    public BrushSize BrushSize { get; set; }

    /// <summary>
    ///  Gets or sets ink Defaulr Color for Drawing.
    /// </summary>
    public string InkDefaultColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether topMost for application.
    /// </summary>
    public bool TopMost { get; set; }
}
