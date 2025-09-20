using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizFilterManager : MonoBehaviour
{
    [Header("References")]
    public Transform contentParent; // parent container semua QuizItem
    private List<QuizItem> quizItems = new List<QuizItem>();

    void Start()
    {
        Refresh();
    }

    // panggil setiap kali QuizSpawner selesai spawn
    public void Refresh()
    {
        quizItems.Clear();
        foreach (Transform child in contentParent)
        {
            QuizItem item = child.GetComponent<QuizItem>();
            if(item != null) quizItems.Add(item);
        }
    }

    // tampilkan semua quiz
    public void ShowAll()
    {
        foreach(var item in quizItems)
            item.gameObject.SetActive(true);
    }

    // filter berdasarkan QuizID
    public void ShowByID(QuizID id)
    {
        foreach(var item in quizItems)
            item.gameObject.SetActive(item.ID == id);
    }

    // wrapper untuk button di UI
    public void ShowQuiz1() => ShowByID(QuizID.Quiz1);
    public void ShowQuiz2() => ShowByID(QuizID.Quiz2);
    public void ShowQuiz3() => ShowByID(QuizID.Quiz3);
}