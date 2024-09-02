using Arrowgene.Ddon.GameServer.Dump;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class AreaGetLeaderAreaReleaseListHandler : PacketHandler<GameClient>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(AreaGetLeaderAreaReleaseListHandler));


        public AreaGetLeaderAreaReleaseListHandler(DdonGameServer server) : base(server)
        {
        }

        public override PacketId Id => PacketId.C2S_AREA_GET_LEADER_AREA_RELEASE_LIST_REQ;

        public override void Handle(GameClient client, IPacket packet)
        {
            client.Send(GameFull.Dump_117);

            // S2CAreaGetLeaderAreaReleaseListRes res = new S2CAreaGetLeaderAreaReleaseListRes();
            // client.Send(res);
        }
    }
}
