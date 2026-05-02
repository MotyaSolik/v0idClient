using System;
using System.Collections.Generic;
using System.Linq;
using CaveGame.Entities;
using CaveGame.Menu;
using CaveGame.Edit;

namespace CaveGame.Core
{
    class GUI
    {
        private int frames;
        private int FPS;
        private DateTime lastTime = DateTime.Now;

        public void FPSCounter()
        {
            frames++;
        }

        public void ShowFPS()
        {

            if ((DateTime.Now - lastTime).TotalSeconds >= 1.0)
            {
                FPS = frames;
                frames = 0;
                lastTime = DateTime.Now;

                Console.SetCursorPosition(0, 29);
                Console.Write($"FPS: {FPS} ");
            }
        }
    }
}
