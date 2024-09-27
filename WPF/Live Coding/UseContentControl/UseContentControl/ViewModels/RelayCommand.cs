using System.Windows.Input;

namespace UseContentControl.ViewModels;

public class RelayCommand : ICommand
{
    private Predicate<object>? _canExecute;
    private Action<object> _execute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        this._canExecute = canExecute;
        this._execute = execute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        if (_canExecute == null) return true;

        return _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}
