using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //[SerializeField] private GameObject _uiInteract;
    [SerializeField] private float _JumpForce = 30f;
    [SerializeField] private float _Speed = 10f;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private PlayerControls _playerControls;
    private bool _Check= false;

    private GameObject _interactable = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();

        _playerControls = new PlayerControls();
        _playerControls.Player.Enable();
        _playerControls.Player.Jump.performed += Jump;
        //_playerControls.Player.Interact.performed += On_InteractPerformed;
    }

    private void Update()
    {
        Vector2 inputVector = _playerControls.Player.Movement.ReadValue<Vector2>();
        transform.position = transform.position + new Vector3(inputVector.x, 0, inputVector.y) * _Speed * Time.deltaTime;
    }

    public void Jump (InputAction.CallbackContext context)
    {
        if (context.performed && _Check)
        {
            _rigidbody.AddForce(Vector3.up * _JumpForce, ForceMode.Impulse);
        }
    }

/*    public void On_InteractPerformed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (_interactable != null)
            {
                _interactable.GetComponent<IInteractable>().Interact();
            }
        }
    }*/

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            _Check = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            _Check = false;
        }
    }

/*    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IInteractable>() != null)
        {
            _uiInteract.SetActive(true);
            _interactable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            _uiInteract.SetActive(false);
            _interactable = null;
        }
    }*/
}
