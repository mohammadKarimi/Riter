using System.ComponentModel;
using System.Windows.Controls;

namespace Riter.Main.ViewModel;

public class PalleteState : INotifyPropertyChanged
{
    private bool _isReleased = true;
    private InkCanvasEditingMode _inkEditingMode = InkCanvasEditingMode.None;
    private string _buttonSelectedName = "ReleasedButton";

    public event PropertyChangedEventHandler PropertyChanged;

    public bool IsReleased
    {
        get => _isReleased;
        private set
        {
            if (_isReleased != value)
            {
                _isReleased = value;
                OnPropertyChanged(nameof(IsReleased));
                InkEditingMode = _isReleased ? InkCanvasEditingMode.None : InkCanvasEditingMode.Ink;
            }
        }
    }

    public InkCanvasEditingMode InkEditingMode
    {
        get => _inkEditingMode;
        private set
        {
            if (_inkEditingMode != value)
            {
                _inkEditingMode = value;
                OnPropertyChanged(nameof(InkEditingMode));
            }
        }
    }

    public string ButtonSelectedName
    {
        get => _buttonSelectedName;
        private set
        {
            if (_buttonSelectedName != value)
            {
                _buttonSelectedName = value;
                OnPropertyChanged(nameof(ButtonSelectedName));
            }
        }
    }

    public event EventHandler<StateChangedEventArgs> StateChanged;

    public void Release(string buttonName)
    {
        IsReleased = true;
        ButtonSelectedName = buttonName;
        StateChanged?.Invoke(this, new StateChangedEventArgs(nameof(IsReleased)));
    }

    public void StartDrawing(string buttonName)
    {
        IsReleased = false;
        InkEditingMode = InkCanvasEditingMode.Ink;
        ButtonSelectedName = buttonName;
        StateChanged?.Invoke(this, new StateChangedEventArgs(nameof(InkEditingMode)));
    }

    public void StartErasing(string buttonName)
    {
        IsReleased = false;
        InkEditingMode = InkCanvasEditingMode.EraseByStroke;
        ButtonSelectedName = buttonName;
        StateChanged?.Invoke(this, new StateChangedEventArgs(nameof(InkEditingMode)));
    }

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
