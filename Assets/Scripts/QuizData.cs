using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuizID
{
    Quiz1,
    Quiz2,
    Quiz3
}

[System.Serializable]
public class QuizData
{
    public QuizID id;          // ID quiz, bisa untuk filter
    public string title;       // judul quiz
    public string sceneToLoad; // untuk load halaman detail quiz
}