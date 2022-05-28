using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        currentLevelSceneIndex = (int)SceneIndexer.SceneType.TutorialScene;

        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    public Transform player;
    public Transform playerPrefab;
    public Cinemachine.CinemachineVirtualCamera mainVCam;
    public PlayerControls playerControls;
    public Transform mainCanvas;
    public Transform mainCanvasPrefab;
    public int currentLevel;
    public int currentLevelSceneIndex;
}
