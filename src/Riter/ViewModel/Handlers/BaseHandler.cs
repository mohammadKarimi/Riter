﻿using System.ComponentModel;
using Riter.Core.Consts;

namespace Riter.ViewModel.Handlers;

public abstract class BaseHandler : INotifyPropertyChanged
{
    private static string _buttonSelectedName;
    private static string _previousButtonSelectedName = string.Empty;
    private static bool _isSettingPanelOpened;

    public BaseHandler()
    {
        ButtonSelectedName = ButtonNames.DefaultButtonSelectedName;
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

    /// <summary>
    /// Updates the value of a field and raises the PropertyChanged event if the value has changed.
    /// This method ensures that the UI is notified of changes in the state, and can optionally
    /// invoke a custom action when the value changes.
    /// </summary>
    /// <typeparam name="T">The type of the property being set.</typeparam>
    /// <param name="field">The field that stores the current value of the property.</param>
    /// <param name="newValue">The new value to be assigned to the field.</param>
    /// <param name="propertyName">The name of the property being updated.</param>
    /// <param name="onChangedAction">An optional action to invoke when the property changes.</param>
    /// <returns>
    /// Returns true if the value of the field was changed; otherwise, false.
    /// </returns>
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
        ButtonSelectedName = _previousButtonSelectedName;
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
