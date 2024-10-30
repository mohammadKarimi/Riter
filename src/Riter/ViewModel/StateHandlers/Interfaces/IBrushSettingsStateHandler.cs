using System.ComponentModel;
using Riter.Core.Enum;

namespace Riter.ViewModel.StateHandlers;

public interface IBrushSettingsStateHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value of InkColor.
    /// </summary>
    string InkColor { get; }

    /// <summary>
    /// Gets a value of InkColor.
    /// </summary>
    string ColorSelected { get; }

    /// <summary>
    /// Gets a value of Size.
    /// </summary>
    double SizeOfBrush { get; }

    /// <summary>
    /// set InkColor from settings.
    /// </summary>
    /// <param name="color">The color user selected.</param>
    void SetInkColor(string color);

    /// <summary>
    /// Set Size of brush from settins.
    /// </summary>
    /// <param name="size">type of brush size enum.</param>
    void SetSizeOfBrush(string size);

    void SetSizeOfBrushWithHotKey(BrushSize size);

    /// <summary>
    /// set InkColor from settings.
    /// </summary>
    /// <param name="color">The color user selected.</param>
    void SetInkColorWithHotKey(InkColor color);
}
