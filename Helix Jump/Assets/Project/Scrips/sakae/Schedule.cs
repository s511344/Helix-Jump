using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Schedule:MonoBehaviour
{
    public int Time { get; private set; }
    public Action<int> UpdateEvent;

    bool startTimer = false;
    Coroutine timer;
    public void StartTimer() 
    {
        startTimer = true;
        Time = 0;
        timer = StartCoroutine(TimerEvent());   
        
    }
    public void StopTimer()
    {
        startTimer = false;
        StopCoroutine(timer);
    }
    void OnTime() 
    {
        Time++;
        UpdateEvent?.Invoke(Time);

    }

    IEnumerator TimerEvent()
    {
        yield return new WaitForSeconds(1);
        OnTime();
        if (startTimer) 
        {
            StartCoroutine(nameof(TimerEvent));
        }
    }
}
