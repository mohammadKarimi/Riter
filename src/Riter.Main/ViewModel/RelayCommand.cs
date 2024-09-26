namespace Riter.Main.ViewModel;

public class RelayCommand<T>(Action<T> execute, Predicate<T> canExecute = null) : ICommand
{
    private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    private readonly Predicate<T> _canExecute = canExecute;

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

    public void Execute(object parameter) => _execute((T)parameter);

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
