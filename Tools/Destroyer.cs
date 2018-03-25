using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Mods.TechTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserControl.Tools
{
    public class Destroyer
    {
        public static void destroyEquipement(PowerGridComponent grid)
        {
            ArrayList toDest = new ArrayList();

            foreach (PowerGridComponent pgc in grid.PowerGrid.Components)
            {
                WorldObject o = pgc.Parent;
                
                if (o is CombustionGeneratorObject || o is SolarGeneratorObject||o is WindTurbineObject)
                {
                    if(LaserControl.config.destroyEnergyAfterMeteorDestroy)
                        toDest.Add(o);
                }
                else if(o is LaserObject && LaserControl.config.destroyLasersAfterMeteorDestroy  || o is ComputerLabObject && LaserControl.config.destroyLasersAfterMeteorDestroy)
                {
                    toDest.Add(o);
                }

            }

            foreach (WorldObject o in toDest)
            {
                o.Destroy();
            }
        }
    }
}
