using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Entities;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Core
{
    class Render
    {
        public bool visionFlag { get; private set; } = Settings.selectedRender || CheatState.Vision;
        public bool fieldOnScreen = false;
        private DateTime lightEnd = DateTime.MinValue;

        //чит-костыль(мб убрать потом)
        public void Cheat()
        {

            Console.Clear();
            visionFlag = !visionFlag;


            if (visionFlag)
            {
                lightEnd = DateTime.Now.AddYears(1);
            }
            else
            {
                lightEnd = DateTime.MinValue;
            }
        }

        public void ActivateVision()
        {
            lightEnd = DateTime.Now.AddSeconds(5);
            visionFlag = true;
        }

        public void DrawOnlyMap(string[] map, Cursor cursor, Editor edit)
        {
            if (fieldOnScreen == false)
            {
                for (int i = 0; i < map.Length; i++)
                {
                    for (int j = 0; j < map[0].Length; j++)
                    {
                        Console.Write(map[i][j]);
                    }

                    if (i < map.Length - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }

            fieldOnScreen = true;

            Console.SetCursorPosition(cursor.entityX, cursor.entityY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(cursor.entity);
            Console.ResetColor();

            if (cursor.entityX != cursor.lastEntityX || cursor.entityY != cursor.lastEntityY)
            {
                Console.SetCursorPosition(cursor.lastEntityX, cursor.lastEntityY);
                Console.WriteLine(edit.GetCharOfCustomMap(cursor.lastEntityY, cursor.lastEntityX));
            }
        }

        public void Draw(GameMap map, Person person, List<Entity> list)
        {
            if (Settings.selectedRender == false)
            {
                if (visionFlag && DateTime.Now > lightEnd)
                {
                    visionFlag = false;
                    Console.Clear();
                }
            }

            if (visionFlag)
            {
                if (fieldOnScreen == false)
                {
                    for (int i = 0; i < map.mapHeight; i++)
                    {
                        for (int j = 0; j < map.mapWidth; j++)
                        {
                            Console.Write(map.GetCharOfMap(i, j));
                        }

                        if (i < map.mapHeight - 1)
                        {
                            Console.WriteLine();
                        }
                    }
                }

                fieldOnScreen = true;

                foreach (var ent in list)
                {
                    if (ent is Person p)
                    {
                        Console.SetCursorPosition(person.entityX, person.entityY);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(person.entity);
                        Console.ResetColor();

                        if (person.entityX != person.lastEntityX || person.entityY != person.lastEntityY)
                        {
                            Console.SetCursorPosition(person.lastEntityX, person.lastEntityY);
                            Console.WriteLine(map.GetCharOfMap(person.lastEntityY, person.lastEntityX));
                        }
                    }
                    if (ent is Light light)
                    {
                        if (light.entityX != -1 && light.entityY != -1)
                        {
                            Console.SetCursorPosition(light.entityX, light.entityY);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(light.entity);
                            Console.ResetColor();
                        }
                    }
                    if (ent is Monster monster)
                    {
                        Console.SetCursorPosition(monster.entityX, monster.entityY);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(monster.entity);
                        Console.ResetColor();

                        if (monster.entityX != monster.lastEntityX || monster.entityY != monster.lastEntityY)
                        {
                            Console.SetCursorPosition(monster.lastEntityX, monster.lastEntityY);
                            Console.WriteLine(map.GetCharOfMap(monster.lastEntityY, monster.lastEntityX));
                        }
                    }
                    if (ent is ExitSymbol exit)
                    {
                        Console.SetCursorPosition(exit.entityX, exit.entityY);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(exit.entity);
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                fieldOnScreen = false;
                List<int> cords = new List<int>();

                foreach (var ent in list)
                {
                    if (Math.Abs(ent.entityX - person.entityX) < 4 && Math.Abs(ent.entityY - person.entityY) < 4)
                    {
                        cords.Add(ent.entityX);
                        cords.Add(ent.entityY);

                        Console.SetCursorPosition(ent.entityX, ent.entityY);

                        if (ent is Person)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(ent.entity);
                            Console.ResetColor(); ;
                        }
                        if (ent is Light light)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(light.entity);
                            Console.ResetColor();
                        }
                        if (ent is Monster monster)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(monster.entity);
                            Console.ResetColor();
                        }
                        if (ent is ExitSymbol exit)
                        {
                            Console.SetCursorPosition(exit.entityX, exit.entityY);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(exit.entity);
                            Console.ResetColor();
                        }
                    }
                }

                for (int y = -4; y <= 4; y++)
                {
                    for (int x = -4; x <= 4; x++)
                    {
                        if (person.entityY + y >= 0 && person.entityY + y < map.mapHeight && person.entityX + x >= 0 && person.entityX + x < map.mapWidth)
                        {

                            bool isEntityHere = false;

                            for (int i = 0; i < cords.Count - 1; i += 2)
                            {
                                if (cords[i] == person.entityX + x && cords[i + 1] == person.entityY + y)
                                {
                                    isEntityHere = true;
                                    break;
                                }
                            }

                            if (isEntityHere)
                            {
                                continue;
                            }

                            Console.SetCursorPosition(person.entityX + x, person.entityY + y);

                            if (y == -4 || y == 4 || x == -4 || x == 4)
                            {
                                Console.Write(" ");
                            }
                            else
                            {
                                Console.Write(map.GetCharOfMap(person.entityY + y, person.entityX + x));
                            }
                        }
                    }
                }
            }
        }
    }
}
