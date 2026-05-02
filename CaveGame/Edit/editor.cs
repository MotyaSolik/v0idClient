using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Entities;
using CaveGame.Core;

namespace CaveGame.Edit
{
    class Editor
    {
        public int selectedButton = 0;
        public bool switchActiveInput = true;
        public string symbol { get; } = "<";
        public int mapWidth { get; } = 119;
        public int mapHeight { get; } = 29;
        private char[] symbolList = new char[]
        {
            '#', '@', '&', 'X', '*'
        };

        private int playerY = -1;
        private int playerX = -1;
        private int lightY = -1;
        private int lightX = -1;
        private int exitY = -1;
        private int exitX = -1;
        public string Exit { get; set; } = "Выйти";
        public string Save { get; set; } = "Сохранить";
        public int selectedSymbol { get; set; } = 0;

        public string[] customMap { get; } = new string[]
        {
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        ",
    "                                                                                                                        "
        };

        public void TryDraw(int y, int x)
        {
            if (symbolList[selectedSymbol] == '@')
            {
                if (playerY != -1)
                {
                    TryErase(playerY, playerX);
                }
                playerY = y;
                playerX = x;
            }
            else if (symbolList[selectedSymbol] == '*')
            {
                if (lightY != -1)
                {
                    TryErase(lightY, lightX);
                }
                lightY = y;
                lightX = x;
            }
            else if (symbolList[selectedSymbol] == 'X')
            {
                if (exitY != -1)
                {
                    TryErase(exitY, exitX);
                }
                exitY = y;
                exitX = x;
            }


            char[] row = customMap[y].ToCharArray();
            row[x] = symbolList[selectedSymbol];
            customMap[y] = new string(row);
        }

        public void TryErase(int y, int x)
        {
            char[] row = customMap[y].ToCharArray();
            row[x] = ' ';
            customMap[y] = new string(row);

            Console.SetCursorPosition(x, y);
            Console.Write(' ');

        }

        public char SwitchSymbol()
        {
            selectedSymbol = (selectedSymbol + 1) % symbolList.Length;
            return symbolList[selectedSymbol];
        }

        public char GetCharOfCustomMap(int y, int x)
        {
            return customMap[y][x];
        }

        public string GetFileName()
        {
            Console.CursorVisible = true;
            int centerX = (Console.WindowWidth / 2) - ("Имя карты: ".Length);
            int centerY = (Console.WindowHeight / 2);
            Console.SetCursorPosition(centerX, centerY);
            Console.Write("Имя карты: ");
            string name = Console.ReadLine();
            Console.CursorVisible = false;
            return name;
        }

        public string[] GetCustomMaps()
        {
            string mapsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maps");

            if (!Directory.Exists(mapsPath))
            {
                return new string[0];
            }

            string[] files = Directory.GetFiles(mapsPath, "*.txt");

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileNameWithoutExtension(files[i]);
            }

            return files;
        }

        public void SaveMap(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maps", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, customMap);
        }

        public void LoadMap(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maps", fileName);
            string[] loadedMap = File.ReadAllLines(path);
            for (int i = 0; i < customMap.Length && i < loadedMap.Length; i++)
            {
                customMap[i] = loadedMap[i];
            }
        }

        public void ClearMap()
        {
            for (int i = 0; i < customMap.Length; i++)
            {
                customMap[i] = new string(' ', mapWidth);
            }
        }

        public void ShowRedactorMenu()
        {
            Console.SetCursorPosition(0, customMap.Length);
            Console.Write(Save + (selectedButton == 0 ? symbol : "   "));
            Console.SetCursorPosition(Save.Length + 3, customMap.Length);
            Console.Write(Exit + (selectedButton == 1 ? symbol : " "));
        }
        public int GetInputRedactor()
        {

            while (true)
            {
                ShowRedactorMenu();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        return 2;
                    case ConsoleKey.A:
                        if (selectedButton > 0)
                        {
                            selectedButton--;
                        }
                        break;
                    case ConsoleKey.D:
                        if (selectedButton < 1)
                        {
                            selectedButton++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (selectedButton == 0)
                        {
                            return 0;
                        }
                        return 1;
                }
            }
        }
    }
}