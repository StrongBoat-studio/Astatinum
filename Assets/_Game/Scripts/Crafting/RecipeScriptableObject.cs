using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Recipes")]
public class RecipeScriptableObject : ScriptableObject
{
    public ItemData output;

    public ItemData item_00;
    public ItemData item_10;
    public ItemData item_20;
                                
    public ItemData item_01;
    public ItemData item_11;
    public ItemData item_21;
                                
    public ItemData item_02;
    public ItemData item_12;
    public ItemData item_22;

    public ItemData GetItem(int x, int y)
    {
        if (x == 0 && y == 0) return item_00;
        else if (x == 1 && y == 0) return item_10;
        else if (x == 2 && y == 0) return item_20;
        else if (x == 0 && y == 1) return item_01;
        else if (x == 1 && y == 1) return item_11;
        else if (x == 2 && y == 1) return item_21;
        else if (x == 0 && y == 2) return item_02;
        else if (x == 1 && y == 2) return item_12;
        else if (x == 2 && y == 2) return item_22;
        else return null;
    }
}
