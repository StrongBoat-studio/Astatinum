using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quest Giver
//This script goes onto gameobject which is responsible for giving player given quest
public class QuestGiver : Interactable
{
    [SerializeField] private QuestScriptableObject quest;
    bool questTaken = false;

    public override string GetInteractionDescription()
    {
        string description = _interactionDescription.Replace("key", GameManager.Instance.playerControls.Interactions.Interact.controls[0].name.ToUpper()).Replace("quest", quest.questTitle);
        return description;
    }

    public override void Interact()
    {
        if (!questTaken)
        {
            Player player = GameManager.Instance.player.GetComponent<Player>();
            PlayerInteraction playerInteraction = GameManager.Instance.player.GetComponent<PlayerInteraction>();
            player.questSystem.AddQuest(QuestAssets.Instance.CreateQuest(quest.questType, quest.questID));
            playerInteraction.ForceRemoveInteraction(GetComponent<Interactable>());
            questTaken = true;
            if (gameObject.GetComponent<SphereCollider>().isTrigger)
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }
}
