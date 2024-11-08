using Riter.Core.Consts;

namespace Riter.ViewModel.Handlers;
public class SettingsPanelStateHandler : BaseStateHandler, ISettingPanelStateHandler
{
    private static bool _settingButtonClicked;
    private static bool _isSettingPanelOpened;
    private static bool _isBrushPanelOpened;
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler;

    static SettingsPanelStateHandler()
    {
        _isSettingPanelOpened = false;
    }

    public SettingsPanelStateHandler(IButtonSelectedStateHandler buttonSelectedStateHandler)
    {
        _buttonSelectedStateHandler = buttonSelectedStateHandler;
        _isSettingPanelOpened = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether value for showing setting pannel.
    /// </summary>
    public bool SettingPanelVisibility
    {
        get => _isSettingPanelOpened;
        protected set => SetProperty(ref _isSettingPanelOpened, value, nameof(SettingPanelVisibility));
    }

    public bool BrushPanelVisibility
    {
        get => _isBrushPanelOpened;
        protected set => SetProperty(ref _isBrushPanelOpened, value, nameof(BrushPanelVisibility));
    }

    public bool SettingButtonClicked
    {
        get => _settingButtonClicked;
        protected set => SetProperty(ref _settingButtonClicked, value, nameof(SettingButtonClicked));
    }

    public void SetSettingPanelInvisibile()
    {
        SettingPanelVisibility = false;
        BrushPanelVisibility = false;
    }

    public void SetSettingPanelVisibile() => SettingPanelVisibility = true;

    public void ToggleBrushSettingsPanel(string button)
    {
        if (BrushPanelVisibility && _buttonSelectedStateHandler.ArrowButtonSelectedName == button)
        {
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
            SettingPanelVisibility = false;
            SettingButtonClicked = false;
            BrushPanelVisibility = false;
        }
        else
        {
            _buttonSelectedStateHandler.SetArrowButtonSelected(ButtonNames.ChangeBrushSettingButton);
            BrushPanelVisibility = true;
        }
    }

    public void ToggleSettingsPanel()
    {
        if (SettingPanelVisibility)
        {
            SettingButtonClicked = false;
            SettingPanelVisibility = false;
            _buttonSelectedStateHandler.ResetArrowButtonSelected();
        }
        else
        {
            SettingButtonClicked = true;
            SettingPanelVisibility = true;
        }
    }
}
