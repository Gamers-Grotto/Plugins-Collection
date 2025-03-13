using UnityEngine;

namespace GamersGrotto.Core
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}
