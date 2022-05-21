using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    private List<TextMeshProUGUI> _interactionsText = new List<TextMeshProUGUI>();
    private RectTransform _interactionPointerUI;
    private int _currentInteraction = 0;

    private List<Interactable> _interactables = new List<Interactable>();

    private void Start()
    {
        //_interactable = null;
        _interactables = new List<Interactable>();
        _currentInteraction = 0;

        _interactionsText.AddRange(GameManager.Instance.mainCanvas.GetComponent<CanvasManager>().GetInteractionsUI().GetComponentsInChildren<TextMeshProUGUI>());
        _interactionPointerUI = GameManager.Instance.mainCanvas.GetComponent<CanvasManager>().GetInteractionsUI().Find("Pointer").GetComponent<RectTransform>();

        GameManager.Instance.playerControls.Interactions.Interact.started += On_InteractStarted;
        GameManager.Instance.playerControls.Interactions.ChooseInteraction.started += On_ChooseInteractionStarted;

        RefreshUI(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        RefreshUI(other);
    }

    private void OnTriggerStay(Collider other)
    {
        RefreshUI(other);
    }

    private void OnTriggerExit(Collider other)
    {
        RefreshUI(other);
    }

    private void OnDestroy()
    {
        //_interactable = null;
        _interactables = new List<Interactable>();
        _currentInteraction = 0;

        GameManager.Instance.playerControls.Interactions.Interact.started -= On_InteractStarted;
        GameManager.Instance.playerControls.Interactions.ChooseInteraction.started -= On_ChooseInteractionStarted;

        RefreshUI(null);
    }

    private void RefreshUI(Collider other)
    {
        if (other != null)
        {
            _interactables.Clear();
            if (other.GetComponent<Interactable>() != null)
            {
                Interactable[] allInteractables = other.GetComponents<Interactable>();
                foreach (Interactable interactable in allInteractables)
                {
                    foreach (Interactable i in _interactables)
                        if (_interactables.Contains(i)) continue;

                    if (interactable.GetCanBeInteractedWith())
                        _interactables.Add(interactable);
                }
            }
        }

        //If there are no interaction distable whole UI
        if (_interactables.Count == 0)
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

        //Are there any interactions available?
        if (_interactables.Count <= 0) return;

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

        RefreshUI(null);
    }

    public void ForceRemoveInteraction(Interactable interaction)
    {
        //Fix pointer after remove
        _interactables.Remove(interaction);
        _currentInteraction = 0;
        RefreshUI(null);
    }

    public void ForceRemoveAllInteractions()
    {
        //Fix pointer after remove
        _interactables.Clear();
        _currentInteraction = 0;
        RefreshUI(null);
    }
}
