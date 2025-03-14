using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

namespace GamersGrotto.Multiplayer_Sample
{
    public class SessionUIManager : MonoBehaviour {
        [SerializeField] GameObject sessionPrefab;
        [SerializeField] GameObject sessionListContent;
        [SerializeField] TMP_InputField sessionNameInput;
        [SerializeField] TMP_InputField playerNameInput;
        [SerializeField] Button refreshButton;
        [SerializeField] Button createSessionButton;
        
        void OnEnable() {
            refreshButton.onClick.AddListener(RefreshSessionList);
            createSessionButton.onClick.AddListener(CreateSession);
            playerNameInput.onSubmit.AddListener((value) => SetPlayerName(playerNameInput.text));
            sessionNameInput.onSubmit.AddListener((value) => SetSessionName(sessionNameInput.text));
        }

        void OnDisable() {
            refreshButton.onClick.RemoveListener(RefreshSessionList);
            createSessionButton.onClick.RemoveListener(CreateSession);
            playerNameInput.onSubmit.RemoveListener((value) => SetPlayerName(playerNameInput.text));
            sessionNameInput.onSubmit.RemoveListener((value) => SetSessionName(sessionNameInput.text));
        }

       

        async void RefreshSessionList() {
            //clear the list
            foreach (Transform child in sessionListContent.transform) {
                Destroy(child.gameObject);
            }
            
            //get the list of sessions
            var sessions = await SessionManager.Instance.QuerySessions();
            foreach (var session in sessions) {
                var sessionObject = Instantiate(sessionPrefab, sessionListContent.transform);
               sessionObject.GetComponent<SessionElementUI>().SetSession(session);
            }
        }

        async void CreateSession() {
            if(SessionManager.Instance.sessionName == "") {
                Debug.LogError("Session name cannot be empty");
                return;
            }
            if(await AuthenticationService.Instance.GetPlayerNameAsync() == "") {
                Debug.LogError("Player name cannot be empty");
                return;
            }
            SessionManager.Instance.StartSessionAsHost();
        }
        
        public async void SetPlayerName(string playerName) {
            await SessionManager.Instance.SetPlayerName(playerName);
        }
        void SetSessionName(string text) {
            SessionManager.Instance.sessionName = text;
        }
    }
}
