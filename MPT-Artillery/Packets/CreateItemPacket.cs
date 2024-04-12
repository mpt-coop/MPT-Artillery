using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSH_Artillery.Packets
{
    public struct CreateItemPacket : INetSerializable
    {
        public string ID { get; set; }
        public string TemplateID { get; set; }

        // How the packet gets unpacked so the data can be read from it.
        public void Deserialize(NetDataReader reader)
        {
            this.ID = reader.GetString();
            this.TemplateID = reader.GetString();
        }

        // How the packet gets packed so the data can be sent to other users.
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(ID);
            writer.Put(TemplateID);
        }
    }
}
