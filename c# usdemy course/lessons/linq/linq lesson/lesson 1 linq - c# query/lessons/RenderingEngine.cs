using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace lessons
{
    internal class RenderingEngine
    {
        public RenderingEngine()
        {
            GameEventManager.OnGameStart += StartGame;
            GameEventManager.OnGameOver += GameOver;
        }
        private void StartGame()
        {
            Console.WriteLine("Rendering Engine Started!");
            Console.WriteLine("Drawing visuals...");
        }
        private void GameOver()
        {
            Console.WriteLine("Rendering Engine Stopped");
        }
    }
}
