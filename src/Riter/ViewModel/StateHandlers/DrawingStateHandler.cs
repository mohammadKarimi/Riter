using Riter.Core.Consts;
using Riter.Core.Enum;

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
    private DrawingShape _currentShape = DrawingShape.FreeDraw;

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
        CurrentShape = DrawingShape.FreeDraw;
        _highlighterStateHandler.DisableHighlighter();
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.DrawingButton);
        _inkEditingModeStateHandler.Ink();
        IsReleased = false;
        _settingPanelStateHandler.SetSettingPanelInvisibile();
    }

    public void StartDrawingShape(string shapeId = "1")
    {
        CurrentShape = (DrawingShape)byte.Parse(shapeId);
        IsReleased = false;
        _settingPanelStateHandler.SetSettingPanelInvisibile();
        _buttonSelectedStateHandler.ResetArrowButtonSelected();
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.ShapeDrawButton);
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
        _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.HighlighterButton);
        _inkEditingModeStateHandler.Ink();
        IsReleased = false;
        _settingPanelStateHandler.SetSettingPanelInvisibile();
    }
}
