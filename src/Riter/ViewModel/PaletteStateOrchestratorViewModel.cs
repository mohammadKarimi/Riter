using System.Windows.Ink;
using Riter.Core.Drawing;
using Riter.Services;

namespace Riter.ViewModel;

public class PaletteStateOrchestratorViewModel : BaseViewModel
{
    private readonly HotKeyCommandService _hotKeyCommandService;

    public PaletteStateOrchestratorViewModel(
        DrawingViewModel drawingViewModel,
        StrokeVisibilityViewModel strokeVisibilityViewModel,
        StrokeHistoryViewModel strokeHistoryViewModel,
        BrushSettingsViewModel brushSettingsViewModel,
        InkEditingModeViewModel inkEditingModeViewModel,
        HighlighterViewModel highlighterViewModel,
        SettingPanelViewModel settingPanelViewModel,
        ButtonSelectedViewModel buttonSelectedViewModel,
        StartupLocationViewModel startupLocationViewModel,
        HotKeyCommandService hotKeyCommandService)
    {
        DrawingViewModel = drawingViewModel;
        StrokeVisibilityViewModel = strokeVisibilityViewModel;
        StrokeHistoryViewModel = strokeHistoryViewModel;
        BrushSettingsViewModel = brushSettingsViewModel;
        InkEditingModeViewModel = inkEditingModeViewModel;
        HighlighterViewModel = highlighterViewModel;
        SettingPanelViewModel = settingPanelViewModel;
        ButtonSelectedViewModel = buttonSelectedViewModel;
        StartupLocationViewModel = startupLocationViewModel;

        _hotKeyCommandService = hotKeyCommandService;
        _hotKeyCommandService.InitializeCommands(this.BuildHotKeyCommandMap());

        AttachPropertyChangedHandlers();
    }

    public DrawingViewModel DrawingViewModel { get; }

    public StrokeVisibilityViewModel StrokeVisibilityViewModel { get; }

    public StrokeHistoryViewModel StrokeHistoryViewModel { get; }

    public BrushSettingsViewModel BrushSettingsViewModel { get; }

    public InkEditingModeViewModel InkEditingModeViewModel { get; }

    public HighlighterViewModel HighlighterViewModel { get; }

    public SettingPanelViewModel SettingPanelViewModel { get; }

    public ButtonSelectedViewModel ButtonSelectedViewModel { get; }

    public StartupLocationViewModel StartupLocationViewModel { get; }

    public DrawingAttributes InkDrawingAttributes =>
        DrawingAttributesFactory.CreateDrawingAttributes(
            BrushSettingsViewModel.InkColor,
            BrushSettingsViewModel.SizeOfBrush,
            HighlighterViewModel.IsHighlighter);

    public void HandleHotkey(HotKeiesPressed hotKeies)
    {
        if (DrawingViewModel.IsReleased || SettingPanelViewModel.SettingPanelVisibility == Visibility.Visible)
        {
            return;
        }

        _hotKeyCommandService.ExecuteHotKey(hotKeies);
    }

    private void AttachPropertyChangedHandlers()
    {
        BrushSettingsViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
        HighlighterViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
    }

    private void OnBrushOrHighlightChanged(string propertyName)
    {
        if (propertyName == nameof(BrushSettingsViewModel.SizeOfBrush) ||
            propertyName == nameof(HighlighterViewModel.IsHighlighter) ||
            propertyName == nameof(BrushSettingsViewModel.InkColor))
        {
            OnPropertyChanged(nameof(InkDrawingAttributes));
        }
    }
}
