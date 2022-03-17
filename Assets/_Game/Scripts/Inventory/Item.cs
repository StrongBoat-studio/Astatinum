using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public ItemType itemType = ItemType.Item1;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Item1: return ItemAssets.Instance.item1;
            case ItemType.Item2: return ItemAssets.Instance.item2;
            case ItemType.Item3: return ItemAssets.Instance.item3;
            case ItemType.Item4: return ItemAssets.Instance.item4;
            case ItemType.Item5: return ItemAssets.Instance.item5;
        }
    }

    public string GetName()
    {
        switch (itemType)
        {
            default:
            case ItemType.Item1: return "Item 1";
            case ItemType.Item2: return "Item 2";
            case ItemType.Item3: return "Item 3";
            case ItemType.Item4: return "Item 4";
            case ItemType.Item5: return "Item 5";
        }
    }
}
