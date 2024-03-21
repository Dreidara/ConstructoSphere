using UnityEngine;

[System.Serializable]
public class QuizItem
{
    [Header("Questions")]
    [TextArea(5, 10)][SerializeField] public string questions;

    [Header("Choices Answers")]
    [SerializeField] public string AnswerA;
    [SerializeField] public string AnswerB;
    [SerializeField] public string AnswerC;
    [SerializeField] public string AnswerD;

    [Header("Correct Answers")]
    [TextArea(2, 10)] public string CorrectChoice;

    [Header("Explanation Description")]
    [TextArea(3, 5)] public string Explanation;

}
[CreateAssetMenu(fileName = "Quiz Card", menuName = "New Quiz Card")]
public class QuizCard : ScriptableObject
{
    public QuizItem[] quizItems;
}

public static class RandomExtensions
{
    public static void Shuffle<T>(this T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}

