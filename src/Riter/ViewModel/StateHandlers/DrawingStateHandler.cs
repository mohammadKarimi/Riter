using Riter.Core.Consts;

namespace Riter.ViewModel.StateHandlers;

public class DrawingStateHandler(IInkEditingModeStateHandler inkEditingModeStateHandler, IHighlighterStateHandler highlighterStateHandler) : BaseStateHandler, IDrawingStateHandler
{
    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler = inkEditingModeStateHandler;
    private readonly IHighlighterStateHandler _highlighterStateHandler = highlighterStateHandler;
    private bool _isReleased = true;

    public bool IsReleased
    {
        get => _isReleased;
        private set => SetProperty(ref _isReleased, value, nameof(IsReleased), () =>
        {
            if (_isReleased)
            {
                _inkEditingModeStateHandler.None();
            }
            else
            {
                _inkEditingModeStateHandler.Ink();
            }
        });
    }

    public void Release()
    {
        ButtonSelectedName = ButtonNames.ReleaseButton;
        IsReleased = true;
    }

    public void StartDrawing()
    {
        _highlighterStateHandler.DisableHighlighter();
        ButtonSelectedName = ButtonNames.DrawingButton;
        _inkEditingModeStateHandler.Ink();
        IsReleased = false;
        SettingPanelVisibility = false;
    }

    public void StartErasing()
    {
        IsReleased = false;
        _inkEditingModeStateHandler.EraseByStroke();
        ButtonSelectedName = ButtonNames.ErasingButton;
    }

    public void EnableHighlighterInk()
    {
        _highlighterStateHandler.EnableHighlighter();
        ButtonSelectedName = ButtonNames.HighlighterButton;
        _inkEditingModeStateHandler.Ink();
        IsReleased = false;
        SettingPanelVisibility = false;
    }
}
