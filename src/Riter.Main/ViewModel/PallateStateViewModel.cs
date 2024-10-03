using System.ComponentModel;
using System.Windows.Controls;
using Riter.Main.Core;

namespace Riter.Main.ViewModel;

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel : INotifyPropertyChanged
{
    private readonly PalleteState _state;

    /// <summary>
    /// This event is for subscribing the UI on it to get any state changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets a value indicating whether the ink has been released.
    /// </summary>
    public bool IsReleased => _state.IsReleased;

    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    public InkCanvasEditingMode InkEditingMode => _state.InkEditingMode;

    /// <summary>
    /// Gets the name of the button that is currently selected.
    /// </summary>
    public string ButtonSelectedName => _state.ButtonSelectedName;

    /// <summary>
    /// Raises the PropertyChanged event when a property value changes.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Handles the state change event and raises the PropertyChanged event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Property changed event arguments.</param>
    private void OnStateChanged(object sender, PropertyChangedEventArgs e)
        => OnPropertyChanged(e.PropertyName);

    /// <summary>
    /// Releases the ink based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to release ink.</param>
    private void ReleaseInk(string buttonName)
        => _state.Release(buttonName);

    /// <summary>
    /// Starts drawing ink based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to start drawing ink.</param>
    private void DrawingInk(string buttonName)
        => _state.StartDrawing(buttonName);

    /// <summary>
    /// Starts erasing based on the button pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed to start erasing.</param>
    private void Erasing(string buttonName)
        => _state.StartErasing(buttonName);
}

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PalleteStateViewModel"/> class.
    /// </summary>
    /// <param name="palleteState">The PalleteState model used to manage ink states.</param>
    public PalleteStateViewModel(PalleteState palleteState)
    {
        _state = palleteState;
        _state.PropertyChanged += OnStateChanged;

        ReleasedButtonCommand = new RelayCommand<string>(ReleaseInk);
        DrawingButtonCommand = new RelayCommand<string>(DrawingInk);
        ErasingButtonCommand = new RelayCommand<string>(Erasing);
    }

    /// <summary>
    /// Gets the command that is executed when the released button is pressed.
    /// </summary>
    public ICommand ReleasedButtonCommand { get; private set; }

    /// <summary>
    /// Gets the command that is executed when the drawing button is pressed.
    /// </summary>
    public ICommand DrawingButtonCommand { get; private set; }

    /// <summary>
    /// Gets the command that is executed when the erasing button is pressed.
    /// </summary>
    public ICommand ErasingButtonCommand { get; private set; }
}
