using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Entities;
using CaveGame.Core;
using CaveGame.Edit;

namespace CaveGame.Menu
{
    class GameMenu
    {
        public int selectedButton = 0;
        public string play { get; } = "Играть";
        public string settings { get; } = "Настройки";
        public string quit { get; } = "Выйти";
        public string redactor { get; } = "Редактор";
        public string symbol { get; } = "<";

        public int width = Console.WindowWidth;
        public int height = Console.WindowHeight;

        private string[,] menus = new string[5, 14]
            {
    { "#"," ","#"," ","#","#"," ","#","#","#"," ","#"," ","#" },
    { "#","#","#"," ","#"," "," ","#"," ","#"," ","#"," ","#" },
    { "#","#","#"," ","#","#"," ","#"," ","#"," ","#"," ","#" },
    { "#"," ","#"," ","#"," "," ","#"," ","#"," ","#"," ","#" },
    { "#"," ","#"," ","#","#"," ","#"," ","#"," ","#","#","#" }
            };

        public void ShowMenuWord()
        {

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - (14 / 2), i);

                for (int j = 0; j < 14; j++)
                {
                    Console.Write(menus[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void ShowMenu()
        {
            int centerX = (Console.WindowWidth / 2) - (play.Length / 2);
            int centerY = (Console.WindowHeight / 2) - 1;

            Console.SetCursorPosition(centerX, centerY);
            Console.WriteLine(play + (selectedButton == 0 ? symbol + "  " : "  "));

            Console.SetCursorPosition(centerX, centerY + 1);
            Console.WriteLine(settings + (selectedButton == 1 ? symbol + "  " : "  "));

            Console.SetCursorPosition(centerX, centerY + 2);
            Console.WriteLine(redactor + (selectedButton == 2 ? symbol + "  " : "  "));

            Console.SetCursorPosition(centerX, centerY + 3);
            Console.WriteLine(quit + (selectedButton == 3 ? symbol + "  " : "  "));
        }

        public int GetInputMenu()
        {
            ShowMenuWord();

            while (true)
            {
                if (width != Console.WindowWidth || height != Console.WindowHeight) // не доделал
                {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;
                    Console.Clear();
                    ShowMenuWord();
                }

                ShowMenu();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        if (selectedButton == 0)
                        {
                            return 0;
                        }
                        else if (selectedButton == 1)
                        {
                            return 1;
                        }
                        else if (selectedButton == 2)
                        {
                            return 2;
                        }
                        else
                        {
                            return 3;
                        }
                    case ConsoleKey.W:
                        if (selectedButton > 0)
                        {
                            selectedButton--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (selectedButton < 3)
                        {
                            selectedButton++;
                        }
                        break;
                }
            }
        }
    }
}