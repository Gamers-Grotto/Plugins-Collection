using GamersGrotto.GG_Broker;

namespace GamersGrotto.Multiplayer_Sample.Messages {
    public class PlayerDeathMessage: IMessage {
        public ulong PlayerId { get; set; }
        public ulong KillerId { get; set; }
        
        public PlayerDeathMessage(ulong playerId, ulong killerId) {
            PlayerId = playerId;
            KillerId = killerId;
        }
    }
}