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
    /// Save The draw if setting available then, Exit Application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnExit_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown(0);
}

