using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public Vector3 rotation;
        public float movementSmoothSpeed = 1f;
        public float rotationSlerpSpeed = 1f;

        public bool useSmoothDamp = false;
        
        private Vector3 worldOverviewPosition;
        private Vector3 worldOverviewRotation;
        private Vector3 smoothingVelocity;

        private void Awake()
        {
            worldOverviewPosition = transform.position;
            worldOverviewRotation = transform.eulerAngles;
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                var desiredPosition = (target.position + offset);

                transform.position = useSmoothDamp 
                    ? Vector3.SmoothDamp(transform.position, desiredPosition, ref smoothingVelocity, movementSmoothSpeed) 
                    : Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * movementSmoothSpeed);
                
                var desiredRotation = Quaternion.Euler(rotation);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSlerpSpeed);
            }
            else
            {
                transform.position = worldOverviewPosition;
                transform.rotation = Quaternion.LookRotation(worldOverviewRotation);
            }
        }
    }
}