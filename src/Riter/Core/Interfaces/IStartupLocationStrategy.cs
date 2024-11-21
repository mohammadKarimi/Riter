using System.Windows.Controls;

namespace Riter.Core.Interfaces;
public interface IStartupLocationStrategy
{
    void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings);
}
