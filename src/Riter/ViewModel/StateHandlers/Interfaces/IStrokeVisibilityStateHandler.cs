using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers;

public interface IStrokeVisibilityStateHandler : INotifyPropertyChanged
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
