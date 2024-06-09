using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    WinScreen winScreen;
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        winScreen = FindObjectOfType<WinScreen>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        winScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.isComplete == true)
        {
            quiz.gameObject.SetActive(false);
            winScreen.gameObject.SetActive(true);
            winScreen.ShowFinalScore();
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }
}
