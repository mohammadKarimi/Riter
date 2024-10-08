using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core;

namespace Riter.ViewModel;

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
    /// Gets a value indicating whether gets the value of IsHideAll props to show or hide the strokes.
    /// </summary>
    public bool IsHideAll => _state.IsHideAll;

    /// <summary>
    /// Gets a value indicating whether gets the value of IsHideAll props to show or hide the strokes.
    /// </summary>
    public bool IsSettingPanelOpened => _state.IsSettingPanelOpened;

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

    /// <summary>
    /// Open Github Project in Broswer.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed.</param>
    private void OpenGithubProject(string buttonName) => _ = Process.Start(new ProcessStartInfo
    {
        FileName = App.ServiceProvider.GetService<AppSettings>().GitHubProjectUrl,
        UseShellExecute = true,
    });

    /// <summary>
    /// Hide All Strokes in Main Ink.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed.</param>
    private void HideAll(string buttonName) => _state.HideAll();

    private void OpenSetting(string buttonName) => _state.OpenSetting(buttonName);
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
        SourceCodeButtonCommand = new RelayCommand<string>(OpenGithubProject);
        HideAllButtonCommand = new RelayCommand<string>(HideAll);
        SettingButtonCommand = new RelayCommand<string>(OpenSetting);
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

    /// <summary>
    /// Gets open Github Project in Broswer.
    /// </summary>
    public ICommand SourceCodeButtonCommand { get; private set; }

    /// <summary>
    /// Gets HideAll Strokes in MainInk.
    /// </summary>
    public ICommand HideAllButtonCommand { get; private set; }

    /// <summary>
    /// Gets SettingButton.
    /// </summary>
    public ICommand SettingButtonCommand { get; private set; }
}
