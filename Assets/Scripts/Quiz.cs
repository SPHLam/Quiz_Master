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

    void Start()
    {
        _questionText.text = _question.GetQuestion();

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = _question.GetAnswer(i);
        }
    }
}
