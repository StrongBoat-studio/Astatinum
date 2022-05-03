using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUseAction : ScriptableObject
{
    public abstract void Do(Item item);
}
