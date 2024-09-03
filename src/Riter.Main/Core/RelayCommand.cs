namespace Riter.Main.Core;
public class RelayCommand(Action execute, Func<bool> canExecute = null) : ICommand
{
    private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    private readonly Func<bool> _canExecute = canExecute;

    public bool CanExecute(object parameter)
        => _canExecute is null || _canExecute();

    public void Execute(object parameter)
        => _execute();

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
