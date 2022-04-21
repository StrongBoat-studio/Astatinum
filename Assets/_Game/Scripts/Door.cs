using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private float moveOffset;

    private void MoveToOtherSide(Transform player)
    {
        //Rewrite?
        Vector3 playerPos = player.gameObject.transform.position;
        Vector3 doorPos = transform.position;
        Vector3 doorScale = transform.localScale;
        float xOffset = doorPos.x - playerPos.x;
        int moveDir = (xOffset >= 0) ? 1 : -1;
        player.position = new Vector3(doorPos.x + (moveDir * moveOffset) + (doorScale.x * moveDir), playerPos.y, playerPos.z);
    }

    public override void Interact()
    {
        //Replace with probably static reference to player from singleton
        var p = GameObject.Find("Player").transform;
        MoveToOtherSide(p);
    }

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        PlayerControls pc = new PlayerControls();
        string description = _interactionDescription.Replace("key", pc.Interactions.Interact.controls[0].name.ToUpper());
        return description;
    }
}
