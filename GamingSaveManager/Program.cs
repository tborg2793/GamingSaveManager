using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GamingSaveManager
{
    class Program
    {
        private static FileSystemWatcher watcher = new FileSystemWatcher();
        private static Logger logger = new Logger();
        private static bool gameProcess = false;

        static void Main(string[] args)
        {

            Game game1 = new Game(1, "Middle-Earth", "C:\\Users\\tb\\Desktop\\test.txt", "notepad.exe");

            StartProcess(game1);
            WatchSaveFile(game1);

            while (gameProcess) ;

            Console.ReadKey();
        }


        public static void StartProcess(Game game)
        {
            Process myProcess = Process.Start(game.exePath);
            logger.Debug("[" + game.gameName + "] " + game.exePath + " process has started.");
            myProcess.Exited += (sender, eventArgs) => OnProcessExited(game, eventArgs);


            myProcess.EnableRaisingEvents = true;
            gameProcess = true;
        }


        public static void OnProcessExited(Game game, EventArgs e)
        {
            logger.Debug("["+game.gameName+"] " + Path.GetFileName(game.exePath) + " process has exited");
            gameProcess = false;
            watcher.Dispose();
        }


        
        
        public static void WatchSaveFile(Game game)
        {
            watcher.Path = Path.GetDirectoryName(game.savePath);
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Filter = Path.GetFileName(game.savePath);
            watcher.Changed += (sender, eventArgs) => OnSaveUpdated(game, eventArgs);

            watcher.EnableRaisingEvents = true;
        }

        private static void OnSaveUpdated(Game game, FileSystemEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;
                logger.Debug("["+game.gameName+"] " + Path.GetFileName(game.savePath) + " was updated.");

            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }

        }


    }

}


