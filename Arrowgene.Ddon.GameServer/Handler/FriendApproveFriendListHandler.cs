﻿using Arrowgene.Ddon.GameServer.Characters;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class FriendApproveFriendListHandler : GameStructurePacketHandler<C2SFriendApproveFriendReq>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(FriendApproveFriendListHandler));


        public FriendApproveFriendListHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SFriendApproveFriendReq> packet)
        {
            ContactListEntity relationship = Database.SelectContactsByCharacterId(packet.Structure.CharacterId, client.Character.CharacterId);
            if (relationship is not { Status: ContactListStatus.PendingApproval })
            {
                Logger.Error(client, $"ContactListEntity not found");
                client.Send(
                    new S2CFriendApproveFriendRes()
                    {
                        FriendInfo = new CDataFriendInfo(),
                        Error = (uint)ErrorCode.ERROR_CODE_FRIEND_NOT_IN_APPROVING_LIST
                    }
                );
                return;
            }
            
            
            if (packet.Structure.IsApproved)
            {
                Database.UpdateContact(packet.Structure.CharacterId, client.Character.CharacterId,
                    ContactListStatus.Accepted, ContactListType.FriendList, false, false);
            }
            else
            {
                Database.DeleteContact(packet.Structure.CharacterId, client.Character.CharacterId);
            }
            
            Character requestingChar = ContactListManager.getCharWithOnlineStatus(Server, Database, packet.Structure.CharacterId);
            var result = new S2CFriendApproveFriendRes()
            {
                FriendInfo = ContactListManager.CharacterToFriend(requestingChar, relationship.Id, relationship.IsFavoriteForCharacter(client.Character.CharacterId)),
                Result = 0,
                Error = 0,
                    
            };
            client.Send(result);
            
            GameClient requestingClient = Server.ClientLookup.GetClientByCharacterId(packet.Structure.CharacterId);
            if (requestingClient != null)
            {
                var ntc = new S2CFriendApproveFriendNtc()
                {
                    FriendInfo = ContactListManager.CharacterToFriend(client.Character, relationship.Id, relationship.IsFavoriteForCharacter(packet.Structure.CharacterId)),
                    IsApproved = packet.Structure.IsApproved
                };
                requestingClient.Send(ntc);
            }
        }
    }
}
