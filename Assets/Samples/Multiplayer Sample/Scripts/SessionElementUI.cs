using System;
using TMPro;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.UI;

namespace GamersGrotto.Multiplayer_Sample {
    public class SessionElementUI : MonoBehaviour {
        [SerializeField] TextMeshProUGUI sessionName;
        [SerializeField] TextMeshProUGUI sessionHostId;
        [SerializeField] TextMeshProUGUI sessionPlayerCount;
        [SerializeField] Button joinButton;
        
        public ISessionInfo Session { get; private set; }

        private void Update()
        {
            joinButton.interactable = Session != null && PlayersManager.PlayerNameRequirementsMet(PlayersManager.Instance.GetLocalPlayerName());
        }

        public void SetSession(ISessionInfo session) {
            Session = session;
            sessionName.text = session.Name;
            sessionHostId.text = session.HostId;
            sessionPlayerCount.text = $"{session.MaxPlayers - session.AvailableSlots}/{session.MaxPlayers}";
        }

        public async void JoinSession() {
            if(Session == null) {
                Debug.LogError("Session is null");
                return;
            }
            
            await SessionManager.Instance.JoinSessionById(Session.Id);
        }
    }
}