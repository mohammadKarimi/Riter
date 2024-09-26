using System.ComponentModel;
using System.Windows.Controls;

namespace Riter.Main.ViewModel;

public partial class PalleteStateViewModel : INotifyPropertyChanged
{
    private readonly PalleteState _state;
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnStateChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);

    private void ReleaseInk(string buttonName) => _state.Release(buttonName);

    private void DrawingInk(string buttonName) => _state.StartDrawing(buttonName);

    private void Erasing(string buttonName) => _state.StartErasing(buttonName);

    public bool IsReleased => _state.IsReleased;
    public InkCanvasEditingMode InkEditingMode => _state.InkEditingMode;
    public string ButtonSelectedName => _state.ButtonSelectedName;

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
        _state.PropertyChanged += OnStateChanged;

        ReleasedButtonCommand = new RelayCommand<string>(ReleaseInk);
        DrawingButtonCommand = new RelayCommand<string>(DrawingInk);
        ErasingButtonCommand = new RelayCommand<string>(Erasing);
    }
}
