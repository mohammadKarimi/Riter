using System.ComponentModel;

namespace Riter.ViewModel.Handlers;

public interface IStrokeVisibilityHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value indicating whether gets the value of Is Hide all Stroke.
    /// </summary>
    bool IsHideAll { get; }

    /// <summary>
    /// Hide All Strokes in Main Ink.
    /// </summary>
    void HideAll();
}
