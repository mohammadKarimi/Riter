using Riter.Core.Consts;

namespace Riter.ViewModel.Handlers;
public class SettingsPanelStateHandler : BaseStateHandler, ISettingPanelStateHandler
{

    public void ToggleSettingsPanel()
    {
        if (SettingPanelVisibility && ButtonSelectedName == ButtonNames.SettingButton)
        {
            ResetPreviousButton();
        }
        else
        {
            StoreCurrentButton();
            ButtonSelectedName = ButtonNames.SettingButton;
            SettingPanelVisibility = true;
        }
    }
}
