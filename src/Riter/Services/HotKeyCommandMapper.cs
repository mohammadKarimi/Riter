using Riter.Core.Enum;
using Riter.ViewModel;

namespace Riter.Services;
public static class HotKeyCommandMapper
{
    public static Dictionary<HotKey, Action> BuildHotKeyCommandMap(this PaletteStateOrchestratorViewModel viewModel) => new()
    {
            { HotKey.Drawing, () => viewModel.DrawingViewModel.StartDrawingCommand.Execute(null) },
            { HotKey.Erasing, () => viewModel.DrawingViewModel.StartErasingCommand.Execute(null) },
            { HotKey.Trash, () => viewModel.StrokeHistoryViewModel.ClearCommand.Execute(null) },
            { HotKey.Highlighter, () => viewModel.DrawingViewModel.ToggleHighlighterCommand.Execute(null) },
            { HotKey.Release, () => viewModel.DrawingViewModel.ReleaseCommand.Execute(null) },
            { HotKey.HideAll, () => viewModel.StrokeVisibilityViewModel.HideAllCommand.Execute(null) },
            { HotKey.Undo, () => viewModel.StrokeHistoryViewModel.UndoCommand.Execute(null) },
            { HotKey.Redo, () => viewModel.StrokeHistoryViewModel.RedoCommand.Execute(null) },
            { HotKey.SizeOfBrush07X, () => viewModel.BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X07) },
            { HotKey.SizeOfBrush1X, () => viewModel.BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X1) },
            { HotKey.SizeOfBrush2X, () => viewModel.BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X2) },
            { HotKey.SizeOfBrush3X, () => viewModel.BrushSettingsViewModel.SetSizeOfBrushWithHotKeyCommand.Execute(BrushSize.X3) },
            { HotKey.Yellow, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Yellow) },
            { HotKey.Purple, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Purple) },
            { HotKey.Mint, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Mint) },
            { HotKey.Coral, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Coral) },
            { HotKey.Red, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Red) },
            { HotKey.Cyan, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Cyan) },
            { HotKey.Pink, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Pink) },
            { HotKey.Gray, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Gray) },
            { HotKey.Black, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.Black) },
            { HotKey.Rainbow, () => viewModel.BrushSettingsViewModel.SetInkColorWithHotKeyCommand.Execute(InkColor.RainBow) },
            { HotKey.TransparentBackground, () => viewModel.InkEditingModeViewModel.EnableTransparentCommand.Execute(null) },
            { HotKey.BlackboardBackground, () => viewModel.InkEditingModeViewModel.EnableBlackboardCommand.Execute(null) },
            { HotKey.WhiteboardBackground, () => viewModel.InkEditingModeViewModel.EnableWhiteboardCommand.Execute(null) },
            { HotKey.Arrow, () => viewModel.DrawingViewModel.DrawShapeCommand.Execute(DrawingShape.Arrow) },
            { HotKey.Rectangle, () => viewModel.DrawingViewModel.DrawShapeCommand.Execute(DrawingShape.Rectangle) },
            { HotKey.Circle, () => viewModel.DrawingViewModel.DrawShapeCommand.Execute(DrawingShape.Circle) },
            { HotKey.FilledRectangle, () => viewModel.DrawingViewModel.DrawShapeCommand.Execute(DrawingShape.FilledRectangle) },
            { HotKey.FilledCircle, () => viewModel.DrawingViewModel.DrawShapeCommand.Execute(DrawingShape.FilledCircle) },
            { HotKey.Database, () => viewModel.DrawingViewModel.DrawShapeCommand.Execute(DrawingShape.Database) },
            { HotKey.Line, () => viewModel.DrawingViewModel.DrawShapeCommand.Execute(DrawingShape.Line) },
            { HotKey.FadeInk, () => viewModel.StrokeVisibilityViewModel.EnableFadeInk.Execute(null) },
            { HotKey.ScreenShot, () => viewModel.ScreenShotViewModel.TakeCommand.Execute(null) },
    };
}
