using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Core;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Entities
{
    class Monster : Entity
    {
        private static Random rnd = new Random();

        public Monster(GameMap map)
        {
            entity = '&';

            while (map.GetCharOfMap(entityY, entityX) == '#')
            {
                entityX = rnd.Next(50, map.mapWidth - 2);
                entityY = rnd.Next(1, map.mapHeight - 2);
            }
        }

        public Monster(GameMap map, int x, int y)
        {
            entity = '&';
            entityX = x;
            entityY = y;
        }

        public new void UpdatePosition(Person player, GameMap map)
        {
            PersonLastPosition();
            base.UpdatePosition(player, map);
        }
    }
}
