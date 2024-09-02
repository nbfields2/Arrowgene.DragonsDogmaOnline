using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class WarpGetReturnLocationHandler : StructurePacketHandler<GameClient, C2SWarpGetReturnLocationReq>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(WarpGetReturnLocationHandler));

        public WarpGetReturnLocationHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SWarpGetReturnLocationReq> packet)
        {
            S2CWarpGetReturnLocationRes response = new S2CWarpGetReturnLocationRes();
            // WDT or BBM Cove
            response.JumpLocation.stageId = (uint)((client.GameMode == GameMode.Normal) ? 3 : 602);
            response.JumpLocation.startPos = 0;

            client.Send(response);
        }
    }
}
