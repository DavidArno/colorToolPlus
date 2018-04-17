using System;
using System.Runtime.InteropServices;

namespace ColorToolPlus
{
    internal static class ConsoleColors
    {
        internal static ConsoleColor PopupBackgroundColor
        {
            get => GetPopupBackgroundColor();
            set => SetPopupBackgroundColor(value);
        }

        internal static ConsoleColor PopupForegroundColor
        {
            get => GetPopupForegroundColor();
            set => SetPopupForegroundColor(value);
        }

        private static ConsoleColor GetPopupBackgroundColor()
        {
            var (_, consoleScreenBufferInfo) = GetCurrentConsoleHandleAndBuffer();
            var popupAttributes = consoleScreenBufferInfo.wPopupAttributes;

            return (ConsoleColor)((popupAttributes & AttributeColors.BackgroundMask) >> 4);
        }

        private static ConsoleColor GetPopupForegroundColor()
        {
            var (_, consoleScreenBufferInfo) = GetCurrentConsoleHandleAndBuffer();
            var popupAttributes = consoleScreenBufferInfo.wPopupAttributes;

            return (ConsoleColor)(popupAttributes & AttributeColors.ForegroundMask);
        }

        private static void SetPopupBackgroundColor(ConsoleColor consoleColor)
        {
            var (handle, consoleScreenBufferInfo) = GetCurrentConsoleHandleAndBuffer();
            var popupAttributes = consoleScreenBufferInfo.wPopupAttributes;

            var popupAttributesLessBackgroundColor = (ushort)(popupAttributes & ~AttributeColors.BackgroundMask);

            consoleScreenBufferInfo.wPopupAttributes =
                (ushort)(popupAttributesLessBackgroundColor | ((ushort)consoleColor << 4));

            SetConsoleScreenBufferInfoEx(handle, ref consoleScreenBufferInfo);
        }

        private static void SetPopupForegroundColor(ConsoleColor consoleColor)
        {
            var (handle, consoleScreenBufferInfo) = GetCurrentConsoleHandleAndBuffer();
            var popupAttributes = consoleScreenBufferInfo.wPopupAttributes;

            var popupAttributesLessForegroundColor = (ushort)(popupAttributes & ~AttributeColors.ForegroundMask);

            consoleScreenBufferInfo.wPopupAttributes =
                (ushort)(popupAttributesLessForegroundColor | (ushort)consoleColor);

            SetConsoleScreenBufferInfoEx(handle, ref consoleScreenBufferInfo);
        }

        private static (IntPtr handle, ConsoleScreenBufferInfoEx screenBuffer) GetCurrentConsoleHandleAndBuffer()
        {
            var consoleScreenBufferInfoEx = CreateConsoleScreenBufferInfoEx();
            var handle = GetStdHandle(StdOutputHandle);
            GetConsoleScreenBufferInfoEx(handle, ref consoleScreenBufferInfoEx);
            return (handle, consoleScreenBufferInfoEx);
        }

        // https://docs.microsoft.com/en-us/windows/console/getstdhandle
        private const int StdOutputHandle = -11;


        // https://github.com/wine-mirror/wine/blob/master/include/wincon.h
        private static class AttributeColors
        {
            internal const ushort Black = 0;
            internal const ushort ForegroundBlue = 0x0001;
            internal const ushort ForegroundGreen = 0x0002;
            internal const ushort ForegroundRed = 0x0004;
            internal const ushort ForegroundIntensity = 0x0008;
            internal const ushort BackgroundBlue = 0x0010;
            internal const ushort BackgroundGreen = 0x0020;
            internal const ushort BackgroundRed = 0x0040;
            internal const ushort BackgroundIntensity = 0x0080;
            internal const ushort ForegroundMask = ForegroundBlue | ForegroundGreen | ForegroundRed | ForegroundIntensity;
            internal const ushort BackgroundMask = BackgroundBlue | BackgroundGreen | BackgroundRed | BackgroundIntensity;
            internal const ushort ColorMask = BackgroundMask | ForegroundMask;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Coord
        {
            private readonly short X;
            private readonly short Y;
        }

        private struct SmallRect
        {
            private short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ConsoleScreenBufferInfoEx
        {
            public uint cbSize;
            public Coord dwSize;
            public Coord dwCursorPosition;
            public short wAttributes;

            public SmallRect srWindow;
            public Coord dwMaximumWindowSize;
            public ushort wPopupAttributes;
            public bool bFullscreenSupported;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public uint[] ColorTable;

        }

        private static ConsoleScreenBufferInfoEx CreateConsoleScreenBufferInfoEx() => new ConsoleScreenBufferInfoEx { cbSize = 96 };


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleScreenBufferInfoEx(IntPtr consoleOutput,
                                                                ref ConsoleScreenBufferInfoEx consoleScreenBufferInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleScreenBufferInfoEx(IntPtr consoleOutput,
                                                                ref ConsoleScreenBufferInfoEx consoleScreenBufferInfo);

        private const int ColorTableSize = 16;

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    }
}




//function Set-ConsoleOpacity
//{
//param(
//    [ValidateRange(10,100)]
//    [int]$Opacity
//)

//# Check if pinvoke type already exists, if not import the relevant functions
//try {
//    $Win32Type = [Win32.WindowLayer]
//} catch {
//$Win32Type = Add-Type -MemberDefinition @'
//'@ -Name WindowLayer -Namespace Win32 -PassThru
//}

//# Calculate opacity value (0-255)
//$OpacityValue = [int] ($Opacity* 2.56) - 1

//# Grab the host windows handle
//$ThisProcess = Get-Process -Id $PID
//$WindowHandle = $ThisProcess.MainWindowHandle

//# "Constants"
//$GwlExStyle  = -20;
//$WsExLayered = 0x80000;
//$LwaAlpha    = 0x2;

//if($Win32Type::GetWindowLong($WindowHandle,-20) -band $WsExLayered -ne $WsExLayered){
//# If Window isn't already marked "Layered", make it so
//[void]$Win32Type::SetWindowLong($WindowHandle,$GwlExStyle,$Win32Type::GetWindowLong($WindowHandle,$GwlExStyle) -bxor $WsExLayered)
//}

//# Set transparency
//[void]$Win32Type::SetLayeredWindowAttributes($WindowHandle,0,$OpacityValue,$LwaAlpha)
//}