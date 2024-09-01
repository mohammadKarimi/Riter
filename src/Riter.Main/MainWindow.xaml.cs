using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Riter.Main.Core;

namespace Riter.Main;

public partial class MainWindow(PallateStateViewModel pallateState) : Window
{
    private PalleteState _palleteState = null;
    public MainWindow()
    {
        InitializeComponent();
        DataContext = pallateState;
        Release(false);
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

    private void BtnLock_Click(object sender, RoutedEventArgs e)
    {
        _release = !_release;
        Release(_release);
    }
}

