using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AnswerNamespace;
using QuestionNamespace;


public class questions : MonoBehaviour
{

    public string[] answersTemplate;

    public TMP_Text questionText;
    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button nextButton;
    public GameObject endScreen;
    
    public TMP_Text pointsText;
    public TMP_Text timerText;
    public TMP_Text endScore;
    public TMP_Text numberOfQuestionText;

    public Image image;
    public Sprite sprite1, sprite2, sprite3, sprite4, sprite5;

    public AudioSource winSound;
    public AudioSource loseSound;
 

    
    public bool firstAnswer, secondAnswer, thirdAnswer;

    public int numberofQuestion;

    private float timeLeft = 30f; 
    private bool isTimerRunning = true;
    private int points = 0;
    private bool endScreenIsActive;
    private bool checkedAnswers = false;

    private List<int> usedNumbers = new List<int>();
    private List<Question> Questions = new List<Question>();


    // Start is called before the first frame update
    void Start()
    {
        endScreenIsActive = false;
        nextButton.gameObject.SetActive(false);

        numberofQuestion = 0;
        numberOfQuestionText.text = "Frage " + numberofQuestion.ToString();
        Debug.Log(answersTemplate);
        InitQuestionList();
        CallRandomMethod();
        pointsText.text = "Punkte: " + points.ToString();

        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            
            button.onClick.AddListener(() => ColorButton(button));
        }


    }

    private void ResetTimer()
    {
        timeLeft = 30f; 
        isTimerRunning = true; 
    }




    void CallRandomMethod()
    {

        InteractableButtons(true);
            nextButton.gameObject.SetActive(false);
        checkedAnswers = false;
        int randomNumber;

       

        if (usedNumbers.Count == 5)
        {
            Debug.Log("Alle Fragen wurden durchgespielt.");
            endScore.text = points.ToString();
            GoToEndScreen();
            
            return;
        }

        do
        {
            randomNumber = Random.Range(0, 5); 
        } while (usedNumbers.Contains(randomNumber)); 

        usedNumbers.Add(randomNumber);

        QuestionPage(Questions[randomNumber]);

        if (numberofQuestion < 5)
        {
            numberofQuestion++;
            numberOfQuestionText.text = "Frage " + numberofQuestion.ToString();
        }
        else
        {
            numberofQuestion = 5;
            numberOfQuestionText.text = "Frage " + numberofQuestion.ToString();
        }
    }



    //    public void QuestionTemplate(string question, string answer1, string answer2, string answer3)
    //{
        
    //    string questionTemplate = question;
    //    answersTemplate = new string[] { answer1, answer2, answer3 };
    //    questionText.text = questionTemplate;
    //    answerButton1.GetComponentInChildren<TMP_Text>().text = answersTemplate[0];
    //    answerButton2.GetComponentInChildren<TMP_Text>().text = answersTemplate[1];
    //    answerButton3.GetComponentInChildren<TMP_Text>().text = answersTemplate[2];
       

    //}

 

    public static void Shuffle<T>( IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            //int k = new Random.Next(n + 1); //zum herumexperimentieren...
            int k = 0;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


    public void QuestionPage(Question question)
    {

        questionText.text = question.questionText;
        //Shuffle(question.Answers);

        image.GetComponent<Image>().sprite = question.sprite;

        answerButton1.GetComponentInChildren<TMP_Text>().text = question.Answers[0].answerText;
        answerButton2.GetComponentInChildren<TMP_Text>().text = question.Answers[1].answerText;
        answerButton3.GetComponentInChildren<TMP_Text>().text = question.Answers[2].answerText;

    }

    private void InitQuestionList()
    {
        Question q = new Question("Welche der folgenden Planeten unseres Sonnensystems sind Gasriesen?", sprite1, new Answer("Mars", false), new Answer("Jupiter", true), new Answer("Venus", false));
        Questions.Add(q);

        Question q1 = new Question("Welches Element hat die höchste Dichte?", sprite2, new Answer("Platin", false), new Answer("Osmium", true), new Answer("Gold", false));
        Questions.Add(q1);

        Question q2 = new Question("Wer war der erste Mensch im All?", sprite3, new Answer("Yuri Gagarin", true), new Answer("James Bond", false), new Answer("Forrest Gump", false));
        Questions.Add(q2);

        Question q3 = new Question("Wer entdeckte die Penicillin-Bakterien?", sprite4, new Answer("Alexander Fleming", true), new Answer("Heinz Fischer", false), new Answer("Josef Huber", false));
        Questions.Add(q3);

        Question q4 = new Question("Welcher Fluss ist der längste der Welt?", sprite5, new Answer("Amazonas", false), new Answer("Nil", true), new Answer("Schwemmbach", false));
        Questions.Add(q4);

      
    }

    //Question1
   
 

    public void NextButtonClick()
    {

        ResetButtons();
        CallRandomMethod();
        ResetTimer();



    }

    private void GoToEndScreen()
    {
        usedNumbers.Clear();
        endScreen.SetActive(true);
        isTimerRunning = false;
        timerText.text = timeLeft.ToString();
        endScreenIsActive = true;

    }
    void Update()
    {
        if (isTimerRunning)
        {
            timeLeft -= Time.deltaTime; 
            if (timeLeft <= 0)
            {
                isTimerRunning = false;
                InteractableButtons(false);
                CheckAnswer();


                ResetButtons();
                CallRandomMethod();
                ResetTimer();
            }
            if(endScreenIsActive || checkedAnswers)
            {
                isTimerRunning = false;
            }
            timerText.text = "Timer: " + Mathf.Round(timeLeft).ToString(); 
        }
        
    }
    public void ColorButton(Button button)
    {
        if (isTimerRunning && button.CompareTag("AnswerButton"))
        {
            button.onClick.AddListener(() =>
            {
                button.GetComponent<Image>().color = Color.blue;
            });
        }
        else
        {
            Debug.LogWarning("Der Button hat nicht den Tag 'AnswerButton'.");
        }
    }



    private void UpdateScore(int value)
    {
        points += value;
        if (points < 0) points = 0;
        pointsText.text = "Punkte: " + points.ToString();
    }

    public void CheckAnswer()
    {
        InteractableButtons(false);
        checkedAnswers = true;
        bool selectedAnswer1 = answerButton1.GetComponent<Image>().color == Color.blue;
        bool selectedAnswer2 = answerButton2.GetComponent<Image>().color == Color.blue;
        bool selectedAnswer3 = answerButton3.GetComponent<Image>().color == Color.blue;

        
        nextButton.gameObject.SetActive(true);


        Question currentQuestion = GetCurrentQuestion();

       
        if (selectedAnswer1 && currentQuestion.Answers[0].isRight)
        {
            Debug.Log("Antwort 1 ist richtig!");
            SetButtonColor(answerButton1, Color.green);
            winSound.Play();
    
            UpdateScore(1);
        }
        else if (selectedAnswer2 && currentQuestion.Answers[1].isRight)
        {
            Debug.Log("Antwort 2 ist richtig!");
            SetButtonColor(answerButton2, Color.green);
            winSound.Play();
            UpdateScore(1);
        }
        else if (selectedAnswer3 && currentQuestion.Answers[2].isRight)
        {
            Debug.Log("Antwort 3 ist richtig!");
            SetButtonColor(answerButton3, Color.green);
            winSound.Play();
            UpdateScore(1);
        }
        else
        {
            Debug.Log("Falsche Antwort!");
           
            if (selectedAnswer1) SetButtonColor(answerButton1, Color.red);
            if (selectedAnswer2) SetButtonColor(answerButton2, Color.red);
            if (selectedAnswer3) SetButtonColor(answerButton3, Color.red);

           if(currentQuestion.Answers[0].isRight) SetButtonColor(answerButton1, Color.green);
            if (currentQuestion.Answers[1].isRight) SetButtonColor(answerButton2, Color.green);
            if (currentQuestion.Answers[2].isRight) SetButtonColor(answerButton3, Color.green);

            loseSound.Play();


        }
    }

   
    private Question GetCurrentQuestion()
    {
        int lastQuestionIndex = usedNumbers[usedNumbers.Count - 1];
        return Questions[lastQuestionIndex];
    }

    
    private void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
    private void InteractableButtons(bool disable)
    {
        answerButton1.interactable = disable;
        answerButton2.interactable = disable;
        answerButton3.interactable = disable;
    }
    public void ResetButtons()
    {
        answerButton1.interactable = false;
        answerButton1.interactable = true;
        answerButton1.GetComponent<Image>().color = Color.white;

        answerButton2.interactable = false;
        answerButton2.interactable = true;
        answerButton2.GetComponent<Image>().color = Color.white;

        answerButton3.interactable = false;
        answerButton3.interactable = true;
        answerButton3.GetComponent<Image>().color = Color.white;
    }

    //Wenn ich auf weiter klicke, sollen die Antworten geprüft werden und dann erst weiter gegangen werden.
    // ToDos

    /*
     *
     * 
     *
     * 
     * 
     * 
     *
     * Anzahl Fragen noch ergänzen + muss sich ebenfalls resetten können
     * 
     * Image einfügen, Template dafür erschaffen
     */
}
