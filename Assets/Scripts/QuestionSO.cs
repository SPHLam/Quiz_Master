using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 5)]
    [SerializeField]
    private string question = "Are you gay?";

    public string GetQuestion()
    {
        return question;
    }
}
