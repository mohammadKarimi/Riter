namespace Riter.Main.Core;

public class PalleteState
{
    private static PalleteState _instance { get; set; }
    private static object _lock { get; set; }

    private PalleteState()
    {
    }

    public static PalleteState Init()
    {
        lock (_lock)
        {
            if (_instance is not null)
            {
                return _instance;
            }
            _instance = new PalleteState();
            return _instance;
        }
    }

    public int IsReleased { get; set; }
}
