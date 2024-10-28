﻿namespace Riter.ViewModel;
public class SettingPanelViewModel : BaseViewModel
{
    private readonly ISettingPanelStateHandler _settingPanelStateHandler;

    public SettingPanelViewModel(ISettingPanelStateHandler settingPanelStateHandler)
    {
        _settingPanelStateHandler = settingPanelStateHandler;
        _settingPanelStateHandler.PropertyChanged += OnStateChanged;
    }

    public Visibility SettingPanelVisibility => _settingPanelStateHandler.SettingPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public ICommand ToggleSettingsPanelCommand => new RelayCommand(_settingPanelStateHandler.ToggleSettingsPanel);

    public ICommand ShowBrushSettingsPanelCommand => new RelayCommand<string>(_settingPanelStateHandler.ToggleBrushSettingsPanel);
}