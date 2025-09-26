using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnsiController : MonoBehaviour
{
    public void OpenQuiz()       => SceneManager.LoadScene("QuizScene");
    public void OpenEncyclopedia() => SceneManager.LoadScene("EncyclopediaScene");
    public void OpenHelp()       => SceneManager.LoadScene("HelpScene");
    public void OpenScan()       => SceneManager.LoadScene("ARScene");
    public void OpenHome()       => SceneManager.LoadScene("MainMenu");
    public void QuitApp()        => Application.Quit();
}
