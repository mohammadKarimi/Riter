using System.Windows.Ink;
using Riter.Core.Enum;

namespace Riter.ViewModel;

public class PalleteStateOrchestratorViewModel : BaseViewModel
{
    public PalleteStateOrchestratorViewModel(
        DrawingViewModel drawingViewModel,
        StrokeVisibilityViewModel strokeVisibilityViewModel,
        StrokeHistoryViewModel strokeHistoryViewModel,
        BrushSettingsViewModel brushSettingsViewModel,
        InkEditingModeViewModel inkEditingModeViewModel,
        HighlighterViewModel highlighterViewModel,
        SettingPanelViewModel settingPanelViewModel,
        ButtonSelectedViewModel buttonSelectedViewModel)
    {
        DrawingViewModel = drawingViewModel;
        StrokeVisibilityViewModel = strokeVisibilityViewModel;
        StrokeHistoryViewModel = strokeHistoryViewModel;
        BrushSettingsViewModel = brushSettingsViewModel;
        InkEditingModeViewModel = inkEditingModeViewModel;
        HighlighterViewModel = highlighterViewModel;
        SettingPanelViewModel = settingPanelViewModel;
        ButtonSelectedViewModel = buttonSelectedViewModel;

        BrushSettingsViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
        HighlighterViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
        DrawingViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
        SettingPanelViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
        buttonSelectedViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
    }

    public DrawingViewModel DrawingViewModel { get; init; }

    public StrokeVisibilityViewModel StrokeVisibilityViewModel { get; init; }

    public InkEditingModeViewModel InkEditingModeViewModel { get; init; }

    public StrokeHistoryViewModel StrokeHistoryViewModel { get; init; }

    public BrushSettingsViewModel BrushSettingsViewModel { get; init; }

    public HighlighterViewModel HighlighterViewModel { get; init; }

    public SettingPanelViewModel SettingPanelViewModel { get; init; }

    public ButtonSelectedViewModel ButtonSelectedViewModel { get; init; }

    public DrawingAttributes InkDrawingAttributes => DrawingAttributesFactory.CreateDrawingAttributes(BrushSettingsViewModel.InkColor, BrushSettingsViewModel.SizeOfBrush, HighlighterViewModel.IsHighlighter);

    public Visibility SettingPanelVisibility => SettingPanelViewModel.SettingPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public string ButtonSelectedName => ButtonSelectedViewModel.ButtonSelectedName;

    public void HandleHotkey(HotKey hotKey)
    {
        switch (hotKey)
        {
            case HotKey.Drawing:
                DrawingViewModel.StartDrawingCommand.Execute(null);
                break;
            case HotKey.Erasing:
                DrawingViewModel.StartErasingCommand.Execute(null);
                break;
            case HotKey.Trash:
                StrokeHistoryViewModel.ClearCommand.Execute(null);
                break;
            case HotKey.Highlighter:
                DrawingViewModel.ToggleHighlighterCommand.Execute(null);
                break;
            case HotKey.Release:
                DrawingViewModel.ReleaseCommand.Execute(null);
                break;
            case HotKey.HideAll:
                StrokeVisibilityViewModel.HideAllCommand.Execute(null);
                break;
            case HotKey.Undo:
                StrokeHistoryViewModel.UndoCommand.Execute(null);
                break;
            case HotKey.Redo:
                StrokeHistoryViewModel.RedoCommand.Execute(null);
                break;
            case HotKey.SizeOfBrush1X:
                BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X1);
                break;
            case HotKey.SizeOfBrush2X:
                BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X2);
                break;
            case HotKey.SizeOfBrush3X:
                BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X3);
                break;
            default:
                break;
        }
    }

    private void OnBrushOrHighlightChanged(string propertyName)
    {
        if (propertyName == nameof(BrushSettingsViewModel.SizeOfBrush) ||
            propertyName == nameof(HighlighterViewModel.IsHighlighter) || propertyName == nameof(BrushSettingsViewModel.InkColor))
        {
            OnPropertyChanged(nameof(InkDrawingAttributes));
        }

        if (propertyName == nameof(SettingPanelVisibility))
        {
            OnPropertyChanged(nameof(SettingPanelVisibility));
        }

        if (propertyName == nameof(ButtonSelectedName))
        {
            OnPropertyChanged(nameof(ButtonSelectedName));
        }
    }
}
