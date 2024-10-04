using System.Windows.Controls;

namespace Riter.Core.UI;

/// <summary>
/// Interaction logic for ApplicationSideboxControl.xaml.
/// </summary>
public partial class WindowControl : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowControl"/> class.
    /// Mange the Minimize and Close application.
    /// </summary>
    public WindowControl()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Minimize The Window and all drawings.
    /// </summary>
    /// <param name="sender">The source of the event, typically the Exit button.</param>
    /// <param name="e">The event data associated with the button click.</param>
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        => Window.GetWindow(this).WindowState = WindowState.Minimized;

    /// <summary>
    /// Handles the click event for the Exit button.
    /// If the drawing settings are saved, the application will exit.
    /// </summary>
    /// <param name="sender">The source of the event, typically the Exit button.</param>
    /// <param name="e">The event data associated with the button click.</param>
    private void ExitButton_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown(0);
}
