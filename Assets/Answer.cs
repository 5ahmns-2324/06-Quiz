using UnityEditor;
using UnityEngine;

namespace AnswerNamespace
{
    public class Answer
    {

        public string answerText;
        public bool isRight;

        public Answer(string answerText, bool isRight)
        {
            this.answerText = answerText;
            this.isRight = isRight;
        }
       
    }
}