using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Quest Giver
//This script goes onto gameobject which is responsible for giving player given quest
public class QuestGiver : Interactable
{
    [SerializeField] private QuestData quest;
    bool questTaken = false;

    public class QuestGiverEventArgs : EventArgs
    {
        public QuestData quest;
    }
    public event EventHandler<QuestGiverEventArgs> onQuestTaken;

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
            _canBeInteractedWith = false;
        }
    }
}
