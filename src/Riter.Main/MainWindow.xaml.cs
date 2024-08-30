using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Riter.Main;

public partial class MainWindow : Window
{
    bool _release = false;
    public MainWindow()
    {
        InitializeComponent();
        Release(false);
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

