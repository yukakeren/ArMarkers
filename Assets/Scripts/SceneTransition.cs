using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [Header("Daun")]
    public RectTransform daun1;
    public RectTransform daun2;
    public RectTransform daun3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // biar nggak ke-destroy pas pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(PlayTransition(sceneName));
    }

    private IEnumerator PlayTransition(string sceneName)
    {
        // animasi masuk
        yield return StartCoroutine(AnimateLeavesIn());

        // ganti scene
        SceneManager.LoadScene(sceneName);

        // animasi keluar
        yield return StartCoroutine(AnimateLeavesOut());
    }

    private IEnumerator AnimateLeavesIn()
    {
        float duration = 1f;
        float time = 0f;

        // posisi awal
        daun1.anchoredPosition = new Vector2(3000, 1688);
        daun2.anchoredPosition = new Vector2(-2000, 625);
        daun3.anchoredPosition = new Vector2(3000, 476);

        // target
        Vector2 target1 = new Vector2(635, 1688);
        Vector2 target2 = new Vector2(252, 625);
        Vector2 target3 = new Vector2(835, 476);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);

            daun1.anchoredPosition = Vector2.Lerp(new Vector2(3000, 1688), target1, t);
            daun2.anchoredPosition = Vector2.Lerp(new Vector2(-2000, 625), target2, t);
            daun3.anchoredPosition = Vector2.Lerp(new Vector2(3000, 476), target3, t);

            yield return null;
        }
    }

    private IEnumerator AnimateLeavesOut()
    {
        float duration = 1f;
        float time = 0f;

        Vector2 start1 = new Vector2(635, 1688);
        Vector2 start2 = new Vector2(252, 625);
        Vector2 start3 = new Vector2(835, 476);

        Vector2 target1 = new Vector2(3000, 1688);
        Vector2 target2 = new Vector2(-2000, 625);
        Vector2 target3 = new Vector2(3000, 476);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);

            daun1.anchoredPosition = Vector2.Lerp(start1, target1, t);
            daun2.anchoredPosition = Vector2.Lerp(start2, target2, t);
            daun3.anchoredPosition = Vector2.Lerp(start3, target3, t);

            yield return null;
        }
    }
}
