using System.ComponentModel;

namespace Riter.Main.Core;

public class PalleteStateViewModel : INotifyPropertyChanged
{
    private PalleteState _state;
    public event PropertyChangedEventHandler PropertyChanged;

    public PalleteStateViewModel()
    {
        _state = new PalleteState();
    }

    public bool IsReleased
    {
        get => _state.IsReleased;
        set
        {
            _state.IsReleased = value;
            OnPropertyChanged(nameof(IsReleased));
        }
    }

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class PalleteState
{
    public bool IsReleased { get; set; }
}
