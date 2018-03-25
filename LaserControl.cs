using Eco.Core.Plugins.Interfaces;
using EcoColorLib;
using LaserControl.Config;
using LaserControl.ThreadWatcher;
using System;
using System.Threading;

namespace LaserControl
{
    public class LaserControl : IModKitPlugin, IServerPlugin
    {
        public static String prefix = "LaserControl: ";
        public static String coloredPrefix = ChatFormat.Green.Value + ChatFormat.Bold.Value + prefix + ChatFormat.Clear.Value;
        public static LaserConfig config;

        private Boolean started = false;

        public string GetStatus()
        {
            if(!started)
            {
                this.start();
                started = true;
            }
            return "";
        }

        public void start()
        {


            config = new LaserConfig("LaserConfig", "config");
            if (config.exist())
            {
                config = config.reload<LaserConfig>();
            }
            else
            {
                config.save();
            }


            Thread objw = new Thread(() => LaserWatcher.OjbectModifier());
            objw.Start();

            MeteorWatcher.Initialize();
            Thread meteorw = new Thread(() => MeteorWatcher.watchMeteor());
            meteorw.Start();

        }
    }
}
