using System;
using System.Collections.Generic;
using System.Text;

namespace GamingSaveManager
{
    public class Game
    {
        public int gameId { get; set;}
        public string gameName { get; set; }
        public string savePath { get; set; }
        public string exePath { get; set; }

        public Game (int gameId, string gameName, string savePath, string exePath)
        {
            this.gameId = gameId;
            this.gameName = gameName;
            this.savePath = savePath;
            this.exePath = exePath;
        }
    }
}
