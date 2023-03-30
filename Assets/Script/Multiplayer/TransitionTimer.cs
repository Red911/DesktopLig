using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TransitionTimer : MonoBehaviour
{
    [Tooltip("In Seconds")] public float TimeLeft;
    public bool TimerOn = false;

    public TextMeshProUGUI TimerTxt;
    

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
        
        float seconds = Mathf.FloorToInt(currentTime % 60);
        
        TimerTxt.text = $"{seconds}";
    }
}
