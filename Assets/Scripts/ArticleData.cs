using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArticleID
{
    Bunga1,
    Bunga2,
    Bunga3
}

[System.Serializable]
public class ArticleData
{
    public ArticleID id;
    public string title;
    public Sprite thumbnail;
    public string sceneToLoad;
}
