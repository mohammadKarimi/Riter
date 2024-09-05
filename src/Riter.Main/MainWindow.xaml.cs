using Riter.Main.Core;

namespace Riter.Main;

public partial class MainWindow : Window
{
    public MainWindow(PalleteStateViewModel pallateStateViewModel)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        Topmost = true;
    }

    private void ShortcutKeyDown(object sender, KeyEventArgs e)
    {

    }

    /// <summary>
    /// Clear History and Clear Canvas Ink.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrashButton_Click(object sender, EventArgs e)
        => MainInkCanvas.Strokes.Clear();

    /// <summary>
    /// Minimize The Window and all drawings.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;

    /// <summary>
    /// Save The draw if setting available then, Exit Application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExitButton_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown(0);
}

