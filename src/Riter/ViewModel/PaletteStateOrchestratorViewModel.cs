using System.Windows.Ink;
using Riter.Core.Enum;

namespace Riter.ViewModel;

public class PaletteStateOrchestratorViewModel : BaseViewModel
{
    private readonly AppSettings _appSettings;
    private readonly Dictionary<HotKey, Action> _hotKeyCommandMap = [];

    public PaletteStateOrchestratorViewModel(
        DrawingViewModel drawingViewModel,
        StrokeVisibilityViewModel strokeVisibilityViewModel,
        StrokeHistoryViewModel strokeHistoryViewModel,
        BrushSettingsViewModel brushSettingsViewModel,
        InkEditingModeViewModel inkEditingModeViewModel,
        HighlighterViewModel highlighterViewModel,
        SettingPanelViewModel settingPanelViewModel,
        ButtonSelectedViewModel buttonSelectedViewModel,
        AppSettings appSettings)
    {
        _appSettings = appSettings;
        DrawingViewModel = drawingViewModel;
        StrokeVisibilityViewModel = strokeVisibilityViewModel;
        StrokeHistoryViewModel = strokeHistoryViewModel;
        BrushSettingsViewModel = brushSettingsViewModel;
        InkEditingModeViewModel = inkEditingModeViewModel;
        HighlighterViewModel = highlighterViewModel;
        SettingPanelViewModel = settingPanelViewModel;
        ButtonSelectedViewModel = buttonSelectedViewModel;

        _hotKeyCommandMap = new()
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
                { HotKey.White, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.White) },
                { HotKey.Orange, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Orange) },
                { HotKey.Gray, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Gray) },
                { HotKey.Black, () => BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Black) },
                { HotKey.TransparentBackground, () => InkEditingModeViewModel.EnableTransparentCommand.Execute(null) },
                { HotKey.BlackboardBackground, () => InkEditingModeViewModel.EnableBlackboardCommand.Execute(null) },
                { HotKey.WhiteboardBackground, () => InkEditingModeViewModel.EnableWhiteboardCommand.Execute(null) },
                { HotKey.Arrow, () => drawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Arrow).ToString()) },
                { HotKey.Rectangle, () => drawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Rectangle).ToString()) },
                { HotKey.Circle, () => drawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Circle).ToString()) },
                { HotKey.Database, () => drawingViewModel.DrawShapeCommand.Execute(((byte)DrawingShape.Database).ToString()) },
            };

        BrushSettingsViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
        HighlighterViewModel.PropertyChanged += (_, e) => OnBrushOrHighlightChanged(e.PropertyName);
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
    
    public void HandleHotkey(HotKeiesPressed hotKeies)
    {
        if (DrawingViewModel.IsReleased)
        {
            return;
        }

        var keiesMap = BuildKeyCombination(hotKeies);
        var hotkey = _appSettings.HotKeysConfig.FirstOrDefault(x => x.Key == keiesMap);
        if (hotkey is null)
        {
            return;
        }

        if (!Enum.TryParse<HotKey>(hotkey.Value, out var hotKeyEnum))
        {
            return;
        }

        if (_hotKeyCommandMap.TryGetValue(hotKeyEnum, out var command))
        {
            command();
        }
    }

    private static string BuildKeyCombination(HotKeiesPressed hotKeies)
    {
        var keiesMap = string.Empty;
        if (hotKeies.CtrlPressed)
        {
            keiesMap += "CTRL + ";
        }

        if (hotKeies.ShiftPressed)
        {
            keiesMap += "SHIFT + ";
        }

        keiesMap += hotKeies.Key.ToString().ToUpper();
        return keiesMap;
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
