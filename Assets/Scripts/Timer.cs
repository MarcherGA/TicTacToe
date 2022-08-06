using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int TimerLength { get => _timerLength; set => _timerLength = value; }
    
    [SerializeField] private TMP_Text TimerText;
    private int _timerLength;
    private bool isTimerOn = false;
    private Coroutine _currentTimer;

    public Action OnTimerEnd;

    public void ResetTimer(int timerLength)
    {
        TimerLength = timerLength;
        ResetTimer();
    }

    public void ResetTimer()
    {
        Stop();
        _currentTimer = StartCoroutine(resetTimer());
    }
    public void Stop()
    {
        if (isTimerOn)
            StopCoroutine(_currentTimer);
    }

    IEnumerator resetTimer()
    {
        float timeLeft = TimerLength;
        isTimerOn = true;
        while (timeLeft > 0)
        {
            updateTimerText(timeLeft);
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        isTimerOn = false;
        OnTimerEnd?.Invoke();
    }

    void updateTimerText(float currentTime)
    {
        float minutes = Mathf.RoundToInt(currentTime / 60);
        float seconds = Mathf.RoundToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}

