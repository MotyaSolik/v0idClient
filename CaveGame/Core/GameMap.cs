using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Entities;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Core
{
    class GameMap
    {
        private string[] map;

        public GameMap(string[] customMap)
        {
            map = new string[customMap.Length];
            Array.Copy(customMap, map, customMap.Length);
        }
        
        public GameMap()
        {
                map = new string[]
                {
            "########################################################################################################################",
            "#        #######                  #                              #             #                  #                    #",
            "#        #     #        #####     #        ######                                                 #                    #",
            "#        #     #     ####         #   #####                                                       #                    #",
            "#        #     #                  #                              #             #                  #                    #",
            "#              #######                                           #             #                  #                    #",
            "#                                 #   #             ########     #             #                                       #",
            "#                                 #   #             #            #     #       #     #########  ################   #####",
            "#                         ####    #   ####          #            #     #       #######                                 #",
            "#        #                #       #           #######            #      #      #          #                  #         #",
            "#        ###########      #       #   ####          #            #      #      #          #                  #         #",
            "#####              #      #       #   #             #            #      #      #          #                  #         #",
            "#           #      #              ###               #            #             #     ######                  #         #",
            "###         #      #                        #                                           #                    #         #",
            "#        #####     #              ###        #                   #             #        #                              #",
            "#        #         #      #       #           #         #        #             #                                       #",
            "#        #                #       #            #        ##########             #          #########                    #",
            "#                  #      ####    #                     #        #             #          #                            #",
            "#         #        #      #       #      #              #        #             #          #                            #",
            "#         #        #              #      #                       #             #          #                            #",
            "#    ######        #                     #        #######        #             #          #            ##########  #####",
            "#         #        #              #      #     ####   #                        #          #            #               #",
            "#         #        ########  ######                   #          #    ####     #                       #               #",
            "#         #        #              #                   #          #  ###        #          #            #               #",
            "#         #        #              #     ###           #          #             #          #            #               #",
            "#   #       #      #              #       #         ######       #             #          #                            #",
            "#   #       #      #                      #                      #             #          #            #               #",
            "#   #       #      #              #       #                      #             #          #            #               #",
            "########################################################################################################################"
                };
        }


        public int mapWidth => map[0].Length;
        public int mapHeight => map.Length;

        public (int x, int y) FindSymbol(char symbol)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    if (map[y][x] == symbol)
                    {
                        return (x, y);
                    }
                }
            }
            return (-1, -1);
        }

        public void EraseSymbol(int x, int y)
        {
            map[y] = map[y].Remove(x, 1).Insert(x, " ");
        }

        public char GetCharOfMap(int y, int x)
        {
            return map[y][x];
        }
    }
}
