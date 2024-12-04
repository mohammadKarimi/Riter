using Riter.ViewModel.StateHandlers.Interfaces;

namespace Riter.ViewModel;
public class ScreenShotViewModel : BaseViewModel
{
    private readonly IScreenShotHandler _handler;

    public ScreenShotViewModel(IScreenShotHandler handler)
    {
        _handler = handler;
        _handler.PropertyChanged += OnStateChanged;
    }

    public ICommand TakeCommand => new RelayCommand(_handler.Take);
}
