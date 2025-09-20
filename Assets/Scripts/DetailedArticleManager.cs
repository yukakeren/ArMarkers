using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailedArticleManager : MonoBehaviour
{
    [Header("References")]
    public Transform contentParent;          // tempat spawn prefab
    public GameObject detailPrefab;          // prefab template
    public DetailedArticleData[] allArticles; // data semua artikel

    void Start()
    {
        // ambil key dari PlayerPrefs (set di Encyclopedia)
        string selectedKey = ArticleSelection.SelectedKey;
        Debug.Log("[DetailedArticleManager] SelectedKey: " + selectedKey);
        
        if(string.IsNullOrEmpty(selectedKey))
        {
            Debug.LogWarning("[DetailedArticleManager] SelectedKey kosong!");
            return;
        }

        // cari data yang sesuai
        DetailedArticleData data = System.Array.Find(allArticles, a => a.key == selectedKey);

        if (data != null)
        {
            GameObject go = Instantiate(detailPrefab, contentParent);
            DetailedArticleInstance instance = go.GetComponent<DetailedArticleInstance>();
            if(instance != null)
            {
                instance.articleImage.sprite = data.image;
                instance.articleTitle.text = data.title;
            }
            // assign konten
            Image img = go.transform.Find("Image")?.GetComponent<Image>();
            TMP_Text txt = go.transform.Find("Text (TMP)")?.GetComponent<TMP_Text>();

            if(img != null) img.sprite = data.image;
            if(txt != null) txt.text = data.title;
        }
        else
        {
            Debug.LogWarning("[DetailedArticleManager] No data matches key: " + selectedKey);
        }
    }
}

