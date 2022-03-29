using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : Interactable
{
    [SerializeField] private string _name;
    [SerializeField] private TextAsset _dialogue;

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        PlayerControls pc = new PlayerControls();
        string description = _interactionDescription.Replace("key", pc.Player.Interact.controls[0].name.ToUpper()).Replace("name", _name);
        return description;
    }

    public override void Interact()
    {
        DialogueManager.Instance.EnterDialogueMode(_dialogue);
    }
}
