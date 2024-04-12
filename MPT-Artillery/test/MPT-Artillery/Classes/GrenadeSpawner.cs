using Comfort.Common;
using EFT.InventoryLogic;
using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT.Ballistics;

namespace SSH_Artillery
{
    internal class GrenadeSpawner : MonoBehaviour
    {
        private void Start()
        {
            GrenadeSpawner.Count = ArtilleryEntry.Instance.Count.Value;
            GrenadeSpawner.rate = ArtilleryEntry.Instance.Rate.Value;
            GrenadeSpawner.range = ArtilleryEntry.Instance.Range.Value;
            GrenadeSpawner.delay = ArtilleryEntry.Instance.Delay.Value;
            this.count = GrenadeSpawner.Count;
            base.InvokeRepeating("Tick", GrenadeSpawner.delay, GrenadeSpawner.rate);
        }

        private void Tick()
        {
            Vector3 position = base.transform.position;
            position.x += UnityEngine.Random.Range(-GrenadeSpawner.range, GrenadeSpawner.range);
            position.z += UnityEngine.Random.Range(-GrenadeSpawner.range, GrenadeSpawner.range);
            position.y += 300f;
            Shoot.MakeShot(Shoot.GetBullet("5d70e500a4b9364de70d38ce") as BulletClass, position, Vector3.down, 1f);
            int num = this.count - 1;
            this.count = num;
            bool flag = num <= 0;
            if (flag)
            {
                UnityEngine.Object.Destroy(this);
            }
        }

        public static int Count = 80;
        public static float rate = 0.5f;
        public static float range = 20f;
        public static float delay = 5f;
        private int count = 10;
        public string Owner { get; set; }
    }
}
