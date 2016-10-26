using System;
using WindowsAPI;

namespace FlashFlashRevolutionAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to begin");
            Console.ReadLine();
            IntPtr hWnd = Window.Get("Adobe Flash Player 16");
            FlashFlashRevolutionAI ai = new FlashFlashRevolutionAI(hWnd);
            System.Threading.Thread.Sleep(500);
            Window.SetFocused(hWnd);
            System.Threading.Thread.Sleep(500);
            ai.Play();
        }
    }
}
