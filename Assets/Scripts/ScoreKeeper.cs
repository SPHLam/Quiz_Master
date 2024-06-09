using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswer = 0;
    int numberOfQuestion = 0;
    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public void IncrementCorrectAnswer()
    {
        correctAnswer++;
    }

    public int GetNumberOfQuestionHasAnswered()
    {
        return numberOfQuestion;
    }

    public void IncrementNumberOfQuestionHasAnswered()
    {
        ++numberOfQuestion;
    }

    public double CalculateScore()
    {
        return Mathf.RoundToInt((float) correctAnswer * 100 / numberOfQuestion);
    }
}
