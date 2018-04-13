using LaserControlLight.Config;
using System;

namespace LaserControl.Config
{
    public class CommonConfigGetterFull : CommonConfigGetter
    {
        public override int getEnergyNeededForLaser()
        {
            double multiplier = Math.Pow(LaserControlMod.config.energyFactorMultiplier, LaserControlMod.config.currentDestruction);
            double needed = LaserControlLight.LaserControlLight.config.energyBaseNeededForLaser * multiplier;

            return (int)needed;
        }

        public override int getLaserNeeded()
        {
            double multiplier = Math.Pow(LaserControlMod.config.laserNeededMultiplier, LaserControlMod.config.currentDestruction);
            double needed = LaserControlLight.LaserControlLight.config.laserBaseNeeded * multiplier;

            return (int)needed;
        }
    }
}
