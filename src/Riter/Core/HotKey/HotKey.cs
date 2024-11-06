namespace Riter.Core;
public enum HotKey
{
    Drawing = 9000,
    Erasing = 9001,
    HideAll = 9002,
    Trash = 9003,
    Undo = 9004,
    Redo = 9005,
    Highlighter = 9006,
    Release = 9007,
    SizeOfBrush1X = 9008,
    SizeOfBrush2X = 9009,
    SizeOfBrush3X = 9010,
    TransparentBackground = 9011,
    WhiteboardBackground = 9012,
    BlackboardBackground = 9013,

    Yellow = 9014,
    Purple = 9015,
    Mint = 9016,
    Coral = 9017,
    Red = 9018,
    Cyan = 9019,
    White = 9020,
    Orange = 9021,
    Gray = 9022,
    Black = 9023,

    Arrow = 1,
    Circle = 2,
    Rectangle = 3,
}

public record struct HotKeiesPressed(string Key, bool CtrlPressed, bool ShiftPressed);
