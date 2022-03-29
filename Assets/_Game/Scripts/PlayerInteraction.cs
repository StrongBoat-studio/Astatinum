using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _interactionText; //Hint
    //private PlayerControls _playerControls;

    private Interactable _interactable = null;

    private void Start()
    {
        //_playerControls = new PlayerControls();
        //_playerControls.Enable();
        //_playerControls.Player.Interact.started += On_InteractStarted;
        GameManager.Instance.playerControls.Player.Interact.started += On_InteractStarted;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Activate interaction hint game object
        //Get interaction hint text
        //Save reference to object
        if (other.GetComponent<Interactable>() != null)
        {
            _interactionText.gameObject.SetActive(true);
            _interactionText.text = other.GetComponent<Interactable>().GetInteractionDescription();
            _interactable = other.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //delete interactable object reference
        //set interaction hint text to empty string
        //disable interaction hint text
        _interactable = null;
        _interactionText.text = "";
        _interactionText.gameObject.SetActive(false);
    }

    public void On_InteractStarted(InputAction.CallbackContext context)
    {
        //Interaction hint button pressed
        if(_interactable != null)
            _interactable.Interact();
    }

    public void ForceRemoveInteraction()
    {
        _interactable = null;
        _interactionText.text = "";
        _interactionText.gameObject.SetActive(false);
    }
}
