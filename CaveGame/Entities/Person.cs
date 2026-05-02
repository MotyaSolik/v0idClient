using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Core;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Entities
{
    class Person : Entity
    {
        public Person(char entity, int x, int y)
        {
            this.entity = entity;
            entityX = x;
            entityY = y;
        }

        public void TryMovePerson(int newY, int newX, GameMap map)
        {
            if (CheatState.Noclip)
            {
                if (newX >= 0 && newX < map.mapWidth && newY >= 0 && newY < map.mapHeight)
                {
                    entityX = newX;
                    entityY = newY;
                }
                return;
            }
            if (newX >= 0 && newX <= map.mapWidth - 1 && newY >= 0 && newY <= map.mapHeight - 1)
            {
                if (map.GetCharOfMap(newY, newX) != '#')
                {
                    entityX = newX;
                    entityY = newY;
                }
            }
        }
    }
}
