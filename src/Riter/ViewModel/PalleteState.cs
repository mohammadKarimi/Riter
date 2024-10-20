using System.ComponentModel;
using System.Windows.Controls;
using Riter.Core.Consts;
using Riter.ViewModel.Handlers;

namespace Riter.ViewModel;

/// <summary>
/// Represents the state of the palette, including whether ink is released,
/// the ink editing mode, and the selected button name.
/// Handles changes in these states and notifies subscribers of changes.
/// </summary>
public class PalleteState : BaseHandler, INotifyPropertyChanged
{
    private bool _isReleased = true;
    private InkCanvasEditingMode _inkEditingMode = InkCanvasEditingMode.None;
    private bool _isHighlighter;

    /// <summary>
    /// Gets a value indicating whether the ink has been released.
    /// </summary>
    public bool IsReleased
    {
        get => _isReleased;
        private set => SetProperty(ref _isReleased, value, nameof(IsReleased), () =>
        {
            InkEditingMode = _isReleased ? InkCanvasEditingMode.None : InkCanvasEditingMode.Ink;
        });
    }

    /// <summary>
    /// Gets a value indicating whether gets a value of Enabling Highlighter Pen.
    /// </summary>
    public bool IsHighlighter
    {
        get => _isHighlighter;
        private set => SetProperty(ref _isHighlighter, value, "InkDrawingAttributes");
    }

    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    public InkCanvasEditingMode InkEditingMode
    {
        get => _inkEditingMode;
        private set => SetProperty(ref _inkEditingMode, value, nameof(InkEditingMode));
    }

    /// <summary>
    /// Releases the ink based on the button pressed.
    /// </summary>
    public void Release()
    {
        ButtonSelectedName = ButtonNames.ReleaseButton;
        IsReleased = true;
    }

    /// <summary>
    /// Starts drawing ink based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to start drawing ink.</param>
    public void StartDrawing()
    {
        IsHighlighter = false;
        if (IsReleased is false && ButtonSelectedName == ButtonNames.DrawingButton)
        {
            ResetToDefault();
        }
        else
        {
            ButtonSelectedName = ButtonNames.DrawingButton;
            InkEditingMode = InkCanvasEditingMode.Ink;
            IsReleased = false;
            SettingPanelVisibility = false;
        }
    }

    /// <summary>
    /// Starts erasing based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to start erasing.</param>
    public void StartErasing()
    {
        IsReleased = false;
        InkEditingMode = InkCanvasEditingMode.EraseByStroke;
        ButtonSelectedName = ButtonNames.ErasingButton;
    }

    /// <summary>
    /// Set Type of Ink editing Mode for Line and shape mode.
    /// </summary>
    /// <param name="inkCanvasEditing">type of edition.</param>
    public void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing) => InkEditingMode = inkCanvasEditing;

    /// <summary>
    /// Open Setting Panel.
    /// </summary>
    public void ToggleSettingsPanel()
    {
        if (SettingPanelVisibility && ButtonSelectedName == ButtonNames.SettingButton)
        {
            ResetPreviousButton();
        }
        else
        {
            StoreCurrentButton();
            ButtonSelectedName = ButtonNames.SettingButton;
            SettingPanelVisibility = true;
        }
    }

    /// <summary>
    /// Enable Highlighter pen.
    /// </summary>
    public void EnableHighlighter()
    {
        IsHighlighter = true;
        ButtonSelectedName = ButtonNames.HighlighterButton;
        InkEditingMode = InkCanvasEditingMode.Ink;
        IsReleased = false;
        SettingPanelVisibility = false;
        OnPropertyChanged("InkDrawingAttributes");
    }

    private void ResetToDefault()
    {
        ButtonSelectedName = ButtonNames.DefaultButtonSelectedName;
        IsReleased = true;
        SettingPanelVisibility = false;
    }
}
