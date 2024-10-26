namespace Riter.ViewModel.StateHandlers;
public interface ISettingPanelStateHandler
{
    bool SettingPanelVisibility { get; }

    void ToggleSettingsPanel();
}
