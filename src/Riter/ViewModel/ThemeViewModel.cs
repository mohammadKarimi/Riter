using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riter.Core.Interfaces;
using Riter.Services;

namespace Riter.ViewModel;
public class ThemeViewModel(IThemeService themeService) : BaseViewModel
{
    private readonly IThemeService _themeService = themeService;

    public string CurrentThemeName
    {
        get
        {
            return _themeService.GetCurrentThemeName();
        }
    }

    public string CurrentThemeHex
    {
        get
        {
            return _themeService.GetCurrentThemeHex();
        }
    }

    public ICommand ToggleCommand => new RelayCommand(() =>
    {
        _themeService.ToggleTheme();
        OnPropertyChanged(nameof(CurrentThemeName));
    });
}
