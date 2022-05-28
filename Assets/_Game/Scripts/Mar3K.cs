using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mar3K : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Transform _player;

    private void Awake()
    {
        //Get referene to player
        _player = GameManager.Instance.player;
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex((int)SceneIndexer.SceneType.PlayerObjects));

        //Scene chanaged event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start() 
    {
    
    }

    private void Update()
    {
        //Calculate direction to player, mirror Mar3K's sprite if necessary
        float xScale = ((_player.position - transform.position) / (_player.position - transform.position).magnitude).x >= 0 ? 1 : -1;
        if(xScale != transform.localScale.x) transform.localScale = new Vector3(xScale, 1f, 1f);
        
        //Move Mar3K towards player if distance between them is greater than 1 unit
        transform.position = Vector3.MoveTowards(_player.position, transform.position, 1f);

        Vector2 vel = GetComponent<Rigidbody>().velocity;
        Vector3 dir = (_player.position - transform.position).normalized;
        Debug.Log(dir);

        if (vel == Vector2.zero)
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
            else if(_animator.GetBool("WalkBack") == true)
            {
                _animator.SetTrigger("IdleBack");
            }
        }
        else
        {
            //Horizontal movement
            if (dir.x > .2f)
            {
                //Right
                _animator.SetTrigger("Walk");
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (dir.x < -.2f)
            {
                //Left
                _animator.SetTrigger("Walk");
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            //Vertical movement
            else if (dir.z > 0)
            {
                //Up
                _animator.SetTrigger("WalkBack");
            }
            else if(dir.z < 0)
            {
                //Down
                _animator.SetTrigger("WalkFront");
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(GameManager.Instance.currentLevelSceneIndex);
    }
}
