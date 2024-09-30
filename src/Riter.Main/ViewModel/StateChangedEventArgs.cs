namespace Riter.Main.ViewModel;

/// <summary>
/// Provides data for the state change event, typically used when a property value changes.
/// Inherits from <see cref="EventArgs"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="StateChangedEventArgs"/> class with the specified property name.
/// </remarks>
/// <param name="propertyName">The name of the property that changed.</param>
public class StateChangedEventArgs(string propertyName) : EventArgs
{
    /// <summary>
    /// Gets the name of the property that changed.
    /// </summary>
    public string PropertyName { get; } = propertyName;
}
