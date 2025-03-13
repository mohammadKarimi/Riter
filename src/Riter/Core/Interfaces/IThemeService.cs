using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riter.Core.Interfaces;
public interface IThemeService
{
    public void ToggleTheme();

    public string GetCurrentThemeName();

    public string GetCurrentThemeHex();
}
