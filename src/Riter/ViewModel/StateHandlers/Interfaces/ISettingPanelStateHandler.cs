using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers;
public interface ISettingPanelStateHandler : INotifyPropertyChanged
{
    bool SettingPanelVisibility { get; }

    bool BrushPanelVisibility { get; }

    bool ShapePanelVisibility { get; }

    bool SettingButtonClicked { get; }

    bool ColorPanelVisibility { get; }

    bool TimerPanelVisibility { get; }

    string PinPanel { get; }

    void HideAllPanels();

    void ToggleShapePanel(string button);

    void ToggleTimerPanel(string button);

    void SetSettingPanelVisibile();

    void ToggleBrushSettingsPanel(string button);

    void ToggleSettingsPanel();

    void ToggleColorPanel();

    void TogglePinPanel(string panelName);
}
