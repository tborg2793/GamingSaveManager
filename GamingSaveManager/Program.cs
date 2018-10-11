using System;
using System.Diagnostics;
using System.IO;

namespace GamingSaveManager
{
    class Program
    {
        private static FileSystemWatcher watcher = new FileSystemWatcher();


        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.Debug("Hello");
            Game game1 = new Game(1, "Middle-Earth", "C:\\Users\\tb\\Desktop\\test.txt", "G:\\MiddleEarth\\ME.exe");
            //Console.Write(game1.gameName);
            StartProcess();
            Watch(game1);
            //Console.ReadLine();
        }

        public static void OnProcessExited(object sender, EventArgs e)
        {
            Console.WriteLine("Process has exited");

            watcher.Dispose();
        }

        public static void StartProcess()
        {
            Process myProcess = Process.Start("notepad.exe");
            myProcess.EnableRaisingEvents = true;
            myProcess.Exited += OnProcessExited;
        }
        
        public static void Watch(Game game)
        {
            watcher.Path = Path.GetDirectoryName(game.savePath);
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Filter = Path.GetFileName(game.savePath);
            watcher.Changed += new FileSystemEventHandler(OnSaveUpdated);
            watcher.EnableRaisingEvents = true;
        }

        private static void OnSaveUpdated(object source, FileSystemEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;
                Console.WriteLine(e.Name);

            }
            finally
            {
                watcher.EnableRaisingEvents = true;

            }

        }


    }
}


