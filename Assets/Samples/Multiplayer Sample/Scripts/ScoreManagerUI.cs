using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class ScoreManagerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private void Start()
        {
            UpdateScores(ScoreManager.Instance.ScoreList);
        }

        private void OnEnable()
        {
            ScoreManager.Instance.OnScoresUpdated.AddListener(UpdateScores);
        }

        private void OnDisable()
        {
            ScoreManager.Instance.OnScoresUpdated.RemoveListener(UpdateScores);
        }

        private void UpdateScores(List<ScoreData> arg0)
        {
            if (arg0 == null || arg0.Count == 0)
            {
                scoreText.text = "No Score Available yet.. kill somebody!";
                return;
            }
            
            var text = string.Join("\n", arg0.Select(x => $"{SessionManager.Instance.GetPlayerName(x.PlayerId)} : {x.Score}"));
            scoreText.text = text;
        }
    }
}