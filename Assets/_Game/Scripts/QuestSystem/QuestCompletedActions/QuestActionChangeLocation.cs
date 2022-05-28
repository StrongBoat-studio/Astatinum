using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Quest System/Quest Actions")]
public class QuestActionChangeLocation : QuestAction
{
    public SceneIndexer.SceneType sceneToLoad;

    public override void Do()
    {
        LevelLoader.Instance.LoadLevelTransition(sceneToLoad);
    }
}
