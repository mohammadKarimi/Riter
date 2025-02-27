using System.Reflection;

namespace Riter.Core;
public class ApplicationVersionMapper
{
    public static string GetVersion()
    {
        Version version = Assembly.GetExecutingAssembly().GetName().Version;
        return $"v{version.Major}.{version.Minor}.{version.Build}";
    }

    public static string GetFullVersion() => $"riter-{GetVersion()}-windows-self";
}
