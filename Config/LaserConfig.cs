using JsonConfigSaver;
using LaserControl.ThreadWatcher;
using Newtonsoft.Json;
using System;

namespace LaserControl.Config
{
    public sealed class LaserConfig : JsonEcoConfig
    {
        //afterDestroy
        [JsonProperty]
        public bool destroyLasersAfterMeteorDestroy = true;
        [JsonProperty]
        public bool destroyEnergyAfterMeteorDestroy = true;
        [JsonProperty]
        public bool multiplyEnergyNeededAfterMeteorDestroy = true;
        [JsonProperty]
        public float energyFactorMultiplier = 1.75f;


        //destruction
        [JsonProperty]
        public int secondsToDestroyMeteor = 20;

        //prerequis
        [JsonProperty]
        private int energyBaseNeededForLaser = 50000;
        [JsonProperty]
        private int laserBaseNeeded = 4;
        [JsonProperty]
        public int onlinePlayersNeededForLaser = 1;
        [JsonProperty]
        public float laserNeededMultiplier = 1.3f;

        //reward
        [JsonProperty]
        public bool activateSkillPointReward = true;
        [JsonProperty]
        public int skillPointRewardPerPlayer = 20;
        [JsonProperty]
        public int skillPointRewardToShare = 200;

        [JsonProperty]
        public bool activateEconomyReward = false;
        [JsonProperty]
        public int moneyToGivePerPlayer = 10;
        [JsonProperty]
        public int moneyToGiveToShare = 1000;
        [JsonProperty]
        public String moneyName = "GiveYouMoneyNameHere";



        //Save
        [JsonProperty]
        private int currentDestruction = 0;


        public LaserConfig(string plugin, string name) : base(plugin, name)
        {
          
        }





        public int getEnergyNeededForLaser()
        {
            double multiplier = Math.Pow(energyFactorMultiplier, currentDestruction);
            double needed = energyBaseNeededForLaser * multiplier;

            return (int)needed;
        }

        public int getLaserNeeded()
        {
            double multiplier = Math.Pow(laserNeededMultiplier, currentDestruction);
            double needed = laserBaseNeeded * multiplier;

            return (int)needed;
        }


        public void updateCurrentDestruction()
        {
            this.currentDestruction++;
            this.save();
            LaserWatcher.reloadAllObject();
        }

    }
}
