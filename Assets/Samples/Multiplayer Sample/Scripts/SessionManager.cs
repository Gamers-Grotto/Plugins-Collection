using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersGrotto.Core.Extended_Attributes;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GamersGrotto.Multiplayer_Sample {
    public class SessionManager : NetworkBehaviour {
        public short maxPlayers = 8;
        public string sessionName = "Default Session";
        public string sessionPassword = null;

        public static SessionManager Instance { get; private set; }
        NetworkManager networkManager;

        ISession activeSession;

        public ISession ActiveSession {
            get => activeSession;
            set {
                activeSession = value;
                Debug.Log($"Active session Id is now {activeSession?.Id}");
            }
        }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        async void Start() {
            try {
                await UnityServices.InitializeAsync(); // Initialize Unity Gaming Services SDKs
                await AuthenticationService.Instance
                    .SignInAnonymouslyAsync(); // Can be replaced with any other sign-in method
                Debug.Log("Signed in anonymously. PlayerID: " + AuthenticationService.Instance.PlayerId);
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
        }

        void OnDisable() {
            UnregisterSessionEvents();
        }


        #region Event Registration

        void RegisterSessionEvents() {
            ActiveSession.Changed += OnSessionChanged;
            ActiveSession.PlayerJoined += OnPlayerJoined;
            ActiveSession.PlayerLeaving += OnPlayerLeaving;
        }

        void UnregisterSessionEvents() {
            if (ActiveSession == null)
                return;

            ActiveSession.Changed -= OnSessionChanged;
            ActiveSession.PlayerJoined -= OnPlayerJoined;
            ActiveSession.PlayerLeaving -= OnPlayerLeaving;
        }

        void OnSessionChanged() {
            Debug.Log("Session changed. New session Id: " + ActiveSession.Id);
        }

        void OnPlayerJoined(string obj) {
            Debug.Log("Player joined: " + obj);
        }

        void OnPlayerLeaving(string obj) {
            Debug.Log("Player left: " + obj);
        }

        #endregion

        [Button]
        public async void StartSessionAsHost() {
            var playerProperties = await PlayersManager.GetPlayerProperties();
            var options = new SessionOptions {
                Name = sessionName,
                MaxPlayers = maxPlayers,
                IsPrivate = false,
                IsLocked = false,
                PlayerProperties = playerProperties
            };
            if (sessionPassword is { Length: >= 8 }) {
                //Idk, arbitrary password requirement when set in body.
                options.Password = sessionPassword;
            }

            options.WithRelayNetwork();


            ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
            Debug.Log(
                $"Session {ActiveSession.Name}({ActiveSession.Id}) created successfully! Join code: {ActiveSession.Code}");
            RegisterSessionEvents();

            await WorldSaveGameManager.Instance.LoadNewGame();
        }

        public async Task JoinSessionById(string sessionId) {
            var options = new JoinSessionOptions() {
                PlayerProperties = await PlayersManager.GetPlayerProperties(),
            };

            ActiveSession = await MultiplayerService.Instance.JoinSessionByIdAsync(sessionId, options);
            Debug.Log($"Session {ActiveSession.Id} joined successfully!");

            await WorldSaveGameManager.Instance.LoadNewGame();
        }

        public async Task JoinSessionByCode(string sessionCode) {
            ActiveSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(sessionCode);
            Debug.Log($"Session {ActiveSession.Id} joined successfully!");

            await WorldSaveGameManager.Instance.LoadNewGame();
        }

        public async Task KickPlayer(string playerId) {
            if (!ActiveSession.IsHost) return;

            await ActiveSession.AsHost().RemovePlayerAsync(playerId);
            Debug.Log($"Player {playerId} kicked successfully!");
        }

        public async Task LeaveSession() {
            if (ActiveSession == null) return;
            UnregisterSessionEvents();

            try {
                await ActiveSession.LeaveAsync();
                Debug.Log($"Session {ActiveSession.Id} left successfully!");
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            finally {
                ActiveSession = null;
            }
        }

        public async Task<IList<ISessionInfo>> QuerySessions() {
            var sessionQueryOptions = new QuerySessionsOptions();
            var results = await MultiplayerService.Instance.QuerySessionsAsync(sessionQueryOptions);
            return results.Sessions;
        }


        public static bool SessionsNameRequirementsMet(string sessionsName) {
            return !string.IsNullOrEmpty(sessionsName) && !string.IsNullOrWhiteSpace(sessionsName) &&
                   sessionsName.Length >= 2;
        }
    }
}