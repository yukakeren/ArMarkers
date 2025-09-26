using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArticleID
{
    hias,
    obat,
    kayu,
    unik,
    makan,
    tropis
}

[System.Serializable]
public class ArticleData
{
    public ArticleID id;
    public string title;
    public Sprite thumbnail;
    public string sceneToLoad;
}
