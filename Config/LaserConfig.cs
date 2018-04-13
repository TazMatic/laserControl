using JsonConfigSaver;
using LaserControl.ThreadWatcher;
using LaserControlLight.Config;
using LaserControlLight.Thread;
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



        //prerequis

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
        public int currentDestruction = 0;


        public LaserConfig(string plugin, string name) : base(plugin, name)
        {
            Console.WriteLine(LaserControlMod.prefix + "Full config controller registred !");
            LaserLightConfig.commonGetter = new CommonConfigGetterFull();
        }



        public void updateCurrentDestruction()
        {
            this.currentDestruction++;
            this.save();
            WorldObjAnalyser.reloadAllObject();
        }

    }
}
