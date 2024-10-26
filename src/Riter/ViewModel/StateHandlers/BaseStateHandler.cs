using System.ComponentModel;
using Riter.Core.Consts;

namespace Riter.ViewModel.StateHandlers;

public abstract class BaseStateHandler : INotifyPropertyChanged
{
    private static string _buttonSelectedName;
    private static string _previousButtonSelectedName = string.Empty;
    private static bool _isSettingPanelOpened;

    static BaseStateHandler()
    {
        _buttonSelectedName = ButtonNames.DefaultButtonSelectedName;
    }

    public BaseStateHandler()
    {
        _isSettingPanelOpened = false;
    }

    /// <summary>
    /// This event is for subscribing the PalleteViewModel for it to send these changes to UI.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets or sets a value indicating whether value for showing setting pannel.
    /// </summary>
    public bool SettingPanelVisibility
    {
        get => _isSettingPanelOpened;
        protected set => SetProperty(ref _isSettingPanelOpened, value, nameof(SettingPanelVisibility));
    }

    /// <summary>
    /// Gets or sets the name of the button that is currently selected.
    /// </summary>
    public string ButtonSelectedName
    {
        get => _buttonSelectedName;
        protected set => SetProperty(ref _buttonSelectedName, value, nameof(ButtonSelectedName));
    }

    /// <summary>
    /// Raises the PropertyChanged event when a property value changes.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetProperty<T>(ref T field, T newValue, string propertyName, Action onChangedAction = null)
    {
        if (!Equals(field, newValue))
        {
            field = newValue;
            onChangedAction?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        return false;
    }

    protected void ResetPreviousButton()
    {
        if (!string.IsNullOrEmpty(_previousButtonSelectedName))
        {
            ButtonSelectedName = _previousButtonSelectedName;
        }

        _previousButtonSelectedName = string.Empty;
        SettingPanelVisibility = false;
    }

    protected void StoreCurrentButton()
    {
        if (string.IsNullOrEmpty(_previousButtonSelectedName))
        {
            _previousButtonSelectedName = ButtonSelectedName;
        }
    }
}
