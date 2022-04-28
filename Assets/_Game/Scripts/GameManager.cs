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

        /*if(GameObject.Find(playerPrefab.name) == null && SceneManager.GetActiveScene().buildIndex == (int)SceneIndexer.SceneType.TutorialScene)
        {
            player = Instantiate(playerPrefab);
            player.transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex((int)SceneIndexer.SceneType.TutorialScene);
        }*/

        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    public Transform player;
    public Transform playerPrefab;
    public PlayerControls playerControls;
    public Transform mainCanvas;
}
