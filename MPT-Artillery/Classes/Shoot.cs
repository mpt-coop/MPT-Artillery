using Comfort.Common;
using EFT;
using EFT.Ballistics;
using EFT.HealthSystem;
using EFT.InventoryLogic;
using LiteNetLib;
using LiteNetLib.Utils;
using MPT.Core.Coop.Matchmaker;
using MPT.Core.Networking;
using SSH_Artillery.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SSH_Artillery
{
    public static class Shoot
    {
        public static object MakeShot(BulletClass ammo, Vector3 shotPosition, Vector3 shotDirection, float speedFactor)
        {
            EftBulletClass obj = Shoot.ballisticsCalculator.CreateShot(ammo, shotPosition, shotDirection, 0, Singleton<GameWorld>.Instance.MainPlayer.ProfileId, Shoot.weapon, speedFactor, 0);
            Shoot.ballisticsCalculator.Shoot(obj);
            if (MatchmakerAcceptPatches.IsServer)
            {
                NetDataWriter netDataWriter = new NetDataWriter();
                ArtilleryShotPacket artilleryShotPacket = new ArtilleryShotPacket { BulletID = ammo.Id, Position = shotPosition, Direction = shotDirection, SpeedFactor = speedFactor };

                //sends the packet to other clients if host
                Singleton<MPTServer>.Instance.SendDataToAll(netDataWriter, ref artilleryShotPacket, DeliveryMethod.ReliableUnordered);
            }
            return obj;
        }

        public static object GetBullet(string tid)
        {
            var itemFactory = Singleton<ItemFactory>.Instance;
            var createdItem = itemFactory.CreateItem(MongoID.Generate(), tid, default);
            if (MatchmakerAcceptPatches.IsServer)
            {
                NetDataWriter netDataWriter = new NetDataWriter();
                CreateItemPacket createItemPacket = new CreateItemPacket { ID = createdItem.Id, TemplateID = tid };

                //sends the packet to other clients if host
                Singleton<MPTServer>.Instance.SendDataToAll(netDataWriter, ref createItemPacket, DeliveryMethod.ReliableUnordered);
            }
            return createdItem;

        }

        public static void Init()
        {
            bool flag = null == Shoot.ballisticsCalculator;
            if (flag)
            {
                Shoot.ballisticsCalculator = Singleton<GameWorld>.Instance._sharedBallisticsCalculator;
            }
            bool flag2 = null == Shoot.methodShoot;
            if (flag2)
            {
                Shoot.methodShoot = Shoot.ballisticsCalculator.GetType().GetMethod("Shoot");
            }
            bool flag3 = null == Shoot.methodCreateShot;
            if (flag3)
            {
                Shoot.methodCreateShot = Shoot.ballisticsCalculator.GetType().GetMethod("CreateShot");
            }
            Shoot.player = Singleton<GameWorld>.Instance.MainPlayer;
            bool flag4 = Shoot.weapon == null;
            if (flag4)
            {
                var itemFactory = Singleton<ItemFactory>.Instance;
                Shoot.weapon = (Weapon)itemFactory.CreateItem(Guid.NewGuid().ToString("N").Substring(0, 24), "5d52cc5ba4b9367408500062", default);
            }
        }

        private static BallisticsCalculator ballisticsCalculator;
        private static MethodInfo methodShoot;
        private static Player player;
        private static Weapon weapon;
        private static MethodBase methodCreateShot;
    }
}
