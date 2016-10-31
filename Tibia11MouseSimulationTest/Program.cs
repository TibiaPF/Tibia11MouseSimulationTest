using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tibia11MouseSimulationTest
{
    class Program
    {

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(HandleRef hWnd, WindowsMessages.WM Msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        const int MAPVK_VK_TO_VSC = 0x00;
        const int MAPVK_VSC_TO_VK = 0x01;
        const int MAPVK_VK_TO_CHAR = 0x02;
        const int MAPVK_VSC_TO_VK_EX = 0x03;
        const int MAPVK_VK_TO_VSC_EX = 0x04;

        static void KeyUp(HandleRef hwnd, uint vk_key)
        {
            PostMessage(hwnd, WindowsMessages.WM.KEYUP, vk_key, (MapVirtualKey(vk_key, MAPVK_VSC_TO_VK)) * 0x10000 + 0xC0000000 + 1);
        }

        static void KeyDown(HandleRef hwnd, uint vk_key)
        {
            PostMessage(hwnd, WindowsMessages.WM.KEYDOWN, vk_key, (MapVirtualKey(vk_key, MAPVK_VSC_TO_VK)) * 0x10000 + 1);
        }

        static void KeyPress(HandleRef hwnd, uint vk_key)
        {
            KeyDown(hwnd, vk_key);
            KeyUp(hwnd, vk_key);
        }

        static void Main(string[] args)
        {
            //simple ctrl+f2 test

            Process tibia = Process.GetProcessesByName("client").FirstOrDefault();

            HandleRef handleRef = new HandleRef(tibia, tibia.MainWindowHandle);

            KeyUp(handleRef, 0x11);
            KeyUp(handleRef, 0x71);            
            KeyDown(handleRef, 0x71);
            KeyDown(handleRef, 0x11);
            Console.ReadKey();
        }


       
    }
}
