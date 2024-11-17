using System.Windows.Ink;
using Riter.Core.Enum;
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

        _hotKeyCommandService = new HotKeyCommandService(BuildHotKeyCommandMap());
        BrushSettingsViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
        HighlighterViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
    }

    public DrawingViewModel DrawingViewModel { get; }

    public StrokeVisibilityViewModel StrokeVisibilityViewModel { get; }

    public StrokeHistoryViewModel StrokeHistoryViewModel { get; }

    public BrushSettingsViewModel BrushSettingsViewModel { get; }

    public InkEditingModeViewModel InkEditingModeViewModel { get; }

    public HighlighterViewModel HighlighterViewModel { get; }

    public SettingPanelViewModel SettingPanelViewModel { get; }

    public ButtonSelectedViewModel ButtonSelectedViewModel { get; }

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

    private Dictionary<HotKey, Action> BuildHotKeyCommandMap() => new()
        {
            { HotKey.Drawing, () => DrawingViewModel.StartDrawingCommand.Execute(null) },
            { HotKey.Erasing, () => DrawingViewModel.StartErasingCommand.Execute(null) },
            { HotKey.Trash, () => StrokeHistoryViewModel.ClearCommand.Execute(null) },
            { HotKey.Highlighter, () => DrawingViewModel.ToggleHighlighterCommand.Execute(null) },
            { HotKey.Release, () => DrawingViewModel.ReleaseCommand.Execute(null) },
            { HotKey.HideAll, () => StrokeVisibilityViewModel.HideAllCommand.Execute(null) },
            { HotKey.Undo, () => StrokeHistoryViewModel.UndoCommand.Execute(null) },
            { HotKey.Redo, () => StrokeHistoryViewModel.RedoCommand.Execute(null) },
            { HotKey.SizeOfBrush1X, () => BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X1) },
            { HotKey.SizeOfBrush2X, () => BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X2) },
            { HotKey.SizeOfBrush3X, () => BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X3) },
            { HotKey.Yellow, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Yellow) },
            { HotKey.Purple, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Purple) },
            { HotKey.Mint, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Mint) },
            { HotKey.Coral, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Coral) },
            { HotKey.Red, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Red) },
            { HotKey.Cyan, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Cyan) },
            { HotKey.Pink, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Pink) },
            { HotKey.Gray, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Gray) },
            { HotKey.Black, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Black) },
            { HotKey.TransparentBackground, () => InkEditingModeViewModel.EnableTransparentCommand.Execute(null) },
            { HotKey.BlackboardBackground, () => InkEditingModeViewModel.EnableBlackboardCommand.Execute(null) },
            { HotKey.WhiteboardBackground, () => InkEditingModeViewModel.EnableWhiteboardCommand.Execute(null) },
            { HotKey.Arrow, () => DrawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Arrow).ToString()) },
            { HotKey.Rectangle, () => DrawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Rectangle).ToString()) },
            { HotKey.Circle, () => DrawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Circle).ToString()) },
            { HotKey.Database, () => DrawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Database).ToString()) },
        };

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
