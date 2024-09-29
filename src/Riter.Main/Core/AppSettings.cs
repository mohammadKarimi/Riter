using Riter.Main.Core.Enum;

namespace Riter.Main.Core;
public class AppSettings
{
    public const string Section = nameof(AppSettings);
    public const string ButtonSelectedName = "ReleasedButton";
    public BrushSize BrushSize { get; set; }
    public string InkDefaultColor { get; set; }
    public bool TopMost { get; set; }

}
