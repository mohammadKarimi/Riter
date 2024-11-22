using Riter.Core.Enum;

namespace Riter.Core.Drawing;

public static class ColorPalette
{
    public static readonly Dictionary<InkColor, (string Name, string Hex)> Colors = new()
    {
        { InkColor.Yellow, ("Yellow", "#FFFF00") },
        { InkColor.Purple, ("Purple", "#7853DE") },
        { InkColor.Mint, ("Mint", "#3BE1A9") },
        { InkColor.Coral, ("Coral", "#FF8C82") },
        { InkColor.Red, ("Red", "#FF5656") },
        { InkColor.Cyan, ("Cyan", "#01C7FC") },
        { InkColor.Pink, ("White", "#DB6EBD") },
        { InkColor.Gray, ("Gray", "#909090") },
        { InkColor.Black, ("Black", "#000000") },
    };
}
