using BepInEx;
using BepInEx.Configuration;
using Comfort.Common;
using EFT;
using EFT.Ballistics;
using EFT.InventoryLogic;
using EFT.UI;
using FlyingWormConsole3.LiteNetLib;
using MPT.Core.Coop.Matchmaker;
using MPT.Core.Modding;
using MPT.Core.Modding.Events;
using SSH_Artillery;
using SSH_Artillery.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SSH_Artillery
{
    [BepInPlugin("com.servph.ArtillerySupport", "Artillery Support", "1.0")]
    public class ArtilleryEntry : BaseUnityPlugin
    {
        public Player LocalPlayer { get; private set; }
        private const string MainSection = "Main Settings";

        public ConfigEntry<int> Count;

        public ConfigEntry<float> Rate;

        public ConfigEntry<float> Range;

        public ConfigEntry<float> Delay;
        public static ArtilleryEntry Instance { get; private set; }

        public Dictionary<string, Item> networkedItems = new Dictionary<string, Item> ();
        public void Awake()
        {
            Instance = this;
            this.Count = base.Config.Bind<int>("Main Settings", "Artillery Round Count", 50, new ConfigDescription("Count", new AcceptableValueRange<int>(0, 200), Array.Empty<object>()));
            this.Rate = base.Config.Bind<float>("Main Settings", "Artillery Fire Rate", 0.5f, new ConfigDescription("Rate", new AcceptableValueRange<float>(0f, 5f), Array.Empty<object>()));
            this.Range = base.Config.Bind<float>("Main Settings", "Artillery Sight In Range", 20f, new ConfigDescription("Range", new AcceptableValueRange<float>(0f, 100f), Array.Empty<object>()));
            this.Delay = base.Config.Bind<float>("Main Settings", "Artillery Firing Delay", 5f, new ConfigDescription("Delay", new AcceptableValueRange<float>(0f, 20f), Array.Empty<object>()));
            new HookThrowablePatch().Enable();

            // Subscribes to the event that triggers when a client joins a raid.
            MPTEventDispatcher.SubscribeEvent<MPTClientCreatedEvent>(OnMPTClientCreatedEvent);

            // Subscribes to the event that triggers when the GameWorld is Created.
            MPTEventDispatcher.SubscribeEvent<GameWorldStartedEvent>(onGameWorldStartedEvent);
        }

        private void onGameWorldStartedEvent(GameWorldStartedEvent @event)
        {
            Shoot.Init(); // Initialize the Shoot Class so bullets can spawn. 
        }

        private void OnMPTClientCreatedEvent(MPTClientCreatedEvent @event)
        {
            if (MatchmakerAcceptPatches.IsClient)
            {
                // Subscribe to both packets for creating an item & having the artillery fire
                @event.Client._packetProcessor.SubscribeNetSerializable<ArtilleryShotPacket>(OnArtilleryShotPacket); 
                @event.Client._packetProcessor.SubscribeNetSerializable<CreateItemPacket>(OnCreateItemPacket);
            }
        }

        // The packet for Creating an Item. 
        private void OnCreateItemPacket(CreateItemPacket packet)
        {
            if (networkedItems.ContainsKey(packet.ID))
            {
                return;
            }
            var createdItem = Singleton<ItemFactory>.Instance.CreateItem(packet.ID, packet.TemplateID, null); // Create an item from the ItemFactory using the packets data
            networkedItems.Add(packet.ID, createdItem);
        }

        private void OnArtilleryShotPacket(ArtilleryShotPacket packet)
        {
            if (networkedItems.TryGetValue(packet.BulletID, out var bullet))
            {
                Shoot.MakeShot(bullet as BulletClass, packet.Position, packet.Direction, packet.SpeedFactor); // Shoot the artillery shell using the packets data so everything will be lined up on all clients. 
            }
        }
    }
}