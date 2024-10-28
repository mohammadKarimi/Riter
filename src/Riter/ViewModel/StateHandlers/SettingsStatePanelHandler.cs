using Riter.Core.Consts;

namespace Riter.ViewModel.Handlers;
public class SettingsPanelStateHandler : BaseStateHandler, ISettingPanelStateHandler
{
    private static bool _isSettingPanelOpened;
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

    public void SetSettingPanelInvisibile() => SettingPanelVisibility = false;

    public void SetSettingPanelVisibile() => SettingPanelVisibility = true;

    public void ToggleSettingsPanel()
    {
        if (SettingPanelVisibility && _buttonSelectedStateHandler.ButtonSelectedName == ButtonNames.SettingButton)
        {
            _buttonSelectedStateHandler.ResetPreviousButton();
            SettingPanelVisibility = false;
        }
        else
        {
            _buttonSelectedStateHandler.StoreCurrentButton();
            _buttonSelectedStateHandler.SetButtonSelectedName(ButtonNames.SettingButton);
            SettingPanelVisibility = true;
        }
    }
}
