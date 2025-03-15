using System;
using System.Linq;
using GamersGrotto.Core;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerWorldSpaceUI : MonoBehaviour
    {
        [SerializeField] private Image healthFill;
        [SerializeField] private TMP_Text playerNameText;
        [SerializeField] private NetworkHealth networkHealth;
        [SerializeField] private Camera playerCamera;

        private void OnEnable()
        {
            networkHealth.onHealthChanged.AddListener(OnHealthChanged);
        }

        private void OnDisable()
        {
            networkHealth.onHealthChanged.RemoveListener(OnHealthChanged);
        }

        private void Start()
        {
            var playerName = AuthenticationService.Instance.PlayerName;
            SetPlayerName(playerName);
        }

        private void OnHealthChanged(float healthNormalized)
        {
            SetHealth(healthNormalized);
        }

        public void SetHealth(float healthNormalized)
        {
            healthFill.fillAmount = healthNormalized;
        }

        public void SetPlayerName(string playerName)
        {
            playerNameText.text = playerName;
        }

        private void Update()
        {
            var desired = playerCamera.transform.position.To(transform.position);
            transform.rotation = Quaternion.LookRotation(desired);
        }
    }
}