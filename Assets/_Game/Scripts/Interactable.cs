using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string _interactionDescription; //Text hint

    public abstract void Interact(); //Functionality
    public abstract string GetInteractionDescription(); //Get text hint
}
