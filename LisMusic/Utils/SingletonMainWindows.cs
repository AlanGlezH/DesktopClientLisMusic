using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.Utils
{
    class SingletonMainWindows
    {
        private static MainWindow mainWindow = null;

        private SingletonMainWindows() { }

        public static void SetSingletonWindow(MainWindow main)
        {
            if(mainWindow == null)
            {
                mainWindow = main;
            }
        }

        public static MainWindow GetSingletonWindow()
        {
            return mainWindow;
        }

        public static void CleanSingleton()
        {
            mainWindow = null;
        }
    }
}
