using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private RectTransform _journal;
    [SerializeField] private RectTransform _dialogue;
    private bool _dialogueActive;
    [SerializeField] private RectTransform _interactions;
    [SerializeField] private RectTransform _pauseMenu;
    [SerializeField] private RectTransform _eventSystems;
    public static bool gamePaused = false;

    private void Awake()
    {
        //DontDestroyOnLoad(this);

        GameManager.Instance.mainCanvas = transform;
        GameManager.Instance.playerControls.Menu.PauseMenu.performed += OnMenuPause;

        _pauseMenu = transform.Find("PauseMenu").GetComponent<RectTransform>();

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        GameManager.Instance.playerControls.Menu.PauseMenu.performed -= OnMenuPause;
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        if(arg0.buildIndex == (int)SceneIndexer.SceneType.OptionsMenu)
        {
            _pauseMenu.gameObject.SetActive(true);
        }
    }

    private void OnMenuPause(InputAction.CallbackContext obj)
    {
        if(gamePaused == true)
        {
            ResumeGame();
        }
        else if(gamePaused == false)
        {
            PauseGame();
        }
    }

    public RectTransform GetInteractionsUI()
    {
        return _interactions;
    }

    private void ResumeGame()
    {
        gamePaused = false;
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.playerControls.Player.Enable();
        GameManager.Instance.playerControls.Interactions.Enable();
        GameManager.Instance.playerControls.Journal.Enable();
        GameManager.Instance.playerControls.Dialogue.Enable();

        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            //Do nothing
            if (child.gameObject == _pauseMenu.gameObject) continue;
            if (child.gameObject == _journal.gameObject) continue;
            if (child.gameObject == _eventSystems.gameObject) continue;

            //Show the rest
            child.gameObject.SetActive(true);

            //Update from saved values
            if (child.gameObject.name == "DialoguePanel") child.gameObject.SetActive(_dialogueActive);
        }
    }

    private void PauseGame()
    {
        //Pause game - disable playercontrolls, show pause menu overlay, stop time
        gamePaused = true;
        Debug.Log(_pauseMenu.name);
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.playerControls.Player.Disable();
        GameManager.Instance.playerControls.Interactions.Disable();
        GameManager.Instance.playerControls.Journal.Disable();
        GameManager.Instance.playerControls.Dialogue.Disable();

        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            //Do nothing
            if (child.gameObject == _pauseMenu.gameObject) continue;
            if (child.gameObject == _eventSystems.gameObject) continue;
            if (child.gameObject.name == "DialoguePanel") _dialogueActive = child.gameObject.activeSelf;


            //Hide the rest
            child.gameObject.SetActive(false);
        }
    }

    public void PauseMenuResume()
    {
        ResumeGame();
    }

    public void PauseMenuOptions()
    {
        //Load options scene and disable pause menu overlay
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.OptionsMenu, LoadSceneMode.Additive);
        _pauseMenu.gameObject.SetActive(false);
    }

    public void PauseMenuExit()
    {
        //Resume game
        ResumeGame();

        //Load menu scene
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.MainMenu);
    }
}
