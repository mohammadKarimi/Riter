using System.Runtime.InteropServices;
using System.Text;

namespace Riter.Core;

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

public static class KeyConverter
{
    [DllImport("user32.dll")]
    private static extern int ToUnicode(
        uint wVirtKey,
        uint wScanCode,
        byte[] lpKeyState,
        [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff,
        int cchBuff,
        uint wFlags);

    [DllImport("user32.dll")]
    private static extern bool GetKeyboardState(byte[] lpKeyState);

    [DllImport("user32.dll")]
    private static extern uint MapVirtualKey(uint uCode, uint uMapType);

    public static string VkCodeToString(int vkCode)
    {
        var keyState = new byte[256];
        if (!GetKeyboardState(keyState))
        {
            return string.Empty;
        }

        // Ensure the shift key state is set if necessary
        if ((vkCode >= 'A' && vkCode <= 'Z') || (vkCode >= '0' && vkCode <= '9'))
        {
            keyState[(int)Key.LeftShift] = 0x80;
        }

        var scanCode = MapVirtualKey((uint)vkCode, 0);
        var sb = new StringBuilder(5);
        int result = ToUnicode((uint)vkCode, scanCode, keyState, sb, sb.Capacity, 0);

        return result > 0 ? sb.ToString() : string.Empty;
    }
}
