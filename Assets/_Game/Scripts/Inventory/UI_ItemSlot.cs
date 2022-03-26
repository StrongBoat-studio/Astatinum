using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//UI item slot
//Represents item slot displayed on UI
public class UI_ItemSlot : MonoBehaviour, IDropHandler
{
    private Action _onDropAction = () => { };

    public void SetOnDropAction(Action onDrop)
    {
        //Set action to be done after dropping something on the slot
        _onDropAction = onDrop;
    }

    public void OnDrop(PointerEventData eventData)
    {
        UI_ItemDrag.Instance.Hide();
        _onDropAction();
    }
}
