//using System;
//using System.Diagnostics;
//using System.Runtime.InteropServices;

//namespace ConsoleBattleship
//{
//  /**
//   * Courtesy of https://www.codeproject.com/Questions/217948/I-need-to-capture-mouse-events-in-Console
//   */
//  partial class Screen
//  {
//    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
//    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//    [return: MarshalAs(UnmanagedType.Bool)]
//    private static extern bool UnhookWindowsHookEx(IntPtr hhk);
//    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
//    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//    private static extern IntPtr GetModuleHandle(string lpModuleName);

//    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
//    private const int WH_MOUSE_LL = 14;

//    private static LowLevelMouseProc _proc = HookCallback;
//    private static IntPtr _hookID = IntPtr.Zero;

//    static void Main(string[] args)
//    {
//      _hookID = SetHook(_proc);

//      //System.Windows.Forms.Application.Run();

//      UnhookWindowsHookEx(_hookID);

//      //Console.Read();
//    }

//    private static IntPtr SetHook(LowLevelMouseProc proc)
//    {
//      using (Process curProcess = Process.GetCurrentProcess())
//      using (ProcessModule curModule = curProcess.MainModule)
//      {
//        return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
//      }
//    }

//    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
//    {
//      if (nCode >= 0 && MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam)
//      {
//        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
//        Console.WriteLine(hookStruct.pt.x + ", " + hookStruct.pt.y);
//      }

//      return CallNextHookEx(_hookID, nCode, wParam, lParam);
//    }

//    private enum MouseMessages
//    {
//      WM_LBUTTONDOWN = 0x0201,
//      WM_LBUTTONUP = 0x0202,
//      WM_MOUSEMOVE = 0x0200,
//      WM_MOUSEWHEEL = 0x020A,
//      WM_RBUTTONDOWN = 0x0204,
//      WM_RBUTTONUP = 0x0205
//    }


//    [StructLayout(LayoutKind.Sequential)]
//    private struct POINT
//    {
//      public int x;
//      public int y;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    private struct MSLLHOOKSTRUCT
//    {
//      public POINT pt;
//      public uint mouseData;
//      public uint flags;
//      public uint time;
//      public IntPtr dwExtraInfo;
//    }

//  }
//}