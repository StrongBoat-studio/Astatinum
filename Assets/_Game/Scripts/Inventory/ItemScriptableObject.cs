using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item scripatble obecjt
//Stores item data
[CreateAssetMenu(menuName = "Scriptable objects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public Item.ItemType itemType;
    public string itemName;
    public Sprite itemSprite;
}
