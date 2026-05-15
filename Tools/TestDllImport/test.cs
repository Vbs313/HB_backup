using System;
using System.Runtime.InteropServices;
class Test {
    [DllImport("kernel32.dll")] static extern IntPtr GetModuleHandle(string n);
    [DllImport("kernel32.dll")] static extern IntPtr GetProcAddress(IntPtr m, string n);
    static void Main() {
        var h = GetModuleHandle("user32.dll");
        Console.WriteLine("user32 handle: " + h);
        if (h != IntPtr.Zero) {
            var p = GetProcAddress(h, "GetActiveWindow");
            Console.WriteLine("GetActiveWindow: " + p);
        }
    }
}
