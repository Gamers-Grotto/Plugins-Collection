using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerDataController : MonoBehaviour
    {
        [SerializeField] private PlayerWorldSpaceUI playerWorldSpaceUI;
        
        private void Start()
        {
           SessionManager.Instance.OnPlayerDataChangedEvent.AddListener(OnClientConnected);
        }

        void OnDisable() {
            SessionManager.Instance.OnPlayerDataChangedEvent.RemoveListener(OnClientConnected);
        }

        void OnClientConnected(List<PlayerSessionData> _) {
            var ownerID = GetComponent<NetworkObject>().OwnerClientId;
            playerWorldSpaceUI.SetPlayerName(SessionManager.Instance.GetPlayerName(ownerID));
        }
    }
}
