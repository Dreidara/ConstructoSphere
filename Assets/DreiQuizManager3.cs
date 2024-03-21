using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DreiQuizManager3 : MonoBehaviour
{
    public List<DreiQnA> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public Text QuestionTxT;
    public Text ScoreTxT;

    public int score;

    int totalQuestions = 0;

    public void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        generateQuestion();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<DreiAnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<DreiAnswerScript>().isCorrect = true;
            }
        }
    }

    void GameOver()
    {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxT.text = score + "/" + totalQuestions;
    }

    public void Wrong()
    {
        //maling sagot pinili bobo siya chzx
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void correct()
    {
        //plus one pag tama
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void generateQuestion ()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxT.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }


        
    }
}
