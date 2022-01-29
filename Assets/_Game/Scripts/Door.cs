using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private float moveOffset;

    private void MoveToOtherSide(Transform player)
    {
        Vector3 playerPos = player.gameObject.transform.position;
        Vector3 doorPos = transform.position;
        Vector3 doorScale = transform.localScale;
        float xOffset = doorPos.x - playerPos.x;
        int moveDir = (xOffset >= 0) ? 1 : -1;
        player.position = new Vector3(doorPos.x + (moveDir * moveOffset) + (doorScale.x * moveDir), playerPos.y, playerPos.z);
    }

    public override void Interact()
    {
        var p = GameObject.Find("Player").transform;
        MoveToOtherSide(p);
    }

    public override string GetInteractionDescription()
    {
        return "Press [F] to move";
    }
}
