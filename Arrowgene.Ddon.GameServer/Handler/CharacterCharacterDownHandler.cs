using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterCharacterDownHandler : GameStructurePacketHandler<C2SCharacterCharacterDownNtc>
    {
        public CharacterCharacterDownHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterCharacterDownNtc> packet)
        {
            //Unsure what CAPCOM wanted with this packet.
        }
    }
}
