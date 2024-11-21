using Riter.Core.Enum;
using Riter.Core.IO;

namespace Riter.ViewModel;
public class StartupLocationViewModel : BaseViewModel
{
    private readonly AppSettings _appSettings;
    private StartupLocation _startupLocation;

    public StartupLocationViewModel(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _startupLocation = _appSettings.StartupLocation;
    }

    public StartupLocation StartupLocation
    {
        get => _startupLocation;
        set
        {
            if (_startupLocation != value)
            {
                _startupLocation = value;
                OnPropertyChanged(nameof(StartupLocation));
            }
        }
    }

    public ICommand SetStartupLocationCommand => new RelayCommand<StartupLocation>(SetStartupLocation);

    private void SetStartupLocation(StartupLocation location)
    {
        if (_startupLocation != location)
        {
            StartupLocation = location;
            _appSettings.StartupLocation = location;
            Task.Run(async () =>
            {
                await FileStorage.SaveConfig(_appSettings).ConfigureAwait(false);
            });
        }
    }
}
