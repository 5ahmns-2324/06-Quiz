using UnityEditor;
using UnityEngine;
using AnswerNamespace;
using System.Collections.Generic;
using UnityEngine.UI;

namespace QuestionNamespace
{
    public class Question
    {
        public string questionText;
        public Sprite sprite;
       

        public List<Answer> Answers = new List<Answer>();
        public Question(string questionText, Sprite sprite, Answer ans1, Answer ans2, Answer ans3)
        {

            this.questionText = questionText;

            this.sprite = sprite;
           


            Answers.Add(ans1);
            Answers.Add(ans2);
            Answers.Add(ans3);

        }

    }
}