using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArticleItemClick : MonoBehaviour
{
    public ArticleItem articleItem; // assign di inspector (prefab root)

    void Start()
    {
        Button btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(OnClickArticle);
        }
    }

    void OnClickArticle()
    {
        Debug.Log($"Klik artikel: {articleItem.Title}, sceneToLoad={articleItem.sceneToLoad}");
        // Simpan key sementara di static variable
        ArticleSelection.SelectedKey = articleItem.sceneToLoad;
        // Load detailed article scene
        SceneManager.LoadScene("DetailedArticleScene");
    }
}
