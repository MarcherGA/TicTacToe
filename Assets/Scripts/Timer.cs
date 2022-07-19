using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private int TimerLength;
    [SerializeField] private Text TimerText;
    private bool isTimerOn = false;

    [HideInInspector] public UnityEvent TimerEnded;
    public void ResetTimer()
    {
        if (isTimerOn)
            StopCoroutine(resetTimer());
        StartCoroutine(resetTimer());
    }

    IEnumerator resetTimer()
    {
        float timeLeft = TimerLength;
        while (timeLeft > 0)
        {
            yield return null;
            timeLeft -= Time.deltaTime;
            updateTimerText(timeLeft);
        }
        TimerEnded?.Invoke();
    }

    void updateTimerText(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}

