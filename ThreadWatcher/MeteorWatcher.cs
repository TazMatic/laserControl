using Eco.Gameplay.Disasters;
using Eco.Shared.Networking;
using LaserControl.Tools;
using System.Linq;
using System.Threading;

namespace LaserControl.ThreadWatcher
{
    class MeteorWatcher
    {
        public static bool wasMeteor = true;


        public static void Initialize()
        {
            if (NetObjectManager.GetObjectsOfType<MeteorObject>().FirstOrDefault() == null)
            {
                wasMeteor = false;
            }
        }

        public static void WatchMeteor()
        {
            while(true)
            {
                Thread.Sleep(1);


                if (DisasterPlugin.Meteor == null && wasMeteor)
                {
                    wasMeteor = false;
                    CustomEvent.onDestroyedMeteor();
                }
                else if (DisasterPlugin.Meteor != null)
                {
                    wasMeteor = true;
                }
            }
        }
    }
}
