namespace Riter.Core;

public class Value
{
    public string Modifiers { get; init; }

    public string Key { get; init; }
}

public class HotKeysConfig
{
    public string Key { get; init; }

    public Value Value { get; init; }
}
