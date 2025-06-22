using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    private bool timerRunning;
    private float time;
    [SerializeField] private TMP_Text text;
    

    void Start()
    {
        time = 0;
        timerRunning = false;
    }
    void Update()
    {
        if (timerRunning)
        {
            time = time + Time.deltaTime;
        }
        TimeSpan _time = TimeSpan.FromSeconds(time);
        text.text = _time.Minutes.ToString() + ":" + _time.Seconds.ToString() + ":" + _time.Milliseconds.ToString();

    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
