using System.Windows.Controls;
using Riter.Core.Consts;

namespace Riter.ViewModel.Handlers;

/// <summary>
/// Represents the state of the palette, including whether ink is released,
/// the ink editing mode, and the selected button name.
/// Handles changes in these states and notifies subscribers of changes.
/// </summary>
public class ButtonSelectionHandler : BaseHandler, IButtonSelectionHandler , IInkEditingModeHandler
{
    private bool _isReleased = true;
    private InkCanvasEditingMode _inkEditingMode = InkCanvasEditingMode.None;
    private bool _isHighlighter;

    public bool IsReleased
    {
        get => _isReleased;
        private set => SetProperty(ref _isReleased, value, nameof(IsReleased), () =>
        {
            InkEditingMode = _isReleased ? InkCanvasEditingMode.None : InkCanvasEditingMode.Ink;
        });
    }

    public bool IsHighlighter
    {
        get => _isHighlighter;
        private set => SetProperty(ref _isHighlighter, value, "InkDrawingAttributes");
    }

    public InkCanvasEditingMode InkEditingMode
    {
        get => _inkEditingMode;
        private set => SetProperty(ref _inkEditingMode, value, nameof(InkEditingMode));
    }

    public void Release()
    {
        ButtonSelectedName = ButtonNames.ReleaseButton;
        IsReleased = true;
    }

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

    public void StartErasing()
    {
        IsReleased = false;
        InkEditingMode = InkCanvasEditingMode.EraseByStroke;
        ButtonSelectedName = ButtonNames.ErasingButton;
    }

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
