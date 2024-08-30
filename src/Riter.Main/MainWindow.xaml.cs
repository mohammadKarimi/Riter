using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Riter.Main;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
       
    }

    private void ShortcutKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void BtnLock_Click(object sender, RoutedEventArgs e)
    {
        Background = Application.Current.Resources["NoneTransparentColor"] as Brush; 
        MainInkCanvas.UseCustomCursor = false;
        MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
       
    }
}

