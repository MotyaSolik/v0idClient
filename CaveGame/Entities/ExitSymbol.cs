using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Core;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Entities
{
    class ExitSymbol : Entity
    {
        private static Random rnd = new Random();

        public ExitSymbol(GameMap map)
        {
            while (true)
            {
                entityX = map.mapWidth - 2;
                entityY = rnd.Next(1, map.mapHeight - 2);

                if (map.GetCharOfMap(entityY, entityX) != '#')
                {
                    entity = 'X';
                    break;
                }
            }
        }

        public bool EndOfGame()
        {
            if (entityY == -1)
            {
                return false;
            }

            return true;
        }
    }
}
