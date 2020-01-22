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
        UpdateEvent?.Invoke(Time);
       

    }

    IEnumerator TimerEvent()
    {
        yield return new WaitForSeconds(1);
        OnTime();
        if (startTimer) 
        {
            Time++;
            StartCoroutine(nameof(TimerEvent));
        }
    }


    /// <summary>
    /// 將某個函式排定在一段時間後執行，需注意只能從 Main Thread 中呼叫 !!
    /// </summary>
    /// <param name="action">要執行的函式</param>
    /// <param name="delayTime">延遲時間</param>
    public void Delay(Action action, float delayTime = 0f)
    {
        StartCoroutine(_Delay(action, delayTime));
    }

    IEnumerator _Delay(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action();
    }
}
