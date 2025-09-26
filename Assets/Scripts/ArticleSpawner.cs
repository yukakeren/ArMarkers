using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ArticleSpawner : MonoBehaviour
{
    public GameObject articlePrefab;
    public Transform contentParent;
    public ArticleData[] articles;

    [Header("Optional Link")]
    public FilterManager filterManager;  // <--- Tambahin ini

    void Start()
    {
        foreach (var article in articles)
        {
            GameObject newItem = Instantiate(articlePrefab, contentParent);

            // isi data ke ArticleItem
            ArticleItem item = newItem.GetComponent<ArticleItem>();
            item.ID = article.id;
            item.Title = article.title;
            item.Thumbnail = article.thumbnail;
            item.sceneToLoad = article.sceneToLoad;

            // isi ke UI
            TMP_Text caption = newItem.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            Image thumb = newItem.transform.Find("Thumbnail").GetComponent<Image>();
            caption.text = item.Title;
            thumb.sprite = item.Thumbnail;

            // tombol untuk pindah scene
            Button button = newItem.GetComponent<Button>();
            string sceneName = article.sceneToLoad;
            // button.onClick.AddListener(() => {
            //     Debug.Log("Loading scene: " + sceneName);
            //     SceneManager.LoadScene(sceneName);
            // });
            button.onClick.AddListener(() =>
            {
                Debug.Log($"Klik artikel: {article.title}, sceneToLoad={article.sceneToLoad}");
                ArticleSelection.SelectedKey = article.sceneToLoad; // simpan key sementara
                UnityEngine.SceneManagement.SceneManager.LoadScene("DetailedArticleScene");
            });

        }

        // === panggil refresh biar FilterManager tahu semua artikel baru ===
        if (filterManager != null)
        {
            filterManager.Refresh();
        }
    }
}
