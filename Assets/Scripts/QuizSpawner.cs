using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject quizPrefab;       // prefab QuizItem
    public Transform contentParent;     // parent container (ScrollView Content)
    public QuizFilterManager filterManager; // assign di inspector

    [Header("Quiz Data")]
    public QuizData[] quizzes;          // array quiz yang diisi di inspector

    void Start()
    {
        foreach (var quiz in quizzes)
        {
            // Instantiate prefab
            GameObject go = Instantiate(quizPrefab, contentParent);

            // Assign data ke QuizItem
            QuizItem item = go.GetComponent<QuizItem>();
            item.ID = quiz.id;
            item.Title = quiz.title;
            item.sceneToLoad = quiz.sceneToLoad;

            // Set UI Text
            TMP_Text txt = go.transform.Find("Text (TMP)")?.GetComponent<TMP_Text>();
            if(txt != null) txt.text = item.Title;

            // Set button click
            Button btn = go.GetComponent<Button>();
            if(btn != null)
            {
                string sceneName = quiz.sceneToLoad; // capture variable untuk lambda
                btn.onClick.AddListener(() =>
                {
                    Debug.Log($"Klik quiz: {quiz.title}, sceneToLoad={sceneName}");
                    QuizSelection.SelectedKey = sceneName; 
                    SceneManager.LoadScene("DetailedQuizScene"); // load detail scene
                });
            }
        }

        // Refresh FilterManager supaya tahu semua item
        if(filterManager != null)
            filterManager.Refresh();
    }
}
