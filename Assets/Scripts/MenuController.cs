using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OpenQuiz()         => SceneTransition.Instance.LoadScene("QuizScene");
    public void OpenEncyclopedia() => SceneTransition.Instance.LoadScene("EncyclopediaScene");
    public void OpenHelp()         => SceneTransition.Instance.LoadScene("HelpScene");
    public void OpenScan()         => SceneTransition.Instance.LoadScene("ARScene");
    public void OpenHome()         => SceneTransition.Instance.LoadScene("MainMenu");
    public void QuitApp()          => Application.Quit();
}