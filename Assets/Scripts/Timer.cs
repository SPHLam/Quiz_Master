using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float timeToCompleteQuestion = 20f;
    [SerializeField]
    float timeToShowCorrectAnswer = 10f;

    public bool isAnswering = false;
    float timerValue;
    void Update()
    {
        UpdateTimer();
    }
    
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (timerValue <= 0)
        {
            if (!isAnswering)
            {
                timerValue = timeToShowCorrectAnswer;
                isAnswering = true;
            }
            else
            {
                timerValue = timeToCompleteQuestion;
                isAnswering = false;
            }
        }

        Debug.Log(timerValue);
    }
}
