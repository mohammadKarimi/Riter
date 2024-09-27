namespace Riter.Main.ViewModel;

public class StateChangedEventArgs(string propertyName) : EventArgs
{
    public string PropertyName { get; } = propertyName;
}

