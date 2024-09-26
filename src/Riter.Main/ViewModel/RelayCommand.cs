namespace Riter.Main.ViewModel;
//public class RelayCommand(Action execute, Func<bool> canExecute = null) : ICommand
//{
//    private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));
//    private readonly Func<bool> _canExecute = canExecute;

//    public event EventHandler CanExecuteChanged
//    {
//        add => CommandManager.RequerySuggested += value;
//        remove => CommandManager.RequerySuggested -= value;
//    }

//    public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

//    public void Execute(object parameter) => _execute();
//}

 

public class RelayCommand<T>(Action<T> execute, Predicate<T> canExecute = null) : ICommand
{
    private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    private readonly Predicate<T> _canExecute = canExecute;

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

    public void Execute(object parameter) => _execute((T)parameter);

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
