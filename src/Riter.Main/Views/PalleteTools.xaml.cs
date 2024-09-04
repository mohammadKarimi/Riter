using System.Windows.Controls;
using Riter.Main.Core;

namespace Riter.Main.Views;

/// <summary>
/// Interaction logic for PalleteTools.xaml
/// </summary>
public partial class PalleteTools : UserControl
{
    public PalleteTools(PalleteStateViewModel palleteStateViewModel)
    {
        InitializeComponent();
        DataContext = palleteStateViewModel;
    }
}
