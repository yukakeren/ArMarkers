using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;  // <--- ini wajib kalau mau pakai Button
using UnityEngine.SceneManagement; // <--- ini buat LoadScene

public class DetailQuizManager : MonoBehaviour
{
    [Header("References")]
    public Transform contentParent;          // panel tempat spawn soal
    public GameObject quizQuestionPrefab;    // prefab soal
    public DetailQuizData[] allQuestions;    // semua soal (isi di inspector)
    public GameObject finishPopup;   // panel popup
    public Button okButton;          // tombol OK di popup

    private string selectedPaket;
    private int currentIndex = 0;
    private int score = 0;
    private DetailQuizData[] currentQuestions;

    void Start()
    {
        selectedPaket = QuizSelection.SelectedKey;
        if (string.IsNullOrEmpty(selectedPaket)) return;

        // filter soal sesuai paket
        currentQuestions = allQuestions
            .Where(q => q.paket == selectedPaket)
            .OrderBy(q => q.nomor)
            .ToArray();

        score = 0;
        finishPopup.SetActive(false);
        ShowQuestion(currentIndex);
    }

    void ShowQuestion(int index)
    {
        // clear previous
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        if (index >= currentQuestions.Length)
        {
            Debug.Log("Quiz selesai!");
            finishPopup.SetActive(true);

            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(() =>
            {
                Debug.Log("OK clicked, balik ke menu atau restart");
                SceneManager.LoadScene("QuizScene"); // balik ke menu quiz
            });

            return; // <-- Penting: hentikan eksekusi agar soal baru tidak dibuat
        }

        // kalau belum selesai, baru buat soal
        GameObject go = Instantiate(quizQuestionPrefab, contentParent);
        QuizQuestionItem item = go.GetComponent<QuizQuestionItem>();
        item.SetData(currentQuestions[index], score, OnAnswerSelected);
    }


    void OnAnswerSelected(string answer)
    {
        // cek jawaban
        if (answer == currentQuestions[currentIndex].correctAnswerID)
            score++;

        currentIndex++;
        ShowQuestion(currentIndex);
    }
}
