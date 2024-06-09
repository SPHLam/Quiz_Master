using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    private QuestionSO _currentQuestion;
    [SerializeField] List<QuestionSO> _questionList = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI _currentQuestionText;

    [Header("Answers")]
    [SerializeField]  GameObject[] _answerButtons = new GameObject[4];
    private int _correctAnswerIndex;
    private bool hasAnsweredEarly = false;

    [Header("Button Sprites")]
    [SerializeField]
    private Sprite defaultAnswerSprite;
    [SerializeField]
    private Sprite correctAnswerSprite;

    [Header("Timer")]
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper _scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        _scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        if (_scoreKeeper == null)
        {
            Debug.Log("Score Keeper not found!");
        }

        scoreText.text = "Score: 0%";
        progressBar.maxValue = _questionList.Count;
        progressBar.value = 0;
        GetNextQuestion();
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
        _currentQuestionText.text = _currentQuestion.GetQuestion();

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = _currentQuestion.GetAnswer(i);
        }
    }

    public void GetNextQuestion()
    {
        if (_questionList.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestionAndAnswers();
            progressBar.value++;
            _scoreKeeper.IncrementNumberOfQuestionHasAnswered();
        }
    }

    private void GetRandomQuestion()
    {
        int random = Random.Range(0, _questionList.Count);
        _currentQuestion = _questionList[random];

        if (_questionList.Contains(_currentQuestion))
            _questionList.Remove(_currentQuestion);
    }

    public void OnAnswerSelected(int buttonIndex)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(buttonIndex);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + _scoreKeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
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
        _correctAnswerIndex = _currentQuestion.GetCorrectAnswerIndex();
        Image buttonImage;
        if (buttonIndex == _correctAnswerIndex)
        {
            _currentQuestionText.text = "Congrats";
            _scoreKeeper.IncrementCorrectAnswer();
        }
        else 
        {
            _currentQuestionText.text = "The correct answer was " + _currentQuestion.GetAnswer(_correctAnswerIndex);
        }
        buttonImage = _answerButtons[_correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }
}
