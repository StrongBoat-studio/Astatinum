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

        DontDestroyOnLoad(this);

        currentLevel = SceneManager.GetActiveScene().buildIndex;

        playerControls = new PlayerControls();
        playerControls.Enable();

        if(mainCanvas == null)
        {
            if (GameObject.FindGameObjectWithTag("MainCanvas") == null)
            {
                Instantiate(mainCanvasPrefab);
            }
            else
            {
                mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            }
        }

        if (player == null)
        {
            if(FindObjectOfType<Player>() == null)
            {
                Instantiate(playerPrefab);
            }
            else
            {
                player = FindObjectOfType<Player>().transform;
            }
        }
    }

    public Transform player;
    public Transform playerPrefab;
    public PlayerControls playerControls;
    public Transform mainCanvas;
    public Transform mainCanvasPrefab;
    public int currentLevel;
}
