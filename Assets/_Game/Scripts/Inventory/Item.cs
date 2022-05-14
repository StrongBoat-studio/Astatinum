using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item
//Class representing items data
[System.Serializable]
public class Item
{
    public enum ItemType
    {
        Item1,
        Item2,
        Item3,
        Item4,
        Key,
        Mar3K_head,
        Mar3K_arm_left,
        Mar3K_arm_right,
        Mar3K_body,
        Mar3K_legs,
        Mar3k
    }

    public ItemData itemData;

    public Sprite GetItemSprite()
    {
        return itemData.itemSprite;
    }

    public string GetItemName()
    {
        return itemData.itemName;
    }

    public ItemType GetItemType()
    {
        return itemData.itemType;
    }
}
