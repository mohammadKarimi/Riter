namespace Riter.ViewModel;
public class SettingPanelViewModel : BaseViewModel
{
    private readonly ISettingPanelStateHandler _settingPanelStateHandler;

    public SettingPanelViewModel(ISettingPanelStateHandler settingPanelStateHandler)
    {
        _settingPanelStateHandler = settingPanelStateHandler;
        _settingPanelStateHandler.PropertyChanged += OnStateChanged;
    }

    public bool SettingPanelVisibility => _settingPanelStateHandler.SettingPanelVisibility;

    public ICommand ToggleSettingsPanelCommand => new RelayCommand(_settingPanelStateHandler.ToggleSettingsPanel);
}
