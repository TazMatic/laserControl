using Eco.Core.Plugins.Interfaces;
using EcoColorLib;
using LaserControl.ThreadWork;
using System;
using System.Threading;

namespace LaserControl
{
    public class LaserControl : IModKitPlugin, IServerPlugin
    {
        public static String prefix = "LaserControl: ";
        public static String coloredPrefix = ChatFormat.Green.Value + ChatFormat.Bold.Value + prefix + ChatFormat.Clear.Value;

        private Boolean started = false;

        public string GetStatus()
        {
            if(!started)
            {
                this.start();
            }
            return "";
        }

        public void start()
        {
            Thread hotf = new Thread(() => LaserWatcher.hotfix());
            hotf.Start();
        }
    }
}
