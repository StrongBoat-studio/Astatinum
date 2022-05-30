using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quest Target
//This script goes onto gameobject which is responsible for finishing the quest
public class QuestTarger : Interactable
{
    [SerializeField] private QuestData _questToReceive;
    [SerializeField] private List<string> _interactConditions;
    [SerializeField] private string _interactionDescriptionFail;

    private void Awake()
    {
        _canBeInteractedWith = false;
        GameManager.Instance.player.GetComponent<Player>().questSystem.onQuestAdd += QuestSystem_OnQuestAdd;
    }

    private void QuestSystem_OnQuestAdd(object sender, QuestSystem.QuestChangeEventArgs e)
    {
        if(e.quest.questData == _questToReceive) _canBeInteractedWith = true;
    }

    public override string GetInteractionDescription()
    {
        string description = _interactionDescription.Replace("key", GameManager.Instance.playerControls.Interactions.Interact.controls[0].name.ToUpper()).Replace("quest", _questToReceive.questTitle);

        if (CanInteract())
            return description;
        else
            return _interactionDescriptionFail;
    }

    public override void Interact()
    {
        if (CanInteract())
        {
            Player player = GameManager.Instance.player.GetComponent<Player>();
            PlayerInteraction playerInteraction = GameManager.Instance.player.GetComponent<PlayerInteraction>();
            foreach (Quest quest in player.questSystem.activeQuests)
            {
                if (quest.questData.questID == _questToReceive.questID)
                {
                    if (quest.CompleteQuest())
                    {
                        playerInteraction.ForceRemoveInteraction(GetComponent<Interactable>());
                        _canBeInteractedWith = false;
                        break;
                    }
                }
            }
        }
    }

    private bool CanInteract()
    {
        if (_interactConditions == null || _interactConditions.Count == 0) return true;

        bool canInteract = true;

        foreach (string condition in _interactConditions)
        {
            string prefix = condition.Split(':')[0];
            string param = condition.Split(':')[1];

            switch (prefix)
            {
                case "questfinished":
                    if (GameManager.Instance.player.GetComponent<Player>().questSystem.activeQuests.Find(x => x.questData.questID == int.Parse(param)) != null) canInteract = false;
                    break;
                default:
                    break;
            }
        }

        return canInteract;
    }
}
