using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class Timer : MonoBehaviour
{

    private float countTime = 0.0f;

    public BehaviorSubject<float> countTimeBehaviorSubject = new BehaviorSubject<float>(0.0f);

    private bool isCount = false;

     

    public float GetCountTime(){
        return countTime;
    }

    public void TimerStart()
    {
        isCount = true;
        countTime = 0.0f;
    }

    public void TimerStop() {
        isCount = false;
    }

    void Update()
    {
        if (!isCount) return;

        countTime += Time.deltaTime;
        countTimeBehaviorSubject.OnNext(countTime);
    }
}
