using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _interactionText; //Hints
    private List<TextMeshProUGUI> _interactionsText = new List<TextMeshProUGUI>();
    private RectTransform _interactionPointerUI;
    private int _currentInteraction = 0;

    private Interactable _interactable = null;
    private List<Interactable> _interactables = new List<Interactable>();

    private void Start()
    {
        _interactionsText.AddRange(GameManager.Instance.mainCanvas.GetComponent<CanvasManager>().GetInteractionsUI().GetComponentsInChildren<TextMeshProUGUI>());
        _interactionPointerUI = GameManager.Instance.mainCanvas.GetComponent<CanvasManager>().GetInteractionsUI().Find("Pointer").GetComponent<RectTransform>();

        GameManager.Instance.playerControls.Interactions.Interact.started += On_InteractStarted;
        GameManager.Instance.playerControls.Interactions.ChooseInteraction.started += On_ChooseInteractionStarted;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Activate interaction hint game object
        //Get interaction hint text
        //Save reference to objects
        if (other.GetComponent<Interactable>() != null)
        {
            bool canAdd = true;

            foreach (Interactable i in _interactables)
            {
                if (i == other.GetComponent<Interactable>()) canAdd = false;
            }

            if (canAdd)
            {
                _interactables.Add(other.GetComponent<Interactable>());
                RefreshUI();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //delete interactable object reference
        //set interaction hint text to empty string
        //disable interaction hint text
        if (other.GetComponent<Interactable>() != null)
        {
            foreach (Interactable i in _interactables)
            {
                if (i == other.GetComponent<Interactable>())
                {
                    _interactables.Remove(i);
                    RefreshUI();
                    break;
                }
            }
        }
    }

    private void RefreshUI()
    { 
        //If there are no interaction distable whole UI
        if(_interactables.Count == 0)
        {
            GameManager.Instance.mainCanvas.GetComponent<CanvasManager>().GetInteractionsUI().gameObject.SetActive(false);
        }
        else
        {
            GameManager.Instance.mainCanvas.GetComponent<CanvasManager>().GetInteractionsUI().gameObject.SetActive(true);
        }

        //Disable all TMPro gameobjects
        foreach(TextMeshProUGUI text in _interactionsText)
        {
            text.gameObject.SetActive(false);
        }

        //Set text and reenable TMPro gameobjects
        for(int i = 0; i < _interactables.Count; i++)
        {
            _interactionsText[i].gameObject.SetActive(true);
            _interactionsText[i].text = _interactables[i].GetInteractionDescription();
        }

        _interactionPointerUI.localPosition = new Vector2(0f, _interactionsText[_currentInteraction].gameObject.GetComponent<RectTransform>().localPosition.y);
    }

    public void On_InteractStarted(InputAction.CallbackContext context)
    {
        //Interaction hint button pressed
        if (_interactables[_currentInteraction] != null)
        {
            _interactables[_currentInteraction].Interact();
        }
    }

    public void On_ChooseInteractionStarted(InputAction.CallbackContext context)
    {
        Vector2 scrollData = context.ReadValue<Vector2>().normalized;
        if(scrollData.y == -1 && _currentInteraction < _interactables.Count - 1)
        {
            _currentInteraction++;
        }
        else if(scrollData.y == 1 && _currentInteraction > 0)
        {
            _currentInteraction--;
        }

        RefreshUI();
    }

    public void ForceRemoveInteraction(Interactable interaction)
    {
        _interactables.Remove(interaction);
        RefreshUI();
    }
}
