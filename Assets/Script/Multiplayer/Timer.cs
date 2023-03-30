using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   [Tooltip("In Seconds")] public float TimeLeft;
    public bool TimerOn = false;

    public TextMeshProUGUI TimerTxt;

    private void Start()
    {
        TimerOn = true;
    }

    private void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime++;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{00:00} : {1:00}", minutes, seconds);
    }
}
