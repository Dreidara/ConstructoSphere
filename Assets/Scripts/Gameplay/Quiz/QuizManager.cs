using ConstructoSphere.Gameplay;
using ConstructoSphere.Main;
using ConstructoSphere.Utilities;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConstructoSphere.Quiz
{
    public class QuizManager : MonoBehaviour
    {
        public static QuizManager Instance;

        [Space]
        [SerializeField] QuizCard[] _quizCards;
        [SerializeField] QuizCard _currentQuizCard;
        [Space]
        [SerializeField] TextMeshProUGUI _questionsText;
        [SerializeField] TextMeshProUGUI _choiceA;
        [SerializeField] TextMeshProUGUI _choiceB;
        [SerializeField] TextMeshProUGUI _choiceC;
        [SerializeField] TextMeshProUGUI _choiceD;
        [SerializeField] TextMeshProUGUI _correctOrWrongText;
        [SerializeField] TextMeshProUGUI _explanationText;
        [SerializeField] GameObject _quizPanel;
        [SerializeField] GameObject _correctOrWrongPanel;

        public float hits;
        private float questionHits;
        private int questionID;

        [Header("Time Reference")]
        public TextMeshProUGUI timeLeftText;
        public float timeLeft;

        private bool _isCorrect = false;
        bool isFinishQuiz = false;

        private void Awake() => Instance = this;

      

        public void StartQuiz()
        {
            StartCoroutine(InitializeQuiz());
        }


        public IEnumerator InitializeQuiz()
        {
            UI_Loader.Instance.Show("Starting Quiz . . . .");
            yield return new WaitForSeconds(1f);
            UI_Loader.Instance.Close();
            UIManager.Instance.ShowCanvas("Quiz");
            questionID = 0;
            int levelModeUnlock = GameDataManager.Instance.currentLevelId;
            _currentQuizCard = _quizCards[levelModeUnlock - 1];
            _currentQuizCard.quizItems.Shuffle();
            questionHits = _currentQuizCard.quizItems.Length;

            ShowQuiz();
        }

        private void ShowQuiz()
        {
            UIManager.Instance.GameState = Main.Enum_GameState.Quiz;
            timeLeft = 30f;
            _questionsText.text = _currentQuizCard.quizItems[questionID].questions;

            _choiceA.text = _currentQuizCard.quizItems[questionID].AnswerA;
            _choiceB.text = _currentQuizCard.quizItems[questionID].AnswerB;
            _choiceC.text = _currentQuizCard.quizItems[questionID].AnswerC;
            _choiceD.text = _currentQuizCard.quizItems[questionID].AnswerD;
        }

        private void Update()
        {
            //if (UIManager.Instance.GameState == Main.Enum_GameState.Quiz)
            //{
            //    timeLeftText.text = "TIME: " + Mathf.RoundToInt(timeLeft).ToString();
            //    timeLeft -= Time.deltaTime;

            //    if (timeLeft <= 10 && timeLeft > 0) // Start flashing the image when timer is 10 or less.
            //    {
            //        imageToFlash.enabled = true; // Show the image

            //        if (isFinishQuiz == false)
            //        {
            //            FlashImage();
            //            isFinishQuiz = true;
            //        }

            //    }

            //    else if (timeLeft > 10)
            //    {
            //        imageToFlash.enabled = false; // Hide the image
            //    }

            //    else if (timeLeft <= 0)
            //    {
            //        isFinishQuiz = false;
            //        imageToFlash.enabled = false; // Hide the image

            //        questionID += 1;
            //        //GameManager.Instance.Resume();
            //        //GameManager.Instance.Enemy_Attack(1f);
            //        //GameManager.Instance.gameMode = GameMode.Playing;
            //        // Result();
            //    }
            //}
        }

        void FlashImage()
        {
            //imageToFlash.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        string correctAnswer;
        public void Answer(string letter)
        {
            if (UIManager.Instance.GameState == Main.Enum_GameState.Quiz)
            {
                if (letter == _currentQuizCard.quizItems[questionID].CorrectChoice)
                {
                    hits += 1;
                    StartCoroutine(CorrectAnswer());
                }

                else
                {
                    StartCoroutine(WrongAnswer());
                }
            }
        }

        IEnumerator CorrectAnswer()
        {
            _isCorrect = true;
            _quizPanel.SetActive(false);
            _explanationText.text = _currentQuizCard.quizItems[questionID].Explanation;
            _correctOrWrongText.text = "CORRECT";
            _correctOrWrongPanel.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
            ResumeGamePlay();

        }

        IEnumerator WrongAnswer()
        {
            _isCorrect = false;
            _quizPanel.SetActive(false);
            _explanationText.text = "";
            _correctOrWrongText.text = "TRY AGAIN";
            _correctOrWrongPanel.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
            _correctOrWrongPanel.SetActive(false);
            _quizPanel.SetActive(true);
            NextQuestion();

        }

        private void ResumeGamePlay()
        {
            if (_isCorrect)
            {
                _correctOrWrongPanel.SetActive(false);
                _quizPanel.SetActive(true);
                NextQuestion();
            }
        }

        private void NextQuestion()
        {
            questionID += 1;

            if (questionID <= (questionHits - 1))
            {

                timeLeft = 30f;
                _questionsText.text = _currentQuizCard.quizItems[questionID].questions;

                _choiceA.text = _currentQuizCard.quizItems[questionID].AnswerA;
                _choiceB.text = _currentQuizCard.quizItems[questionID].AnswerB;
                _choiceC.text = _currentQuizCard.quizItems[questionID].AnswerC;
                _choiceD.text = _currentQuizCard.quizItems[questionID].AnswerD;

            }

            else
            {
                //UIManager.Instance.GameState = Main.Enum_GameState.Playing;
                _quizPanel.SetActive(false);
                UIManager.Instance.UpdateStarRating();
                UIManager.Instance.ShowCanvas("Success");
            }
        }
    }
}

