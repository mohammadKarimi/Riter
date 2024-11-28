using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers;

public abstract class BaseStateHandler : INotifyPropertyChanged
{
    /// <summary>
    /// This event is for subscribing the PaletteViewModel for it to send these changes to UI.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

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

    protected bool AlwaysSetProperty<T>(ref T field, T newValue, string propertyName, Action onChangedAction = null)
    {
        field = newValue;
        onChangedAction?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }
}
