using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lessons
{
    internal class Player
    { 
    
       
        public string PlayerName { get; set; }
        public Player(string name)
        {
            this.PlayerName = name;
            GameEventManager.OnGameStart += StartGame;
            GameEventManager.OnGameOver += GameOver;
        }
        private void StartGame()
        {
            Console.WriteLine("spawning Player with ID: {0}",PlayerName);
        }
        private void GameOver()
        {
            Console.WriteLine($"removing game with ID: {PlayerName}");
        }
    }
}
