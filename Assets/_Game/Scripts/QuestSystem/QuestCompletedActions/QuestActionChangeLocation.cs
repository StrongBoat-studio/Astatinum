using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Quest System/Quest Actions/Change location")]
public class QuestActionChangeLocation : QuestAction
{
    public SceneIndexer.SceneType sceneToLoad;

    public override void Do()
    {
        GameManager.Instance.player.GetComponent<Player>().journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes.Find(x => x.noteID == 2) });
        LevelLoader.Instance.LoadLevelTransition(sceneToLoad);
    }
}
