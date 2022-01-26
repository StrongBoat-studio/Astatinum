using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float jump_force = 30f;
    [SerializeField] private float speed = 10f;
    private Rigidbody rigidbody;
    private PlayerInput playerInput;
    private PlayerControls playerControls;
    private float check = 0f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        playerControls.Player.Jump.performed += Jump;

    }

    private void Update()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        transform.position = transform.position + new Vector3(inputVector.x, 0, inputVector.y) * speed * Time.deltaTime;
    }

    public void Jump (InputAction.CallbackContext context)
    {
        if (context.performed && check==0)
        {
            rigidbody.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            check = 0f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            check = 1f;
        }
    }
}
