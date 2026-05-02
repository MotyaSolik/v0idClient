using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Core;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Entities
{
    class Light : Entity
    {
        private static Random rnd = new Random();

        public Light(GameMap map, char entity)
        {
            if (Settings.selectedRender == false)
            {
                while (true)
                {
                    entityX = rnd.Next(1, map.mapWidth - 2);
                    entityY = rnd.Next(1, map.mapHeight - 2);

                    if (map.GetCharOfMap(entityY, entityX) != '#')
                    {
                        this.entity = entity;
                        break;
                    }
                }
            }
        }
    }
}
