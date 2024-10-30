using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraMovementType{ HardLock, Interpolate, SmoothDamp }
    public enum UpdateType { Update, FixedUpdate, LateUpdate }
    
    public Transform target;
    
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private CameraMovementType cameraMovementType;
    [SerializeField] private UpdateType updateType;
    
    private Vector3 currentVelocity;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void FixedUpdate()
    {
        if(updateType != UpdateType.FixedUpdate)
            return;
        
        if(target == null)
            return;

        MoveCamera();
    }

    private void Update()
    {
        if(updateType != UpdateType.Update)
            return;
        
        if(target == null)
            return;

        MoveCamera();
    }

    private void LateUpdate()
    {
        if(updateType != UpdateType.LateUpdate)
            return;
        
        if(target == null)
            return;

        MoveCamera();
    }

    private void MoveCamera()
    {
        switch (cameraMovementType)
        {
            case CameraMovementType.HardLock:
                transform.position = target.transform.position + offset;
                break;
            case CameraMovementType.Interpolate:
                transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, smoothTime);
                break;
            case CameraMovementType.SmoothDamp:
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    target.position + offset,
                    ref currentVelocity,
                    smoothTime,
                    maxSpeed);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}