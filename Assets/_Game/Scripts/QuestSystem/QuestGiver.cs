using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quest Giver
//This script goes onto gameobject which is responsible for giving player given quest
public class QuestGiver : Interactable
{
    [SerializeField] private QuestScriptableObject.QuestType questType;
    [SerializeField] private int questID;

    public override string GetInteractionDescription()
    {
        return "Press F to take";
    }

    public override void Interact()
    {
        if (questID >= 0)
        {
            Player player = GameManager.Instance.player.GetComponent<Player>();
            PlayerInteraction playerInteraction = GameManager.Instance.player.GetComponent<PlayerInteraction>();
            player.questSystem.AddQuest(QuestAssets.Instance.CreateQuest(questType, questID));
            playerInteraction.ForceRemoveInteraction();
            questID = -1;
            if (gameObject.GetComponent<SphereCollider>().isTrigger)
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }
}
