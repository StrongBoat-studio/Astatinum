using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNote : Interactable
{
    [SerializeField] private Note _note;
    public Note note { get => _note; }

    private bool _isTaken = false;

    public override string GetInteractionDescription()
    {
        if (!_isTaken)
        {
            //Get key name from action map and replace 'key' with it
            string description = _interactionDescription.Replace("key", GameManager.Instance.playerControls.Interactions.Interact.controls[0].name.ToUpper());
            return description;
        }
        else
        {
            return "";
        }
    }

    public override void Interact()
    {
        if (_isTaken)
        {
            if (GetComponent<SphereCollider>().enabled)
                GetComponent<SphereCollider>().enabled = false;

            return;
        }

        if (GameManager.Instance.player.GetComponent<Player>().TakeWorldNote(_note))
        {
            _isTaken = true;
            GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveInteraction(GetComponent<Interactable>());
        }
    }
}
