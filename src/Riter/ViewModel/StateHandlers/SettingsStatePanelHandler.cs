using Riter.Core.Consts;

namespace Riter.ViewModel.Handlers;

public class SettingsPanelStateHandler : BaseStateHandler, ISettingPanelStateHandler
{
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler;
    private bool _settingButtonClicked;
    private bool _isSettingPanelOpened;
    private bool _isBrushPanelOpened;
    private bool _isShapePanelOpened;
    private bool _isColorPanelOpened;
    private bool _isTimerPanelOpened;
    private bool _isNotificationIndicatorVisible;
    private string _pinPanel;

    public SettingsPanelStateHandler(IButtonSelectedStateHandler buttonSelectedStateHandler)
    {
        _isSettingPanelOpened = false;
        _buttonSelectedStateHandler = buttonSelectedStateHandler;
        _isSettingPanelOpened = false;
    }

    public bool SettingPanelVisibility
    {
        get => _isSettingPanelOpened;
        protected set => SetProperty(ref _isSettingPanelOpened, value, nameof(SettingPanelVisibility));
    }

    public bool ColorPanelVisibility
    {
        get => _isColorPanelOpened;
        protected set => SetProperty(ref _isColorPanelOpened, value, nameof(ColorPanelVisibility));
    }

    public bool BrushPanelVisibility
    {
        get => _isBrushPanelOpened;
        protected set => SetProperty(ref _isBrushPanelOpened, value, nameof(BrushPanelVisibility));
    }

    public bool ShapePanelVisibility
    {
        get => _isShapePanelOpened;
        protected set => SetProperty(ref _isShapePanelOpened, value, nameof(ShapePanelVisibility));
    }

    public bool TimerPanelVisibility
    {
        get => _isTimerPanelOpened;
        protected set => SetProperty(ref _isTimerPanelOpened, value, nameof(TimerPanelVisibility));
    }

    public bool SettingButtonClicked
    {
        get => _settingButtonClicked;
        protected set => SetProperty(ref _settingButtonClicked, value, nameof(SettingButtonClicked));
    }

    public string PinPanel
    {
        get => _pinPanel;
        protected set => SetProperty(ref _pinPanel, value, nameof(PinPanel));
    }

    public bool NotificationIndicatorVisibility
    {
        get => _isNotificationIndicatorVisible;
        protected set => SetProperty(ref _isNotificationIndicatorVisible, value, nameof(NotificationIndicatorVisibility));
    }

    public void HideAllPanels()
    {
        SettingPanelVisibility = false;
        BrushPanelVisibility = false;
        ShapePanelVisibility = false;
        SettingButtonClicked = false;
        ColorPanelVisibility = false;
        TimerPanelVisibility = false;
        _buttonSelectedStateHandler.ResetArrowButtonSelected();
    }

    public void SetSettingPanelVisibile() => SettingPanelVisibility = true;

    public void ToggleBrushSettingsPanel(string button)
    {
        if (BrushPanelVisibility && _buttonSelectedStateHandler.ArrowButtonSelectedName == button)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            HideAllPanels();
            return;
        }

        HideAllPanels();
        _buttonSelectedStateHandler.SetArrowButtonSelected(ButtonNames.ChangeBrushSettingButton);
        BrushPanelVisibility = true;
    }

    public void ToggleShapePanel(string button)
    {
        if (ShapePanelVisibility && _buttonSelectedStateHandler.ArrowButtonSelectedName == button)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            HideAllPanels();
            return;
        }

        HideAllPanels();
        _buttonSelectedStateHandler.SetArrowButtonSelected(ButtonNames.ChangeShapeSettingButton);
        ShapePanelVisibility = true;
    }

    public void ToggleSettingsPanel()
    {
        if (SettingPanelVisibility)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            HideAllPanels();
            return;
        }

        HideAllPanels();
        SettingButtonClicked = true;
        SettingPanelVisibility = true;
    }

    public void ToggleColorPanel()
    {
        if (ColorPanelVisibility)
        {
            HideAllPanels();
            return;
        }

        HideAllPanels();
        ColorPanelVisibility = true;
    }

    public void ToggleTimerPanel(string button)
    {
        if (TimerPanelVisibility && _buttonSelectedStateHandler.ArrowButtonSelectedName == button)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            HideAllPanels();
            return;
        }

        HideAllPanels();
        _buttonSelectedStateHandler.SetArrowButtonSelected(ButtonNames.ChangeTimerSettingButton);
        TimerPanelVisibility = true;
    }

    public void TogglePinPanel(string panelName)
    {
        if (PinPanel != panelName)
        {
            PinPanel = panelName;
            HideAllPanels();
        }
        else
        {
            PinPanel = null;
        }
    }

    public void ShowNotification() => NotificationIndicatorVisibility = true;
}
