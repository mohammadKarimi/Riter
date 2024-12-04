using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers.Interfaces;
public interface IScreenShotHandler : INotifyPropertyChanged
{
    void Take();
}
