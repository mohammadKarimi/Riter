using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers;

/// <summary>
/// Represents the state of the palette, including whether ink is released,
/// the ink editing mode, and the selected button name.
/// Handles changes in these states and notifies subscribers of changes.
/// </summary>
public interface IDrawingStateHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value indicating whether the ink has been released.
    /// </summary>
    bool IsReleased { get; }

    string ButtonSelectedName { get; }

    /// <summary>
    /// Releases the ink based on the button pressed.
    /// </summary>
    void Release();

    /// <summary>
    /// Starts drawing ink based on the button pressed.
    /// </summary>
    void StartDrawing();

    /// <summary>
    /// Starts erasing based on the button pressed.
    /// </summary>
    void StartErasing();

    void EnableHighlighterInk();
}
