using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item use action - Mar3K")]
public class ItemUseMar3K : ItemUseAction
{
    public Transform mar3kPrefab;

    public override void Do(Item item)
    {
        if(GameManager.Instance.player.GetComponent<Player>().inventory.RemoveItem(item))
        {
            Instantiate(mar3kPrefab, GameManager.Instance.player.position, Quaternion.identity);
        }
    }
}