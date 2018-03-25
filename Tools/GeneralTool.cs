using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserControl.Tools
{
    class GeneralTool
    {
        public static ArrayList getUserNear(WorldObject o,float maxDistance)
        {
            ArrayList liste = new ArrayList();
            foreach (User u in UserManager.OnlineUsers)
            {
                float distance = WorldPosition3i.Distance(u.Player.Position.WorldPosition3i, o.Position3i);
                if(distance<=maxDistance)
                {
                    liste.Add(u);
                }
            }
            return liste;
        }
    }
}
