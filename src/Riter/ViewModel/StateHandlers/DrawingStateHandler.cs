using Riter.Core.Consts;

namespace Riter.ViewModel.StateHandlers;

public class DrawingStateHandler(
    IInkEditingModeStateHandler inkEditingModeStateHandler,
    IHighlighterStateHandler highlighterStateHandler,
    IButtonSelectedStateHandler buttonSelectedStateHandler,
    ISettingPanelStateHandler settingPanelStateHandler) : BaseStateHandler, IDrawingStateHandler
{
    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler = inkEditingModeStateHandler;
    private readonly IHighlighterStateHandler _highlighterStateHandler = highlighterStateHandler;
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler = buttonSelectedStateHandler;
    private readonly ISettingPanelStateHandler _settingPanelStateHandler = settingPanelStateHandler;
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
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.ReleaseButton);
        IsReleased = true;
    }

    public void StartDrawing()
    {
        _highlighterStateHandler.DisableHighlighter();
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.DrawingButton);
        _inkEditingModeStateHandler.Ink();
        IsReleased = false;
        _settingPanelStateHandler.SetSettingPanelInvisibile();
    }

    public void StartErasing()
    {
        IsReleased = false;
        _inkEditingModeStateHandler.EraseByStroke();
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.ErasingButton);
    }

    public void StartHighlighterDrawing()
    {
        _highlighterStateHandler.EnableHighlighter();
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.HighlighterButton);
        _inkEditingModeStateHandler.Ink();
        IsReleased = false;
        _settingPanelStateHandler.SetSettingPanelInvisibile();
    }
}
