using System;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

namespace GamersGrotto.Multiplayer_Sample
{
    public class SessionUIManager : MonoBehaviour {
        [SerializeField] SessionElementUI sessionPrefab;
        [SerializeField] GameObject sessionListContent;
        [SerializeField] TMP_InputField sessionNameInput;
        [SerializeField] TMP_InputField playerNameInput;
        [SerializeField] Button refreshButton;
        [SerializeField] Button createSessionButton;
        
        void OnEnable() {
            refreshButton.onClick.AddListener(RefreshSessionList);
            createSessionButton.onClick.AddListener(CreateSession);
            playerNameInput.onSubmit.AddListener(SetPlayerName);
            playerNameInput.onDeselect.AddListener(SetPlayerName);
            playerNameInput.onValueChanged.AddListener(CheckRequirementsSet);
            sessionNameInput.onSubmit.AddListener(SetSessionName);
            sessionNameInput.onDeselect.AddListener(SetSessionName);
            sessionNameInput.onValueChanged.AddListener(CheckRequirementsSet);
        }

        void OnDisable() {
            refreshButton.onClick.RemoveListener(RefreshSessionList);
            createSessionButton.onClick.RemoveListener(CreateSession);
            playerNameInput.onSubmit.RemoveListener(SetPlayerName);
            playerNameInput.onDeselect.RemoveListener(SetPlayerName);
            playerNameInput.onValueChanged.RemoveListener(CheckRequirementsSet);
            sessionNameInput.onSubmit.RemoveListener(SetSessionName);
            sessionNameInput.onDeselect.RemoveListener(SetSessionName);
            sessionNameInput.onValueChanged.RemoveListener(CheckRequirementsSet);
        }

        void Start() {
            CheckRequirementsSet(string.Empty);
        }

        void CheckRequirementsSet(string _) {
            var playerNameRequirementsMet = SessionManager.PlayerNameRequirementsMet(playerNameInput.text);
            var sessionNameRequirementsMet = SessionManager.SessionsNameRequirementsMet(sessionNameInput.text);
            createSessionButton.interactable = playerNameRequirementsMet && sessionNameRequirementsMet;
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
                sessionObject.SetSession(session);
            }
        }

        async void CreateSession() {
            if(string.IsNullOrEmpty(SessionManager.Instance.sessionName)) {
                Debug.LogError("Session name cannot be empty");
                return;
            }
            
            if(string.IsNullOrEmpty(await AuthenticationService.Instance.GetPlayerNameAsync())) {
                Debug.LogError("Player name cannot be empty");
                return;
            }
            SessionManager.Instance.StartSessionAsHost();
        }
        
        public async void SetPlayerName(string playerName) {
            if(!string.IsNullOrEmpty(playerName))
                await SessionManager.Instance.SetPlayerName(playerName);
        }
        
        void SetSessionName(string text) {
            if(!string.IsNullOrEmpty(text))
                SessionManager.Instance.sessionName = text;
        }
    }
}
