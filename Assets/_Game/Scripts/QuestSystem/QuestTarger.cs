using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quest Target
//This script goes onto gameobject which is responsible for finishing the quest
public class QuestTarger : Interactable
{
    [SerializeField] private QuestScriptableObject questToReceive;

    public override string GetInteractionDescription()
    {
        return "Press F to complete";
    }

    public override void Interact()
    {
        Player player = GameManager.Instance.player.GetComponent<Player>();
        PlayerInteraction playerInteraction = GameManager.Instance.player.GetComponent<PlayerInteraction>();
        foreach (Quest quest in player.questSystem.activeQuests)
        {
            if(quest.questData.questID == questToReceive.questID)
            {
                if (quest.CompleteQuest())
                {
                    playerInteraction.ForceRemoveInteraction();
                    if (gameObject.GetComponent<SphereCollider>().isTrigger)
                    {
                        gameObject.GetComponent<SphereCollider>().enabled = false;
                    }
                    break;
                }
            }
        }
    }
}
