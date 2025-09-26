using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FilterManager : MonoBehaviour
{
    [Header("References")]
    public Transform contentParent;
    public TMP_InputField searchInput;

    private List<ArticleItem> articles = new List<ArticleItem>();

    void Start()
    {
        Refresh();

        // supaya search jalan otomatis saat user ngetik
        if (searchInput != null)
            searchInput.onValueChanged.AddListener(SearchArticles);
    }

    public void Refresh()
    {
        articles.Clear();
        foreach (Transform child in contentParent)
        {
            ArticleItem item = child.GetComponent<ArticleItem>();
            if (item != null)
                articles.Add(item);
        }
    }

    public void ShowAll()
    {
        foreach (var article in articles)
            article.gameObject.SetActive(true);
    }

    public void ShowByID(ArticleID id)
    {
        Debug.Log("Filtering by: " + id);
        foreach (var article in articles)
            article.gameObject.SetActive(article.ID == id);
    }

    // === SEARCH FEATURE ===
    public void SearchArticles(string query)
    {
        query = query.ToLower();

        foreach (var article in articles)
        {
            bool match = article.Title.ToLower().Contains(query);
            Debug.Log($"[SEARCH] {article.Title} match={match}");

            article.gameObject.SetActive(match || string.IsNullOrEmpty(query));
        }
    }

    // fungsi fuzzy search
    int GetLevenshteinDistance(string a, string b)
    {
        if (string.IsNullOrEmpty(a)) return b.Length;
        if (string.IsNullOrEmpty(b)) return a.Length;

        var costs = new int[b.Length + 1];

        for (int j = 0; j < costs.Length; j++)
            costs[j] = j;

        for (int i = 1; i <= a.Length; i++)
        {
            costs[0] = i;
            int corner = i - 1;

            for (int j = 1; j <= b.Length; j++)
            {
                int upper = costs[j];
                int cost = (a[i - 1] == b[j - 1]) ? corner : (corner + 1);
                cost = Mathf.Min(cost, Mathf.Min(costs[j] + 1, costs[j - 1] + 1));
                corner = upper;
                costs[j] = cost;
            }
        }

        return costs[b.Length];
    }

    // === WRAPPER METHODS ===
    public void ShowBungaHias() => ShowByID(ArticleID.hias);
    public void ShowBungaObat() => ShowByID(ArticleID.obat);
    public void ShowBungaKayu() => ShowByID(ArticleID.kayu);
    public void ShowBungaMakan() => ShowByID(ArticleID.makan);
    public void ShowBungaUnik() => ShowByID(ArticleID.unik);
    public void ShowBungaTropis() => ShowByID(ArticleID.tropis);
}
