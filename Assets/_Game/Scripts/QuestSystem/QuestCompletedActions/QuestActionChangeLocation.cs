using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest Actions")]
public class QuestActionChangeLocation : QuestAction
{
    public SceneIndexer.SceneType sceneToLoad;

    public override void Do()
    {
        //UnityEngine.GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel((int)sceneToLoad);
        GameManager.Instance.levelLoader.LoadNextLevel((int)sceneToLoad);
    }
}
