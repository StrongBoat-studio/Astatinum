using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interfce included to classes that can hold item refereces
public interface IItemHolder
{
    public void RemoveItemFromHolder(Item item);
}
