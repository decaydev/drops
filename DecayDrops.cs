using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oxide.Plugins
{
    [Info("DecayDrops", "decay.dev", "0.1.0")]
    [Description("managed supply drops, https://decay.dev/drops")]
    public class DecayDrops : RustPlugin
    {
        private ConfigData configData;
        private Queue<SupplySignal> signals;
        private bool init = false;

        private void OnServerInitialized()
        {
            signals = new Queue<SupplySignal>();
            init = true;
        }

        void OnExplosiveThrown(BasePlayer player, BaseEntity entity)
        {
            if (!init || entity == null || !(entity is SupplySignal)) return;
            signals.Enqueue(entity as SupplySignal);
        }

        void OnExplosiveDropped(BasePlayer player, BaseEntity entity)
        {
            if (!init || entity == null || !(entity is SupplySignal)) return;
            signals.Enqueue(entity as SupplySignal);
        }

        void OnEntitySpawned(BaseNetworkable entity)
        {
            if (entity == null) return;
            var b = entity.GetComponent<BaseEntity>();
            if (b == null) return;
            if (b is CargoPlane)
            {
                var plane = b as CargoPlane;
                plane.secondsToTake = configData.flightDuration;
            }
            else if (b is SupplyDrop)
            {
                var drop = b as SupplyDrop;
                var v = drop.GetDropVelocity();
                drop.RemoveParachute();
                drop.MakeLootable();
                var signal = signals.Dequeue();
                var sp = signal.transform.position;
                drop.transform.position = new UnityEngine.Vector3(sp.x, sp.y + 0.015f, sp.z);
                signal.Kill();
            }
        }


        protected override void LoadConfig()
        {
            base.LoadConfig();
            configData = Config.ReadObject<ConfigData>();
            Config.WriteObject(configData, true);
        }

        protected override void LoadDefaultConfig()
        {
            LoadConfig();
        }

        private class ConfigData
        {
            [JsonProperty(PropertyName = "flight-duration")]
            public float flightDuration { get; set; }
            [JsonProperty(PropertyName = "drop-duration")]
            public float dropDuration { get; set; }
        }
    }
}

