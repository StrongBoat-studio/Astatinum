using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string _interactionDescription; //Text hint
    [SerializeField] protected bool _canBeInteractedWith = true;

    public abstract void Interact(); //Functionality
    public abstract string GetInteractionDescription(); //Get text hint
    public bool GetCanBeInteractedWith()
    {
        return _canBeInteractedWith;
    }
    public void SetCanBeInteractedWith(bool val)
    {
        _canBeInteractedWith = val;
    }
}
