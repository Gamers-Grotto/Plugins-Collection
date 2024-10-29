using System;
using System.Collections;
using GamersGrotto.Damage_System;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockForce = 5f;
    [SerializeField] private float knockBackDuration = 0.5f;

    public UnityEvent knockBackStarted;
    public UnityEvent knockBackEnded;
    
    private Rigidbody rb;
    private Locomotion locomotion;
    private bool IsBeingKnockedBack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        locomotion = GetComponent<Locomotion>();
    }

    public void Knock()
    {
        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine()
    {
        if(IsBeingKnockedBack)
            yield break; 
        
        IsBeingKnockedBack = true;
        locomotion.enabled = false;
        knockBackStarted?.Invoke();
        
        rb.AddForce(new Vector3(-rb.linearVelocity.x, 1f, 0f).normalized * knockForce, ForceMode.Impulse);
        yield return new WaitForSeconds(knockBackDuration);
        
        locomotion.enabled = true;
        IsBeingKnockedBack = false;
        knockBackEnded?.Invoke();
    }
}