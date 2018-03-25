using JsonConfigSaver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int laserNeeded = 10;
        [JsonProperty]
        public int onlinePlayersNeededForLaser = 1;

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


        public LaserConfig(string plugin, string name) : base(plugin, name)
        {
          
        }


        //Save
        [JsonProperty]
        private int currentDestruction = 0;


        public int getEnergyNeededForLaser()
        {
            double multiplier = Math.Pow(energyFactorMultiplier, currentDestruction);
            int needed = energyBaseNeededForLaser * (int) multiplier;

            return needed;
        }

        public void updateCurrentDestruction()
        {
            this.currentDestruction++;
        }

    }
}
