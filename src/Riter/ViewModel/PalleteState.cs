using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core;

namespace Riter.ViewModel;

/// <summary>
/// Represents the state of the palette, including whether ink is released,
/// the ink editing mode, and the selected button name.
/// Handles changes in these states and notifies subscribers of changes.
/// </summary>
public class PalleteState : INotifyPropertyChanged
{
    private bool _isReleased = true;
    private InkCanvasEditingMode _inkEditingMode = InkCanvasEditingMode.None;
    private string _buttonSelectedName = AppSettings.ButtonSelectedName;

    /// <summary>
    /// This event is for subscribing the PalleteViewModel for it to send these changes to UI.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets a value indicating whether the ink has been released.
    /// </summary>
    public bool IsReleased
    {
        get => _isReleased;
        private set => SetProperty(ref _isReleased, value, nameof(IsReleased), () =>
        {
            InkEditingMode = _isReleased ? InkCanvasEditingMode.None : InkCanvasEditingMode.Ink;
        });
    }

    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    public InkCanvasEditingMode InkEditingMode
    {
        get => _inkEditingMode;
        private set => SetProperty(ref _inkEditingMode, value, nameof(InkEditingMode));
    }

    /// <summary>
    /// Gets the name of the button that is currently selected.
    /// </summary>
    public string ButtonSelectedName
    {
        get => _buttonSelectedName;
        private set => SetProperty(ref _buttonSelectedName, value, nameof(ButtonSelectedName));
    }

    /// <summary>
    /// Releases the ink based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to release ink.</param>
    public void Release(string buttonName)
    {
        ButtonSelectedName = buttonName;
        IsReleased = !IsReleased;
    }

    /// <summary>
    /// Starts drawing ink based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to start drawing ink.</param>
    public void StartDrawing(string buttonName)
    {
        if (IsReleased is false)
        {
            ButtonSelectedName = AppSettings.ButtonSelectedName;
        }
        else
        {
            ButtonSelectedName = buttonName;
            InkEditingMode = InkCanvasEditingMode.Ink;
        }

        IsReleased = !IsReleased;
    }

    /// <summary>
    /// Starts erasing based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to start erasing.</param>
    public void StartErasing(string buttonName)
    {
        IsReleased = false;
        InkEditingMode = InkCanvasEditingMode.EraseByStroke;
        ButtonSelectedName = buttonName;
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
}
