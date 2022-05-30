using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : Interactable
{
    [SerializeField] private string _name;
    [SerializeField] private TextAsset _dialogue;
    [SerializeField] private string _interactCondition;
    [SerializeField] private string _interactionDescriptionFail;

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        PlayerControls pc = new PlayerControls();
        string description = "";
        if (CanInteract())
            description = _interactionDescription.Replace("key", pc.Interactions.Interact.controls[0].name.ToUpper()).Replace("name", _name);
        else
            description = _interactionDescriptionFail;
        return description;
    }

    public override void Interact()
    {
        if(CanInteract())
            DialogueManager.Instance.EnterDialogueMode(_dialogue);
    }

    private bool CanInteract()
    {
        if (_interactCondition == null || _interactCondition == "") return true;

        string prefix = _interactCondition.Split(':')[0];
        string param = _interactCondition.Split(':')[1];

        switch(prefix)
        {
            case "hasitem":
                foreach (ItemData itemData in ItemAssets.Instance.itemsData)
                {
                    if (itemData.itemType.ToString() == param)
                    {
                        if(GameManager.Instance.player.GetComponent<Player>().inventory.GetFirstInventorySlotWithItemOfItemData(itemData) != null)
                        {
                            AudioManager.Instance.PlayGameKeyRandom();
                            return true;
                        }
                    }
                }
                return false;
            case "hasjournal":
                if (GameManager.Instance.player.GetComponent<Player>().journal != null) return true;
                return false;
            case "questfinished":
                if (GameManager.Instance.player.GetComponent<Player>().questSystem.activeQuests.Find(x => x.questData.questID == int.Parse(param)) != null) return false;
                else return true;
            default: 
                return true;
        }
    }

    public TextAsset GetDialogue()
    {
        return _dialogue;
    }
}
