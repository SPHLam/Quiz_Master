using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float timeToCompleteQuestion = 20f;
    [SerializeField]
    float timeToShowCorrectAnswer = 10f;

    [SerializeField]
    Image timerImage;

    public bool showingCorrectAnswer = false;
    public bool nextQuestion = false;
    private float timerValue; // time left
    private float fillFraction; // for timer UI 
    private int answerChoice;

    Quiz quiz;
    void Start()
    {
        timerValue = timeToCompleteQuestion;
        quiz = GameObject.Find("QuizCanvas").GetComponent<Quiz>();
    }
    void Update()
    {
        UpdateTimer();
    }
    
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (timerValue <= 0)
        {
            if (showingCorrectAnswer) // Showing answer -> Next question
            {
                nextQuestion = true;
                timerValue = timeToCompleteQuestion;
                showingCorrectAnswer = false;
            }
            else // Question -> Showing answer
            {
                timerValue = timeToShowCorrectAnswer;
                showingCorrectAnswer = true;
            }
        }
        else // Timer UI
        {
            if (showingCorrectAnswer)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }

            timerImage.fillAmount = fillFraction;

            // Debug.Log(fillFraction + "\n" + timerValue);
        }
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }
}
