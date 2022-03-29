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

    public ItemScriptableObject itemScriptableObject;

    public Sprite GetItemSprite()
    {
        return itemScriptableObject.itemSprite;
    }

    public string GetItemName()
    {
        return itemScriptableObject.itemName;
    }

    public ItemType GetItemType()
    {
        return itemScriptableObject.itemType;
    }
}
