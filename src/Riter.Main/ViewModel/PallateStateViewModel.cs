using System.ComponentModel;
using System.Windows.Controls;

namespace Riter.Main.ViewModel;

public partial class PalleteStateViewModel : INotifyPropertyChanged
{
    private readonly PalleteState _state;
    public event PropertyChangedEventHandler PropertyChanged;

    private void ReleaseInk(string buttonName)
    {
        IsReleased = true;
        ButtonSelectedName = buttonName;
    }

    private void DrawingInk(string buttonName)
    {
        IsReleased = false;
        InkEditingMode = InkCanvasEditingMode.Ink;
        ButtonSelectedName = buttonName;
    }

    private void Erasing(string buttonName)
    {
        IsReleased = false;
        InkEditingMode = InkCanvasEditingMode.EraseByStroke;
        ButtonSelectedName = buttonName;
    }

    public bool IsReleased
    {
        get => _state.IsReleased;
        set
        {
            if (_state.IsReleased != value)
            {
                _state.IsReleased = value;
                OnPropertyChanged(nameof(IsReleased));
                InkEditingMode = _state.IsReleased ? InkCanvasEditingMode.None : InkCanvasEditingMode.Ink;
            }
        }
    }

    public InkCanvasEditingMode InkEditingMode
    {
        get => _state.InkEditingMode;
        set
        {
            if (_state.InkEditingMode != value)
            {
                _state.InkEditingMode = value;
                OnPropertyChanged(nameof(InkEditingMode));
            }
        }
    }

    public string ButtonSelectedName
    {
        get => _state.ButtonSelectedName;
        set
        {
            if (_state.ButtonSelectedName != value)
            {
                _state.ButtonSelectedName = value;
                OnPropertyChanged(nameof(ButtonSelectedName));
            }
        }
    }

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public partial class PalleteStateViewModel
{
    public ICommand ReleasedButtonCommand { get; private set; }
    public ICommand DrawingButtonCommand { get; private set; }
    public ICommand ErasingButtonCommand { get; private set; }

    public PalleteStateViewModel()
    {
        _state = new PalleteState();
        ReleasedButtonCommand = new RelayCommand<string>(ReleaseInk);
        DrawingButtonCommand = new RelayCommand<string>(DrawingInk);
        ErasingButtonCommand = new RelayCommand<string>(Erasing);
    }
}
