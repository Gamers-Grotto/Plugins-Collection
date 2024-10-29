using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}