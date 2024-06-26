﻿using JetBrains.Annotations;
using LiteNetLib;
using LiteNetLib.Utils;
using MPT.Core.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SSH_Artillery.Packets
{
    public struct ArtilleryShotPacket : INetSerializable
    {
        public string BulletID { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public float SpeedFactor { get; set; }

        // How the packet gets unpacked so the data can be read from it.
        public void Deserialize(NetDataReader reader)
        {
            this.BulletID = reader.GetString();
            this.Position = reader.GetVector3();
            this.Direction = reader.GetVector3();
            this.SpeedFactor = reader.GetFloat();
        }

        // How the packet gets packed so the data can be sent to other users.
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(BulletID);
            writer.Put(Position);
            writer.Put(Direction);
            writer.Put(SpeedFactor);
        }
    }
}
