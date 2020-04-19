using System;

namespace LaserControl.Config
{
    public class ConfigGetter
    {
//        public int getEnergyNeededForLaser()
//        {
//            double multiplier = Math.Pow(LaserControlMod.config.energyFactorMultiplier, LaserControlMod.config.currentDestruction);
//            double needed = LaserControlMod.config.energyBaseNeededForLaser * multiplier;
//
//            return (int)needed;
//        }

        public int getLaserNeeded()
        {
            double multiplier = Math.Pow(LaserControlMod.config.laserNeededMultiplier, LaserControlMod.config.currentDestruction);
            double needed = LaserControlMod.config.laserBaseNeeded * multiplier;

            return (int)needed;
        }
    }
}
