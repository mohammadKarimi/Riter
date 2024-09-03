using System.ComponentModel;

namespace Riter.Main.Core;

public class PalleteStateViewModel : INotifyPropertyChanged
{
    public PalleteState _state;

    public event PropertyChangedEventHandler PropertyChanged;

}

public class PalleteState
{
    public int IsReleased { get; set; }
}
