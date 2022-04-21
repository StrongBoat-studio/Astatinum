using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    [SerializeField] public Transform player;
    public PlayerControls playerControls;
}
