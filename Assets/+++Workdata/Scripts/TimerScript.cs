using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    private bool timerRunning;
    private float time;
    private int countTime = 3;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text countdown;
    [SerializeField] private UiControls uic;

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
        StartCoroutine(StartCountdown());
    }
    private IEnumerator StartCountdown()
    {
        uic.panelCountdown.SetActive(true);
        countdown.text = countTime.ToString();
        yield return new WaitForSeconds(1f);
        countTime--;
        countdown.text = countTime.ToString();
        yield return new WaitForSeconds(1f);
        countTime--;
        countdown.text = countTime.ToString();
        yield return new WaitForSeconds(1f);
        countTime--;
        countdown.text = countTime.ToString();
        timerRunning = true;
        uic.panelCountdown.SetActive(false);
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
