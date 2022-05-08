using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mar3K : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        //Get referene to player
        _player = GameManager.Instance.player;

        //Scene chanaged event
        SceneManager.activeSceneChanged += OnSceneChange;

        DontDestroyOnLoad(this);
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
    }

    private void OnSceneChange(Scene arg0, Scene arg1)
    {
        //Set Mar3K's position to spawn location of loaded scene
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(arg1.buildIndex);
    }
}
