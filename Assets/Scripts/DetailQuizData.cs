using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DetailQuizData
{
    public string paket;           // paket soal, sama dengan sceneToLoad dari QuizScene
    public int nomor;              // nomor urut soal di paket
    public string question;        // pertanyaan
    public string option1;
    public string option2;
    public string option3;
    public string option4;
    public string correctAnswerID; // jawaban benar (bisa string atau QuizOptionID)
    public string questionID;      // id unik soal
}
