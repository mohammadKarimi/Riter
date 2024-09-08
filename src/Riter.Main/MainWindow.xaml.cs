using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Main.Core;
using Riter.Main.Core.Extensions;

namespace Riter.Main;

public partial class MainWindow : Window
{
    private readonly PalleteStateViewModel _pallateStateViewModel;
    public MainWindow(PalleteStateViewModel pallateStateViewModel)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        _pallateStateViewModel = pallateStateViewModel;
        this.SetEventListeners()
            .SetTopMost(true)
            .SetDefaultColor()
            .SetBrushSize();
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
    {
        _pallateStateViewModel.ClearHistory();
        MainInkCanvas.Strokes.Clear();
    }

    private void EraserButton_Click(object sender,EventArgs e)
    {
        var s = MainInkCanvas.EraserShape.Height;
        MainInkCanvas.EraserShape = new EllipseStylusShape(s, s);
        MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
    }

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

    private void DrawButton_Click(object sender, RoutedEventArgs e)
    {
        MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
    }
}
