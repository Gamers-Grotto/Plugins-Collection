using GamersGrotto.Core;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerWorldSpaceUI : MonoBehaviour
    {
        [SerializeField] private Image healthFill;
        [SerializeField] private TMP_Text playerNameText;
        [SerializeField] private NetworkHealth networkHealth;
        
        private Camera mainCam;
        
        private void OnEnable()
        {
            networkHealth.onHealthChanged.AddListener(OnHealthChanged);
            mainCam = Camera.main;
            
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }

        

        private void OnDisable()
        {
            networkHealth.onHealthChanged.RemoveListener(OnHealthChanged);
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
        
        void OnClientConnected(ulong connectingClientId) {
            Debug.Log($"Client connected - PlayerworldSpaceUI {connectingClientId}");
            //Set other players names
            //Set own name
            
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
            if (mainCam == null)
                mainCam = Camera.main;

            if (mainCam != null)
            {
                var desired = mainCam.transform.position.To(transform.position);
                transform.rotation = Quaternion.LookRotation(desired);
            }
        }
    }
}