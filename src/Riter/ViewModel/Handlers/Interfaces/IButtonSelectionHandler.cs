using System.ComponentModel;
using System.Windows.Controls;

namespace Riter.ViewModel;

public interface IButtonSelectionHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value indicating whether the ink has been released.
    /// </summary>
    bool IsReleased { get; }

    string ButtonSelectedName { get; }

    /// <summary>
    /// Gets a value indicating whether gets a value of Enabling Highlighter Pen.
    /// </summary>
    public bool IsHighlighter { get; }

#warning This prop must move to another handler
    bool SettingPanelVisibility { get; }

    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    InkCanvasEditingMode InkEditingMode { get; }

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

    void EnableHighlighter();

#warning This method must move to another handler
    void ToggleSettingsPanel();

#warning This method must move to another handler
    /// <summary>
    /// Set Type of Ink editing Mode for Line and shape mode.
    /// </summary>
    /// <param name="inkCanvasEditing">type of edition.</param>
    void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing);
}
