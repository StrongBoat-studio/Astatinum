using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mar3K : MonoBehaviour
{
    private Transform _player;
    private Rigidbody _rb;

    private void Awake()
    {
        _player = GameManager.Instance.player;
        _rb = GetComponent<Rigidbody>();

        SceneManager.activeSceneChanged += OnSceneChange;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Vector3.Distance(_player.position, transform.position) > 1f)
        {
            _rb.AddForce(_player.position - transform.position, ForceMode.Force);
        }
    }

    private void OnSceneChange(Scene arg0, Scene arg1)
    {
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(arg1.buildIndex);
    }
}
