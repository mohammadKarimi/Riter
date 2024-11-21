using Riter.Core.Consts;
using Riter.Core.Enum;
using Riter.Core.WindowExtensions;

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
    private DrawingShape _currentShape = DrawingShape.Line;

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

    public DrawingShape CurrentShape
    {
        get => _currentShape;
        private set => SetProperty(ref _currentShape, value, nameof(CurrentShape));
    }

    public void Release()
    {
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.ReleaseButton);
        IsReleased = true;
    }

    public void StartDrawing()
    {
        _inkEditingModeStateHandler.Ink();
        _highlighterStateHandler.DisableHighlighter();
        SetupDrawingMode(ButtonNames.DrawingButton);
    }

    public void StartDrawingShape(DrawingShape shape)
    {
        CurrentShape = shape;

        _highlighterStateHandler.DisableHighlighter();
        SetupDrawingMode(ButtonNames.ShapeDrawButton);
        _inkEditingModeStateHandler.None();
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
        _inkEditingModeStateHandler.Ink();
        SetupDrawingMode(ButtonNames.HighlighterButton);
    }

    private void SetupDrawingMode(string buttonName)
    {
        if (IsReleased)
        {
            IsReleased = false;
        }

        _buttonSelectedStateHandler.SetButtonSelectedName(buttonName);
        _settingPanelStateHandler.HideAllPanels();
        _buttonSelectedStateHandler.ResetArrowButtonSelected();
    }
}
