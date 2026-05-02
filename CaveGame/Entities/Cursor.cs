using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Core;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Entities
{
    class Cursor : Entity
    {
        public Cursor(int x, int y)
        {
            entity = '#';
            entityX = x;
            entityY = y;
        }

        public void SwapChar(char chr)
        {
            entity = chr;
        }

        public void TryMoveCursor(int newY, int newX, Editor edit)
        {
            if (newX >= 0 && newX <= edit.mapWidth - 1 && newY >= 0 && newY <= edit.mapHeight - 1)
            {
                entityX = newX;
                entityY = newY;
            }
        }
    }
}
