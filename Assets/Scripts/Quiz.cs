using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField]
    private QuestionSO _question;
    [SerializeField]
    private TextMeshProUGUI _questionText;

    [Header("Answers")]
    [SerializeField]
    GameObject[] _answerButtons = new GameObject[4];
    private int _correctAnswerIndex;
    private bool hasAnsweredEarly = false;

    [Header("Button Sprites")]
    [SerializeField]
    private Sprite defaultAnswerSprite;
    [SerializeField]
    private Sprite correctAnswerSprite;

    [Header("Timer")]
    Timer timer;

    void Start()
    {
        _correctAnswerIndex = _question.GetCorrectAnswerIndex();
        GetNextQuestion();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    void Update()
    {
        if (timer.nextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.nextQuestion = false;
        }
        else if (!hasAnsweredEarly && timer.showingCorrectAnswer)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }


    private void DisplayQuestionAndAnswers()
    {
        _questionText.text = _question.GetQuestion();

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = _question.GetAnswer(i);
        }
    }

    public void GetNextQuestion()
    {
        DisplayQuestionAndAnswers();
        SetButtonState(true);
        SetDefaultButtonSprite();
    }

    public void OnAnswerSelected(int buttonIndex)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(buttonIndex);
        SetButtonState(false);
        timer.CancelTimer();
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }

    private void SetDefaultButtonSprite()
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    public void DisplayAnswer(int buttonIndex)
    {
        Image buttonImage;
        if (buttonIndex == _correctAnswerIndex)
        {
            _questionText.text = "Congrats";
        }
        else 
        {
            _questionText.text = "The correct answer was " + _question.GetAnswer(_correctAnswerIndex);
        }
        buttonImage = _answerButtons[_correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }
}
