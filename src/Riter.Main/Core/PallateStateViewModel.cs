using System.ComponentModel;

namespace Riter.Main.Core;

public partial class PalleteStateViewModel : INotifyPropertyChanged
{
    private readonly PalleteState _state;
    public event PropertyChangedEventHandler PropertyChanged;

    private void ReleasedLock()
        => IsReleased = !IsReleased;

    public bool IsReleased
    {
        get => _state.IsReleased;
        set
        {
            if (_state.IsReleased != value)
            {
                _state.IsReleased = value;
                OnPropertyChanged(nameof(IsReleased));
            }
        }
    }

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    internal void ClearHistory()
    {
        _state.DrawingHistory.Clear();
        _state.RedoHistory.Clear();
    }
}

public partial class PalleteStateViewModel
{
    public ICommand ReleasedButtonCommand;
    public PalleteStateViewModel()
    {
        _state = new PalleteState();
        ReleasedButtonCommand = new RelayCommand(ReleasedLock);
    }
}
