namespace Riter.Core;

/// <summary>
/// A generic implementation of the <see cref="ICommand"/> interface, 
/// allowing commands to be bound to actions with parameters and conditional execution logic.
/// </summary>
/// <typeparam name="T">The type of the parameter passed to the command.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="RelayCommand{T}"/> class with the specified execute action
/// and an optional condition for whether the command can execute.
/// </remarks>
/// <param name="execute">The action to execute when the command is invoked.</param>
/// <param name="canExecute">The condition that determines if the command can execute. If null, the command is always executable.</param>
/// <exception cref="ArgumentNullException">Thrown if the execute action is null.</exception>
public class RelayCommand(Action execute, Func<bool> canExecute = null) : ICommand
{
    private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    private readonly Func<bool> _canExecute = canExecute;

    /// <summary>
    /// CanExecuteChanged.
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">The parameter to pass to the <see cref="Predicate{T}"/> that determines whether the command can execute.</param>
    /// <returns>
    /// True if the command can execute; otherwise, false.
    /// </returns>
    public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

    /// <summary>
    /// Executes the command action with the specified parameter.
    /// </summary>
    /// <param name="parameter">The parameter to pass to the <see cref="Action{T}"/> that executes the command logic.</param>

    public void Execute(object parameter) => _execute();
}
