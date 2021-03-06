using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using FMODUnity;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _jumpForce = 30f;
    [SerializeField] private float _movementSpeed = 10f;
    private Vector3 _move;
    private bool _jump = false;
    private Rigidbody _rb;
    private bool _check = false;
    private int materialValue = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        GameManager.Instance.playerControls.Player.Jump.performed += Jump;
    }

    private void FixedUpdate()
    {
        //Change velocity
        _rb.velocity = _move * _movementSpeed * Time.deltaTime;

        //Jump if can
        if(_jump)
        {
            _rb.AddForce(Vector3.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _jump = false;
        }
    }

    private void Update()
    {
        //Set _move vector 
        Vector2 move2D = GameManager.Instance.playerControls.Player.Movement.ReadValue<Vector2>();
        _move = new Vector3(move2D.x, _rb.velocity.y, move2D.y);

        //Movement animations logic
        if (move2D == Vector2.zero)
        {
            //Idle
            if (_animator.GetBool("Walk") == true)
            {
                _animator.SetTrigger("Idle");
            }
            else if (_animator.GetBool("WalkFront") == true)
            {
                _animator.SetTrigger("IdleFront");
            }
            else if (_animator.GetBool("WalkBack") == true)
            {
                _animator.SetTrigger("IdleBack");
            }
        }
        else
        {
            //Horizontal movement
            if(move2D.x > 0)
            {
                //Right
                _animator.SetTrigger("Walk");
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if(move2D.x < 0)
            {
                //Left
                _animator.SetTrigger("Walk");
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            //Vertical movement
            else if (move2D.y > 0)
            {
                //Up & Down
                _animator.SetTrigger("WalkBack");
            }
            else if(move2D.y < 0)
            {
                _animator.SetTrigger("WalkFront");
            }
        }
    }

    public void Jump (InputAction.CallbackContext context)
    {
        if (context.performed && _check)
        {
            _jump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _check = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _check = false;
        }
    }

    public void PlayWalkEvenet()
    {
        SceneCheck();
        switch(materialValue)
        {
            case 0: AudioManager.Instance.PlayGameAstaWalkGrassRandom(); break;
            case 1: AudioManager.Instance.PlayGameAstaWalkSoildRandom(); break;
        }
    }

    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }

    private void SceneCheck()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.TutorialScene ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.TutorialScene2 ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.DomEzila ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.DomEzila2 ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.RoadScene ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.AstaRoom ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.AstaRoom2 ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.Bathroom ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.Bathroom2)
            {
                materialValue = 1;
            }
            else if(
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.LocationOneScene ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.Podworko2 ||
                SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.Junkyard)
            {
                materialValue = 0;
            }
        }
    }
}
