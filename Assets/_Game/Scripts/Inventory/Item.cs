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
        Item5
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
