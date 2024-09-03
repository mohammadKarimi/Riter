using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Riter.Main.Core;

namespace Riter.Main;

public partial class MainWindow : Window
{
    public MainWindow(PalleteStateViewModel pallateStateViewModel)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        //Release(false);
        Topmost = true;
    }

    private void Release(bool release)
    {
        if (release)
        {
            MainInkCanvas.EditingMode = InkCanvasEditingMode.None;
            Background = Application.Current.Resources["Transparent"] as Brush;
        }
        else
        {
            Background = Application.Current.Resources["NoneTransparent"] as Brush;
            MainInkCanvas.UseCustomCursor = false;
            MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }
    }
    private void ShortcutKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void BtnRelease_Click(object sender, RoutedEventArgs e)
    {

    }

    /// <summary>
    /// Save The draw if setting available then, Exit Application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnExit_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown(0);
}

