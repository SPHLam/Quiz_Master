using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField]
    private QuestionSO _question;

    [SerializeField]
    private TextMeshProUGUI _questionText;

    [SerializeField]
    GameObject[] _answerButtons = new GameObject[4];

    private int _correctAnswerIndex;

    [SerializeField]
    private Sprite defaultAnswerSprite;
    [SerializeField]
    private Sprite correctAnswerSprite;

    void Start()
    {
        GetNextQuestion();
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

    private void GetNextQuestion()
    {
        DisplayQuestionAndAnswers();
        SetButtonState(true);
        SetDefaultButtonSprite();
    }

    public void OnAnswerSelected(int buttonIndex)
    {
        Image buttonImage;

        _correctAnswerIndex = _question.GetCorrectAnswerIndex();

        if (buttonIndex == _correctAnswerIndex)
        {
            _questionText.text = "Correct";
            buttonImage = _answerButtons[buttonIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            _questionText.text = "Sorry, the answer was " + _question.GetAnswer(_correctAnswerIndex);
            buttonImage = _answerButtons[_correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
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
}
