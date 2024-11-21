using System.Windows.Controls;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.WindowExtensions;
public class StartupLocationContext(StartupLocation startupLocation)
{
    private readonly IStartupLocationStrategy _strategy = startupLocation switch
    {
        StartupLocation.BottomCenter => new BottomCenterStrategy(),
        StartupLocation.BottomLeft => new BottomLeftStrategy(),
        StartupLocation.BottomRight => new BottomRightStrategy(),
        StartupLocation.Center => new CenterStrategy(),
        _ => new BottomCenterStrategy()
    };

    public void ExecuteStrategy(Grid layout, StackPanel mainPalette, AppSettings appSettings)
        => _strategy.AdjustSize(layout, mainPalette, appSettings);
}
