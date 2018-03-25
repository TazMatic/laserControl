using Eco.Core.Controller;
using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Disasters;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Mods.TechTree;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using LaserControl.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserControl.CustomComponent
{
    [Serialized]
    [Priority(-2)]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(StatusComponent))]
    class OnlinePlayerComponent : WorldObjectComponent
    {

        private StatusElement status;

        private bool enabled;
        public override bool Enabled => this.enabled;


        public override void Initialize()
        {
            this.enabled = false;
            this.status = this.Parent.GetComponent<StatusComponent>().CreateStatusElement();
        }

        public override void Tick()
        {
            base.Tick();
            this.UpdateNetwork();
        }

        private void UpdateNetwork()
        {
            int minPlayer = LaserControl.config.onlinePlayersNeededForLaser;
            int count = UserManager.OnlineUsers.Count();

            if(count>= minPlayer)
            {
                this.enabled = true;
            }
            else
            {
                this.enabled = false;
            }

            this.status.SetStatusMessage(this.Enabled, "Minimum online player ok ("+ count + "/"+ minPlayer + ")", "Insufficient online player ("+ count + "/"+ minPlayer + ")");




          
        }

    }
}
