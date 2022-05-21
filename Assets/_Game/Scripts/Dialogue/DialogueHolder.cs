using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : Interactable
{
    [SerializeField] private string _name;
    [SerializeField] private TextAsset _dialogue;
    [SerializeField] private string _interactCondition;

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        PlayerControls pc = new PlayerControls();
        string description = _interactionDescription.Replace("key", pc.Interactions.Interact.controls[0].name.ToUpper()).Replace("name", _name);
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
                            return true;
                        }
                    }
                }
                return false;
            default: 
                return true;
        }
    }
}
