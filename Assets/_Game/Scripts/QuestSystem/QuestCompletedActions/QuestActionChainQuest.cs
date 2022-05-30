using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest Actions/Chain quest")]
public class QuestActionChainQuest : QuestAction
{
    public QuestData nextQuest;

    public override void Do()
    {
        GameManager.Instance.player.GetComponent<Player>().questSystem.AddQuest(QuestAssets.Instance.CreateQuest(nextQuest.questType, nextQuest.questID));
    }
}
