using UnityEngine;

namespace GamersGrotto
{
    public class Logger : MonoBehaviour
    {
        public void Log(string log) => Debug.Log(log);
        public void LogWarning(string warning) => Debug.LogWarning(warning);
        public void LogError(string error) => Debug.LogError(error);
    }
}