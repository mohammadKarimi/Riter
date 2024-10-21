namespace Riter.ViewModel.Handlers.Interfaces;
public interface ISettingPanelHandler
{
    bool SettingPanelVisibility { get; }

    void ToggleSettingsPanel();
}
