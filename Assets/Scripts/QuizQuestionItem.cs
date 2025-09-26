using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizQuestionItem : MonoBehaviour
{
    [Header("Data")]
    public string questionID;
    public string correctAnswerID;

    [Header("UI References")]
    public TMP_Text questionText;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button option4Button;
    public Button nextButton;
    public TMP_Text scoreText;   // tampil di prefab

    private string selectedAnswer = null;
    private Action<string> onAnswerSelected;

    // Custom Colors
    private readonly Color correctColor = new Color32(52, 140, 57, 102);   // #348C39, 20% opacity
    private readonly Color wrongColor   = new Color32(114, 10, 10, 102);   // #720A0A, 20% opacity
    private readonly Color defaultColor = new Color32(235, 235, 235, 255);

    public void SetData(DetailQuizData data, int currentScore, Action<string> onAnswer)
    {
        questionID = data.questionID;
        correctAnswerID = data.correctAnswerID;
        questionText.text = data.question;

        option1Button.GetComponentInChildren<TMP_Text>().text = data.option1;
        option2Button.GetComponentInChildren<TMP_Text>().text = data.option2;
        option3Button.GetComponentInChildren<TMP_Text>().text = data.option3;
        option4Button.GetComponentInChildren<TMP_Text>().text = data.option4;

        scoreText.text = $"Score: {currentScore}";

        selectedAnswer = null;
        onAnswerSelected = onAnswer;

        // Reset warna & listener
        ResetOptionColors();

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
        option3Button.onClick.RemoveAllListeners();
        option4Button.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();

        option1Button.onClick.AddListener(() => OnOptionClicked(data.option1, option1Button));
        option2Button.onClick.AddListener(() => OnOptionClicked(data.option2, option2Button));
        option3Button.onClick.AddListener(() => OnOptionClicked(data.option3, option3Button));
        option4Button.onClick.AddListener(() => OnOptionClicked(data.option4, option4Button));
    }

    private void OnOptionClicked(string answer, Button btn)
    {
        selectedAnswer = answer;

        // Feedback warna
        if (selectedAnswer == correctAnswerID)
            btn.image.color = correctColor;
        else
        {
            btn.image.color = wrongColor;
            HighlightCorrectAnswer();
        }

        // enable next button
        nextButton.interactable = true;
        nextButton.onClick.AddListener(() =>
        {
            onAnswerSelected?.Invoke(selectedAnswer);
        });
    }

    private void HighlightCorrectAnswer()
    {
        if (option1Button.GetComponentInChildren<TMP_Text>().text == correctAnswerID)
            option1Button.image.color = correctColor;
        else if (option2Button.GetComponentInChildren<TMP_Text>().text == correctAnswerID)
            option2Button.image.color = correctColor;
        else if (option3Button.GetComponentInChildren<TMP_Text>().text == correctAnswerID)
            option3Button.image.color = correctColor;
        else if (option4Button.GetComponentInChildren<TMP_Text>().text == correctAnswerID)
            option4Button.image.color = correctColor;
    }

    private void ResetOptionColors()
    {
        option1Button.image.color = defaultColor;
        option2Button.image.color = defaultColor;
        option3Button.image.color = defaultColor;
        option4Button.image.color = defaultColor;
        nextButton.interactable = false;
    }
}
