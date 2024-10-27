using Riter.Core.Consts;

namespace Riter.ViewModel.StateHandlers;

public class DrawingStateHandler(IInkEditingModeStateHandler inkEditingModeStateHandler) : BaseStateHandler, IDrawingStateHandler
{
    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler = inkEditingModeStateHandler;
    private bool _isReleased = true;
    private bool _isHighlighter;

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

    public bool IsHighlighter
    {
        get => _isHighlighter;
        private set => SetProperty(ref _isHighlighter, value, nameof(IsHighlighter));
    }

    public void Release()
    {
        ButtonSelectedName = ButtonNames.ReleaseButton;
        IsReleased = true;
    }

    public void StartDrawing()
    {
        IsHighlighter = false;
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

    public void EnableHighlighter()
    {
        IsHighlighter = true;
        ButtonSelectedName = ButtonNames.HighlighterButton;
        _inkEditingModeStateHandler.Ink();
        IsReleased = false;
        SettingPanelVisibility = false;
    }
}
