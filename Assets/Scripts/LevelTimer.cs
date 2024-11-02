using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    
    private bool timerActive;
    public float Timer { get; private set; }
    
    private void Start()
    {
        Timer = 0f;
        timerText.text = "00:00";
        timerActive = true;
    }

    private void Update()
    {
        if (!timerActive)
            return;
        
        Timer += Time.deltaTime;
        
        timerText.text = Timer.ToString("N");
    }

    public void StopTimer()
    {
        timerActive = false;
        timerText.text = Timer.ToString("N");
    }
}