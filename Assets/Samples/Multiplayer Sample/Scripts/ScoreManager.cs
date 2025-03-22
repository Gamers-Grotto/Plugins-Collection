using System;
using System.Collections.Generic;
using System.Text;
using GamersGrotto.Core;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Multiplayer_Sample
{
    public class ScoreManager : NetworkBehaviour
    {
        private NetworkList<ScoreData> Scores = new ();
        public UnityEvent<List<ScoreData>> OnScoresUpdated;

        public static ScoreManager Instance;
        
        [RuntimeInitializeOnLoadMethod]
        private static void SelfInstantiate()
        {
            var go = new GameObject("ScoreManager");
            go.GetOrAddComponent<ScoreManager>();
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AddScore(ulong clientId, int points)
        {
            if(!IsHost)
                return;
            
            for (var i = 0; i < Scores.Count; i++)
            {
                if (Scores[i].PlayerId == clientId)
                {
                    var newValue = Scores[i];
                    newValue.Score += points;
                    Scores[i] = newValue;
                    break;
                }
            }
            
            Scores.Add(new ScoreData()
            {
                PlayerId = clientId,
                Score = points
            });
        }

        public void ResetScores()
        {
            Scores.Clear();
        }

        [ServerRpc]
        public void AddScoreServerRpc(ulong clientId, int points)
        {
            AddScore(clientId, points);
        }

        private void OnEnable()
        {
            Scores.OnListChanged += OnListChanged;
        }

        private void OnDisable()
        {
            Scores.OnListChanged -= OnListChanged;
        }

        private void OnListChanged(NetworkListEvent<ScoreData> changeevent)
        {
            OnScoresUpdated?.Invoke(Scores.AsList());
        }

        [ContextMenu("Log Scores")]
        public void Test()
        {
            var scores = Scores.AsList();

            var sb = new StringBuilder();
            foreach (var score in scores)
            {
                sb.AppendLine($"Player {score.PlayerId} : score : {score.Score}");
            }
            Debug.Log(sb);
        }
    }

    public struct ScoreData : IEquatable<ScoreData>
    {
        public ulong PlayerId;
        public int Score;

        public bool Equals(ScoreData other)
        {
            return PlayerId == other.PlayerId && Score == other.Score;
        }

        public override bool Equals(object obj)
        {
            return obj is ScoreData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PlayerId, Score);
        }
    }
}