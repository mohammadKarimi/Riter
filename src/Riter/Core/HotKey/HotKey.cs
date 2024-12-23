namespace Riter.Core;
public enum HotKey
{
    Drawing = 100,
    Erasing = 101,
    HideAll = 102,
    Trash = 103,
    Undo = 104,
    Redo = 105,
    Highlighter = 106,
    Release = 107,

    SizeOfBrush07X = 200,
    SizeOfBrush1X = 201,
    SizeOfBrush2X = 202,
    SizeOfBrush3X = 203,

    TransparentBackground = 300,
    WhiteboardBackground = 301,
    BlackboardBackground = 302,

    Yellow = 400,
    Purple = 401,
    Mint = 402,
    Coral = 403,
    Red = 404,
    Cyan = 405,
    Pink = 406,
    Gray = 407,
    Black = 408,
    Rainbow = 409,

    Arrow = 500,
    Circle = 501,
    Rectangle = 502,
    Database = 503,
    Line = 504,
    FadeInk = 505,
    FilledCircle = 506,
    FilledRectangle = 507,

    ScreenShot = 600,
}

public record struct HotKeiesPressed(string Key, bool CtrlPressed, bool ShiftPressed);
