using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerDataController : NetworkBehaviour
    {
        [SerializeField] private PlayerWorldSpaceUI playerWorldSpaceUI;
        
        private void Start()
        {
           SessionManager.Instance.OnPlayerDataChangedEvent.AddListener(OnClientConnected);
           if(IsHost)
               OnClientConnected();
        }

        void OnDisable() {
            SessionManager.Instance.OnPlayerDataChangedEvent.RemoveListener(OnClientConnected);
        }

        void OnClientConnected(List<PlayerSessionData> _ = default) {
            var ownerID = GetComponent<NetworkObject>().OwnerClientId;
            playerWorldSpaceUI.SetPlayerName(SessionManager.Instance.GetPlayerName(ownerID));
        }
    }
}
