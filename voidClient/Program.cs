using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace v0idClient
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        static bool noclip = false;
        static bool godmode = false;
        static bool vision = false;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "v0idClient";
            DrawMenu();

            while (true)
            {
                if ((GetAsyncKeyState(0x70) & 0x8000) != 0) // F1
                {
                    noclip = !noclip;
                    DrawMenu();
                    System.Threading.Thread.Sleep(300);
                }
                if ((GetAsyncKeyState(0x71) & 0x8000) != 0) // F2
                {
                    godmode = !godmode;
                    DrawMenu();
                    System.Threading.Thread.Sleep(300);
                }
                if ((GetAsyncKeyState(0x74) & 0x8000) != 0) // F5
                {
                    LaunchGame();
                    System.Threading.Thread.Sleep(500);
                }
                if ((GetAsyncKeyState(0x72) & 0x8000) != 0) // F3
                {
                    vision = !vision;
                    DrawMenu();
                    System.Threading.Thread.Sleep(300);
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        static void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine("=== v0idClient ===");
            Console.WriteLine($"[F1] Noclip  - {(noclip ? "ON " : "OFF")}");
            Console.WriteLine($"[F2] Godmode - {(godmode ? "ON " : "OFF")}");
            Console.WriteLine($"[F3] Vision  - {(vision ? "ON " : "OFF")} (space to activate)");
            Console.WriteLine("==================");
            Console.WriteLine("[F5] Запустить CaveGame");
        }

        static void LaunchGame()
        {
            string exePath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "source", "CaveGame.exe");

            var arguments = new System.Collections.Generic.List<string>();
            if (noclip) arguments.Add("--noclip");
            if (godmode) arguments.Add("--godmode");
            if (vision) arguments.Add("--vision");

            Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = string.Join(" ", arguments),
                UseShellExecute = true
            });
        }
    }
}